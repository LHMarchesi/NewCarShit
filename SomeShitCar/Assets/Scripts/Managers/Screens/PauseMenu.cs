using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pauseCanvas;

    void Start()
    {
        pauseButton.onClick.AddListener(PauseGame);
    }

    public void PauseGame()
    {
        GameManager.Instance.SetGameState(GameManager.GameStates.Pause);

        pauseCanvas.SetActive(true);

        Button resumeButton = pauseCanvas.transform.Find("PausePanel/ResumeButton").GetComponent<Button>();

        Button mainMenuButton = pauseCanvas.transform.Find("PausePanel/MainMenuButton").GetComponent<Button>();

        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(SceneLoadManger.Instance.LoadMainMenu);

        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.SetGameState(GameManager.GameStates.Game);
    }
}
