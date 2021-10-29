using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Скрипт осуществляющий управление списком.
/// </summary>
public class ScrollViewAdapter : MonoBehaviour
{
    #region Параметры
    [SerializeField] private Transform scrollContent;

    [Header("Prefab элемента игрока списке")]
    [SerializeField] private GameObject scrollItem;

    private List<GameObject> itemsList;

    [Header("Спрайт для выделения выбранного игрока в списке")]
    [SerializeField] private Sprite selectedPlayerSprite;

    private GameObject previousPlayer;

    #endregion

    private void Awake()
    {
        itemsList = new List<GameObject>();
    }

    /// <summary>
    /// Добавляет полученные значения в список.
    /// </summary>
    /// <param name="userNames">Список значений.</param>
    public void AddItems(List<string> userNames)
    {
        ClearItems();

        for(int i = 0; i < userNames.Count; i++)
        {
            GameObject item = Instantiate(scrollItem);
            item.transform.SetParent(scrollContent, false);

            item.GetComponentInChildren<Text>().text = userNames[i].ToString();

            SelectCurrentPlayerIntoList(item);

            itemsList.Add(item);
        }
    }

    /// <summary>
    /// Выделяет текущего игрока в списке.
    /// </summary>
    /// <param name="listItem">Элемент игрока из списка.</param>
    public void SelectCurrentPlayerIntoList(GameObject listItem)
    {
        var currentPlayer = PlayerPrefs.GetString("CurrentPlayerName");

        if (currentPlayer == listItem.GetComponentInChildren<Text>().text)
        {
            if(previousPlayer != null)
                previousPlayer.GetComponent<Image>().sprite = listItem.GetComponent<Image>().sprite;


            listItem.GetComponent<Image>().sprite = selectedPlayerSprite;
            previousPlayer = listItem;
        }
    }

    /// <summary>
    /// Возвращает список значений.
    /// </summary>
    /// <returns>Список значений.</returns>
    public List<GameObject> GetItems()
    {
        return itemsList;
    }

    /// <summary>
    /// Очищает список.
    /// </summary>
    private void ClearItems()
    {
        itemsList.Select(item => { Destroy(item); return item;}).Count();

        itemsList.Clear();
    }
}