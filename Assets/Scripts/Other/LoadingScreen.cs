using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [Header("Экран загрузки")]
    [SerializeField] private GameObject loadingScreen;

    [Header("Полоска загрузки")]
    [SerializeField] private Slider loadingSlider;

    [SerializeField] private Text loadingText;

    private float waiting = 0.5f;

    private bool isStarted;

    public void LoadScene(int index)
    {
        StartCoroutine(AsyncLoadSimulation(index));
        //StartCoroutine(AsyncLoad(index));
    }

    IEnumerator AsyncLoadSimulation(int index)
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(waiting);
        SetLoadingProgress(0.2f);
        yield return new WaitForSeconds(waiting);
        SetLoadingProgress(0.4f);
        yield return new WaitForSeconds(waiting);
        SetLoadingProgress(0.8f);
        yield return new WaitForSeconds(waiting);
        SetLoadingProgress(1f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        yield return null;
    }

    IEnumerator AsyncLoad(int index)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;

            yield return null;
        }
    }

    public void SetLoadingText(string text)
    {
        loadingText.text = text;
    }

    public string GetLoadingText()
    {
        return loadingText.text;
    }

    public void SetLoadingProgress(float value)
    {
        loadingSlider.value = value;
    }

    public float GetLoadingProgress()
    {
        return loadingSlider.value;
    }
}