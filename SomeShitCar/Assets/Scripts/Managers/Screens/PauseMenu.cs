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
        pauseCanvas.SetActive(true);
        GameManager.Instance.SetGameState(GameManager.GameStates.Pause);

        Button resumeButton = pauseCanvas.transform.Find("PausePanel/ResumeButton").GetComponent<Button>();
        Button mainMenuButton = pauseCanvas.transform.Find("PausePanel/MainMenuButton").GetComponent<Button>();

        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMenu);

        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        GameManager.Instance.SetGameState(GameManager.GameStates.Game);
        Time.timeScale = 1f;
    } 
    
    private void GoToMenu()
    {
        GameManager.Instance.SetGameState(GameManager.GameStates.MainMenu);
        SceneLoadManger.Instance.LoadMainMenu();
    }
}
