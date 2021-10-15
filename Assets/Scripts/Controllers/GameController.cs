using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void PauseButton()
    {
        //Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
}