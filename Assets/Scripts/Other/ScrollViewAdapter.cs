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
    [SerializeField] private GameObject scrollItem;

    private List<GameObject> itemsList;
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
            itemsList.Add(item);
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