using UnityEngine;

public class RatingItem : MonoBehaviour
{
    private string playerName;
    private float playerScore;

    public RatingItem(string name, float score)
    {
        PlayerName = name;
        PlayerScore = score;
    }

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