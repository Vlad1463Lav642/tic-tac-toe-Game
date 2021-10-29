using UnityEngine;

/// <summary>
/// Элемент игрока в позиции рекордов.
/// </summary>
public class RatingItem : MonoBehaviour
{
    #region Параметры
    private string playerName;
    private float playerScore;
    #endregion

    #region Конструктор
    public RatingItem(string name, float score)
    {
        PlayerName = name;
        PlayerScore = score;
    }
    #endregion

    /// <summary>
    /// Имя игрока.
    /// </summary>
    public string PlayerName
    {
        get
        {
            return playerName;
        }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                playerName = value;
            }
        }
    }

    /// <summary>
    /// Количество очков у игрока.
    /// </summary>
    public float PlayerScore
    {
        get
        {
            return playerScore;
        }
        set
        {
            playerScore = value;
        }
    }

    /// <summary>
    /// Возвращение данных в виде строки.
    /// </summary>
    /// <returns>Строка данных.</returns>
    public string WriteDataToString()
    {
        return $"{PlayerName}: {PlayerScore}";
    }
}