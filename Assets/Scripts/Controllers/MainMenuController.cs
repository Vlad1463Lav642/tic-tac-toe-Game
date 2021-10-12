using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private List<GameObject> windowPanels;

    public void PlayGame()
    {

    }

    public void Settings()
    {
        Instantiate(windowPanels[1], transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(windowPanels[0],transform);
        }
    }
}