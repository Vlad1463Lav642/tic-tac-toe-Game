using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Параметры
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private sbyte playerTurn; //Текущий игрок.
    [SerializeField] private int turnCount; //Счетчик ходов.
    [SerializeField] private GameObject[] turnIcons; //Отметка текущего игрока.
    [SerializeField] private Sprite[] playerIcons; //Х и О.
    [SerializeField] private Image[] tictactoeSpaces; //Клетки поля.

    [SerializeField] private Text[] playerNames; //Имена игроков.
    [SerializeField] private Text[] playerScores; //Количество выигранных каждым из игроков раундов.

    [SerializeField] private int rounds = 3; //Количество раундов.
    private List<int> playerScoreCounts; //Счетчик раундов.

    private sbyte emptyID = -1; //ID пустой клетки.
    private sbyte cpuID; //ID компьютера.
    private sbyte playerID; //ID игрока.

    [Header("Количество очков за победу/поражение")]
    [SerializeField] private int toScore = 100;

    [SerializeField] private WinWindow winPanel; //Окно победы.


    private List<sbyte> markedSpaces; //Список ID клеток поля.
    #endregion

    /// <summary>
    /// Инициализация игры.
    /// </summary>
    private void InitializeGame()
    {
        turnCount = 0;
        markedSpaces = new List<sbyte>();
        playerScoreCounts = new List<int>();


        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
            tictactoeSpaces[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            tictactoeSpaces[i].GetComponentInParent<Button>().interactable = true;

            markedSpaces.Add(emptyID);
        }

        for(int k = 0; k < playerScores.Length; k++)
        {
            playerScoreCounts.Add(Convert.ToInt32(playerScores[k].text));
        }
    }

    private void Start()
    {
        InitializeGame();

        //Рандомный выбор игрока.
        if (UnityEngine.Random.Range(0f, 10f) <= 5f)
        {
            playerNames[0].text = PlayerPrefs.GetString("CurrentPlayerName");
            playerNames[1].text = "CPU";
            cpuID = 1;
            playerID = 0;
        }
        else
        {
            playerNames[0].text = "CPU";
            playerNames[1].text = PlayerPrefs.GetString("CurrentPlayerName");
            cpuID = 0;
            playerID = 1;
        }

        //Рандомный выбор хода.
        if (UnityEngine.Random.Range(0f, 10f) <= 5f)
        {
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
            playerTurn = 0;
        }
        else
        {
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
            playerTurn = 1;
        }
    }

    public void PauseButton()
    {
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// Записывает в клетку Х или О.
    /// </summary>
    /// <param name="number">ID клетки.</param>
    public void TicTacToeButton(int number)
    {
        tictactoeSpaces[number].GetComponent<Image>().sprite = playerIcons[playerTurn];
        tictactoeSpaces[number].GetComponent<Image>().color = new Color(0,0,0,255);
        tictactoeSpaces[number].GetComponentInParent<Button>().interactable = false;

        markedSpaces[number] = playerTurn;
        turnCount++;

        //Если совершено более четырех ходов.
        if(turnCount > 4)
            if (Win(playerTurn))
                WinnerWindow();

        NextTurn();
    }

    /// <summary>
    /// Переход хода.
    /// </summary>
    private void NextTurn()
    {
        if (playerTurn == 0)
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

    /// <summary>
    /// ИИ компьютера.
    /// </summary>
    private void CPUControl()
    {
        var bestScore = -100;
        var movePosition = emptyID;
        int score;

        for(sbyte i = 0; i < markedSpaces.Count; i++)
        {
            if(markedSpaces[i] == emptyID)
            {
                markedSpaces[i] = cpuID;
                score = MinMaxCPU(0, false);
                markedSpaces[i] = emptyID;

                if (score > bestScore)
                {
                    bestScore = score;
                    movePosition = i;
                }
            }
        }
        if (movePosition > emptyID)
            TicTacToeButton(movePosition);
    }

    /// <summary>
    /// Алгоритм МинМакс.
    /// </summary>
    /// <param name="depth">ID хода по дереву.</param>
    /// <param name="isAiTurn">Если ходит компьютер.</param>
    /// <returns>Наиболее приемлемый ход.</returns>
    private int MinMaxCPU(int depth, bool isAiTurn)
    {
        int score;
        var bestScore = -100;

        if (IsDraw())
            return 0;

        if (Win(cpuID))
            return 100;

        if (Win(playerID))
            return -100;

        if (isAiTurn)
        {
            bestScore = -100;

            for (sbyte i = 0; i < markedSpaces.Count; i++)
            {
                if (markedSpaces[i] == emptyID)
                {
                    markedSpaces[i] = cpuID;
                    score = MinMaxCPU(depth + 1, false);
                    markedSpaces[i] = emptyID;

                    bestScore = Mathf.Max(score, bestScore);
                }
            }
            return bestScore;
        }
        else
        {
            bestScore = 100;

            for (sbyte i = 0; i < markedSpaces.Count; i++)
            {
                if (markedSpaces[i] == emptyID)
                {
                    markedSpaces[i] = playerID;
                    score = MinMaxCPU(depth + 1, true);
                    markedSpaces[i] = emptyID;

                    bestScore = Mathf.Min(score, bestScore);
                }
            }
            return bestScore;
        }
    }

    /// <summary>
    /// Осуществляет проверку победы.
    /// </summary>
    /// <param name="id">ID игрока.</param>
    /// <returns>Значение была победа или нет.</returns>
    private bool Win(int id)
    {
        var victory = false;

        //Массив всех решений.
        sbyte[,] allSolutions = new sbyte[8, 3]
        {
            { markedSpaces[0], markedSpaces[1], markedSpaces[2] },
            { markedSpaces[3], markedSpaces[4], markedSpaces[5] },
            { markedSpaces[6], markedSpaces[7], markedSpaces[8] },
            { markedSpaces[0], markedSpaces[3], markedSpaces[6] },
            { markedSpaces[1], markedSpaces[4], markedSpaces[7] },
            { markedSpaces[2], markedSpaces[5], markedSpaces[8] },
            { markedSpaces[0], markedSpaces[4], markedSpaces[8] },
            { markedSpaces[2], markedSpaces[4], markedSpaces[6] }
        };

        for(byte i = 0; i < 8; i++)
        {
            if (IsDraw())
            {
                if (allSolutions[i, 0] == id && allSolutions[i, 1] == id && allSolutions[i, 2] == id)
                {
                    victory = true;
                    break;
                }
            }
            else
                InitializeGame();
        }

        return victory;
    }


    /// <summary>
    /// Осуществляет управление окном победы.
    /// </summary>
    private void WinnerWindow()
    {
        var scoreCount = ++playerScoreCounts[playerTurn];

        //Если у игрока выигравшего раунд количество побед меньше необходимого:
        if (playerScoreCounts[playerTurn] < rounds)
        {
            //Добавление результата к счету игрока и запуск следующего раунда.
            playerScores[playerTurn].text = scoreCount.ToString();
            InitializeGame();
        }
        else
        {
            //Получение старого рейтинга игрока.
            var winPlayer = PlayerPrefs.GetFloat(playerNames[playerID].text);

            if (playerTurn != cpuID)
            {
                if (PlayerPrefs.HasKey(playerNames[playerID].text))
                    //Добавление 100 очков к рейтингу.
                    PlayerPrefs.SetFloat(playerNames[playerID].text, toScore + winPlayer);
            }
            else if (PlayerPrefs.HasKey(playerNames[playerID].text))
                //Удаление 100 очков из рейтинга.
                PlayerPrefs.SetFloat(playerNames[playerID].text, -toScore + winPlayer);

            //Запуск затемнения экрана с соответствующим результатом по очкам.
            if (playerTurn != cpuID)
            {
                gameObject.GetComponent<FaderBlackout>().FaderStart(3f, 0.2f,"+100");
            }
            else
            {
                gameObject.GetComponent<FaderBlackout>().FaderStart(3f, 0.2f, "-100");
            }

            //Открытие окна победы.
            winPanel.gameObject.SetActive(true);
            winPanel.SetLabelText(playerNames[playerTurn].text);
        }
    }

    /// <summary>
    /// Проверка на существование не занятых клеток.
    /// </summary>
    /// <returns>Значение True/False</returns>
    private bool IsDraw()
    {
        var isDraw = false;

        for(sbyte i = 0; i<markedSpaces.Count; i++)
        {
            if(markedSpaces[i] == emptyID)
                isDraw = true;
        }

        return isDraw;
    }

    private void Update()
    {
        if(playerTurn == cpuID)
            CPUControl();
    }
}