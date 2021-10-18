using UnityEngine;

public class RatingPanelController : MonoBehaviour
{
    #region Параметры
    [SerializeField] private float speedPanel = 2f;
    [SerializeField] private Transform arrowImage;
    [SerializeField] private LoginWindow loginPanel;
    private string[] playerLogins;

    private bool isOpen;
    private Vector3 panelPosition;
    private float closePosition;
    private float openPosition;
    #endregion

    private void Start()
    {
        panelPosition = transform.position; //Текущие координаты панели.
        closePosition = transform.position.x; //Координаты по х закрытой панели.
        openPosition = closePosition - 304f; //Координаты по х открытой панели.

        RatingDataLoad();
    }

    /// <summary>
    /// Передает данные в ScrollViewAdapter для вывода в списке.
    /// </summary>
    public void RatingDataLoad()
    {
        playerLogins = loginPanel.GetPlayerLogins();
        string[] ratingScoreData = new string[playerLogins.Length];

        for (int i = 0; i < ratingScoreData.Length; i++)
        {
            ratingScoreData[i] = $"{playerLogins[i]}: {PlayerPrefs.GetFloat(playerLogins[i])}";
        }

        gameObject.GetComponent<ScrollViewAdapter>().AddItems(ratingScoreData);
    }

    private void Update()
    {
        //Сдвиг панели по координатам.
        if (!isOpen)
        {
            if (panelPosition.x <= closePosition)
            {
                panelPosition = new Vector3(panelPosition.x + speedPanel, panelPosition.y, panelPosition.z);
            }
        }
        else
        {
            if (panelPosition.x >= openPosition)
            {
                panelPosition = new Vector3(panelPosition.x - speedPanel, panelPosition.y, panelPosition.z);
            }
        }
        transform.position = panelPosition;
    }

    /// <summary>
    /// Управляет поворотом стрелки на кнопке открытия рейтинга.
    /// </summary>
    public void ControlPanel()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            arrowImage.rotation = new Quaternion(arrowImage.rotation.x, arrowImage.rotation.y, 0f, arrowImage.rotation.w);
        }
        else
        {
            arrowImage.rotation = new Quaternion(arrowImage.rotation.x, arrowImage.rotation.y, -180f, arrowImage.rotation.w);
        }
    }
}