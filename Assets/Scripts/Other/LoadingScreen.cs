using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Скрипт осуществляющий управление экраном загрузки.
/// </summary>
public class LoadingScreen : MonoBehaviour
{
    #region Параметры
    [Header("Экран загрузки")]
    [SerializeField] private GameObject loadingScreen;

    [Header("Полоска загрузки")]
    [SerializeField] private Slider loadingSlider;

    [SerializeField] private Text loadingText;

    private float waiting = 0.5f;
    #endregion

    public void LoadScene(int index)
    {
        StartCoroutine(AsyncLoadSimulation(index));
        //StartCoroutine(AsyncLoad(index));
    }

    /// <summary>
    /// Симуляция загрузки.
    /// </summary>
    /// <param name="index">ID сцены.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Обычная загрузка.
    /// </summary>
    /// <param name="index">ID сцены.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Устанавливает текст на экране загрузки.
    /// </summary>
    /// <param name="text">Текст загрузки.</param>
    public void SetLoadingText(string text)
    {
        loadingText.text = text;
    }

    /// <summary>
    /// Возвращает текст с экрана загрузки.
    /// </summary>
    /// <returns>Текст загрузки.</returns>
    public string GetLoadingText()
    {
        return loadingText.text;
    }

    /// <summary>
    /// Устанавливает значение полоски загрузки.
    /// </summary>
    /// <param name="value">Значение полоски</param>
    public void SetLoadingProgress(float value)
    {
        loadingSlider.value = value;
    }

    /// <summary>
    /// Возвращает значение полоски загрузки.
    /// </summary>
    /// <returns>Значение полоски.</returns>
    public float GetLoadingProgress()
    {
        return loadingSlider.value;
    }
}