using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewAdapter : MonoBehaviour
{
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject scrollItem;

    private List<GameObject> itemsList;

    private void Awake()
    {
        itemsList = new List<GameObject>();
    }

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

    public List<GameObject> GetItems()
    {
        return itemsList;
    }

    private void ClearItems()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            Destroy(itemsList[i]);
        }
        itemsList.Clear();
    }
}