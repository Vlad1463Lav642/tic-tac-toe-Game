using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RatingPanelController : MonoBehaviour
{
    #region Параметры
    [SerializeField] private float speedPanel = 2f;
    [SerializeField] private Transform arrowImage;
    [SerializeField] private LoginWindow loginPanel;

    private bool isOpen;
    private Vector3 panelPosition;
    private Vector3 closePosition;
    private Vector3 openPosition;
    #endregion

    private void Start()
    {
        panelPosition = transform.position; //Текущие координаты панели.
        closePosition = transform.position; //Координаты по х закрытой панели.
        openPosition = new Vector3(closePosition.x - 304f, closePosition.y, closePosition.z); //Координаты по х открытой панели.
        isOpen = false;

        RatingDataLoad();
    }

    /// <summary>
    /// Передает данные в ScrollViewAdapter для вывода в списке.
    /// </summary>
    public void RatingDataLoad()
    {
        List<string> playerLogins = loginPanel.GetPlayerLogins();
        List<string> ratingScoreData = new List<string>();

        ratingScoreData = playerLogins.Select(item => $"{item}: {PlayerPrefs.GetFloat(item)}").ToList();

        gameObject.GetComponent<ScrollViewAdapter>().AddItems(ratingScoreData);
    }

    private void Update()
    {
        //Сдвиг панели по координатам.
        if (!isOpen)
        {
            if (panelPosition.x <= closePosition.x)
            {
                panelPosition = Vector3.Lerp(panelPosition, closePosition, Time.deltaTime * speedPanel);
            }
        }
        else
        {
            if (panelPosition.x >= openPosition.x)
            {
                panelPosition = Vector3.Lerp(panelPosition, openPosition, Time.deltaTime * speedPanel);
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