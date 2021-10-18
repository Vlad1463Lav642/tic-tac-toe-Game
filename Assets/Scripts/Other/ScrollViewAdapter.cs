﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    /// <param name="userNames">Массив значений.</param>
    public void AddItems(string[] userNames)
    {
        ClearItems();

        for(int i = 0; i < userNames.Length; i++)
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
        for (int i = 0; i < itemsList.Count; i++)
        {
            Destroy(itemsList[i]);
        }
        itemsList.Clear();
    }
}