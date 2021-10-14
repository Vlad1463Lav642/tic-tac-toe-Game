using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{
    private void Start()
    {
        var loading = gameObject.GetComponent<LoadingScreen>();

        loading.LoadScene(1);
        loading.SetLoadingText("Запуск приложения!");
    }
}