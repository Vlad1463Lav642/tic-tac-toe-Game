using UnityEngine;

public class RatingPanelController : MonoBehaviour
{
    [SerializeField] private float speedPanel = 2f;
    [SerializeField] private Transform arrowImage;
    [SerializeField] private LoginWindow loginPanel;
    private string[] playerLogins;

    private bool isOpen;
    private Vector3 panelPosition;
    private float closePosition;
    private float openPosition;

    private void Start()
    {
        panelPosition = transform.position;
        closePosition = transform.position.x;
        openPosition = closePosition - 304f;

        RatingDataLoad();
    }

    public void RatingDataLoad()
    {
        playerLogins = loginPanel.GetPlayerLogins();
        string[] ratingData = new string[playerLogins.Length];

        for (int i = 0; i < ratingData.Length; i++)
        {
            ratingData[i] = $"{playerLogins[i]}: {PlayerPrefs.GetFloat(playerLogins[i])}";
        }

        gameObject.GetComponent<ScrollViewAdapter>().AddItems(ratingData);
    }

    private void Update()
    {
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