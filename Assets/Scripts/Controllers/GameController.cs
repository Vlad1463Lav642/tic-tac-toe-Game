using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private int playerTurn;
    [SerializeField] private int turnCount;
    [SerializeField] private GameObject[] turnIcons;
    [SerializeField] private Sprite[] playerIcons;
    [SerializeField] private Image[] tictactoeSpaces;

    [SerializeField] private Text[] playerNames;
    [SerializeField] private Text[] playerScores;

    [SerializeField] private int rounds = 3;
    private int roundsCount = 0;
    private int[] playerScoreCounts;

    [SerializeField] private WinWindow winPanel;


    private int[] markedSpaces;

    private void InitializeGame()
    {
        playerTurn = 0;
        turnCount = 0;
        markedSpaces = new int[tictactoeSpaces.Length];
        playerScoreCounts = new int[playerScores.Length];


        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
            tictactoeSpaces[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            tictactoeSpaces[i].GetComponentInParent<Button>().interactable = true;
        }

        for (int j = 0; j < markedSpaces.Length; j++)
        {
            markedSpaces[j] = -1;
        }

        for(int k = 0; k < playerScoreCounts.Length; k++)
        {
            playerScoreCounts[k] = Convert.ToInt32(playerScores[k].text);
        }

        if(UnityEngine.Random.Range(0f,10f) <= 5f)
        {
            playerNames[0].text = PlayerPrefs.GetString("CurrentPlayerName");
            playerNames[1].text = "AI";
        }
        else
        {
            playerNames[0].text = "AI";
            playerNames[1].text = PlayerPrefs.GetString("CurrentPlayerName");
        }


        if(UnityEngine.Random.Range(0f,10f) <= 5f)
        {
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else
        {
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    public void PauseButton()
    {
        pausePanel.SetActive(true);
    }

    public void TicTacToeButton(int number)
    {
        tictactoeSpaces[number].GetComponent<Image>().sprite = playerIcons[playerTurn];
        tictactoeSpaces[number].GetComponent<Image>().color = new Color(0,0,0,255);
        tictactoeSpaces[number].GetComponentInParent<Button>().interactable = false;

        markedSpaces[number] = playerTurn + 1;
        turnCount++;

        if(turnCount > 4)
        {
            Win();
        }

        if(playerTurn == 0)
        {
            playerTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            playerTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    private void Win()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[0] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };

        for (int i = 0; i < solutions.Length; i++)
        {
            if(solutions[i] == 3 * (playerTurn + 1))
            {
                WinnerWindow(playerTurn);
                return;
            }
        }
    }

    private void WinnerWindow(int playerID)
    {
        if (roundsCount < rounds)
        {
            roundsCount++;
            float scoreCount = ++playerScoreCounts[playerID];

            playerScores[playerTurn].text = scoreCount.ToString();
            InitializeGame();
        }
        else
        {
            float winPlayer = PlayerPrefs.GetFloat(playerNames[playerID].text);

            Debug.Log(playerNames[0].text);
            Debug.Log(PlayerPrefs.HasKey(playerNames[playerID].text));

            if (PlayerPrefs.HasKey(playerNames[playerID].text))
            {
                PlayerPrefs.SetFloat(playerNames[playerID].text, ++winPlayer);
                Debug.Log(winPlayer);
            }

            winPanel.gameObject.SetActive(true);
            winPanel.SetLabelText(playerNames[playerID].text);
        }
    }
}