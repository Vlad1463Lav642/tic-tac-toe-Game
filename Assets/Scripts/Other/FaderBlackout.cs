using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Скрипт контролирующий затемнение экрана.
/// </summary>
public class FaderBlackout : MonoBehaviour
{
    #region Параметры
    [SerializeField] private GameObject fader;
    [SerializeField] private Text faderText;
    [SerializeField] [Range(0f, 1f)] float lerpTime;
    #endregion

    public void FaderStart(float speed, float step, string score)
    {
        fader.SetActive(true);
        StartCoroutine(FaderDark(speed, score));
    }

    IEnumerator FaderDark(float speed, string score)
    {
        var color = fader.GetComponentInChildren<Image>().color;
        faderText.gameObject.SetActive(true);

        for (float f = 0f; f < 1f; f += speed * Time.deltaTime)
        {
            color = Color.Lerp(color, Color.black, f);
            fader.GetComponentInChildren<Image>().color = color;

            faderText.text = score;

            yield return new WaitForSeconds(0.05f);
        }

        fader.SetActive(false);
    }
}