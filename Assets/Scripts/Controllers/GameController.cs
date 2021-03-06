using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Параметры
    [SerializeField] private GameObject pausePanel;

    private sbyte currentPlayerTurn; //Текущий игрок.
    private int turnCount; //Счетчик ходов.

    [Header("Базовые данные")]
    [SerializeField] private GameObject[] turnIcons; //Отметка текущего игрока.
    [SerializeField] private Sprite[] playerIcons; //Х и О.
    [SerializeField] private Image[] tictactoeSpaces; //Клетки поля.

    [Header("Имена игроков")]
    [SerializeField] private Text[] playerNames;
    [Header("Счетчики выигранных раундов")]
    [SerializeField] private Text[] playerScores;

    [Header("Количество раундов")]
    [SerializeField] private int rounds = 3; //Количество раундов.
    private List<int> playerScoreCounts; //Счетчик раундов.

    private sbyte emptyID = -1; //ID пустой клетки.
    private sbyte cpuID; //ID компьютера.
    private sbyte playerID; //ID игрока.

    [Header("Количество очков за победу/поражение")]
    [SerializeField] private int toScore = 100;

    [Header("Окно победы")]
    [SerializeField] private WinWindow winPanel;


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
            playerScoreCounts.Add(Convert.ToInt32(playerScores[k].text));
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
            currentPlayerTurn = 0;
        }
        else
        {
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
            currentPlayerTurn = 1;
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
        tictactoeSpaces[number].GetComponent<Image>().sprite = playerIcons[currentPlayerTurn];
        tictactoeSpaces[number].GetComponent<Image>().color = new Color(0,0,0,255);
        tictactoeSpaces[number].GetComponentInParent<Button>().interactable = false;

        markedSpaces[number] = currentPlayerTurn;
        turnCount++;

        //Если совершено более четырех ходов.
        if(turnCount > 4)
            if (Win(currentPlayerTurn))
                WinnerWindow();

        NextTurn();
    }

    /// <summary>
    /// Переход хода.
    /// </summary>
    private void NextTurn()
    {
        if (currentPlayerTurn == 0)
        {
            currentPlayerTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            currentPlayerTurn = 0;
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
    /// <param name="depth">ID хода.</param>
    /// <param name="isAiTurn">Если ходит компьютер.</param>
    /// <returns>Наиболее приемлемый ход.</returns>
    private int MinMaxCPU(int depth, bool isAiTurn)
    {
        int score;

        if (IsDraw())
            return 0;

        if (Win(cpuID))
            return 100;

        if (Win(playerID))
            return -100;

        if (isAiTurn)
        {
            var bestScore = -100;

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
            var bestScore = 100;

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
        var allSolutions = new sbyte[8, 3]
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
        var scoreCount = ++playerScoreCounts[currentPlayerTurn];

        //Если у игрока выигравшего раунд количество побед меньше необходимого:
        if (playerScoreCounts[currentPlayerTurn] < rounds)
        {
            //Добавление результата к счету игрока и запуск следующего раунда.
            playerScores[currentPlayerTurn].text = scoreCount.ToString();
            InitializeGame();
        }
        else
        {
            //Получение старого рейтинга игрока.
            var winPlayer = PlayerPrefs.GetFloat(playerNames[playerID].text);

            if (currentPlayerTurn != cpuID)
            {
                if (PlayerPrefs.HasKey(playerNames[playerID].text))
                    //Добавление очков к рейтингу.
                    PlayerPrefs.SetFloat(playerNames[playerID].text, toScore + winPlayer);
            }
            else if (PlayerPrefs.HasKey(playerNames[playerID].text))
                //Удаление очков из рейтинга.
                PlayerPrefs.SetFloat(playerNames[playerID].text, -toScore + winPlayer);

            //Запуск затемнения экрана с соответствующим результатом по очкам.
            if (currentPlayerTurn != cpuID)
            {
                gameObject.GetComponent<FaderBlackout>().FaderStart(3f, 0.2f,$"+{toScore}");
            }
            else
            {
                gameObject.GetComponent<FaderBlackout>().FaderStart(3f, 0.2f, $"-{toScore}");
            }

            //Открытие окна победы.
            winPanel.gameObject.SetActive(true);
            winPanel.SetLabelText(playerNames[currentPlayerTurn].text);
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
        if (currentPlayerTurn == cpuID)
            CPUControl();
    }
}