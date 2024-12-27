using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    private GameObject pauseCanvas;

    void Start()
    {
        pauseButton.onClick.AddListener(PauseGame);
    }

    public void PauseGame()
    {
        GameManager.Instance.SetGameState(GameStates.Pause);

        pauseCanvas = transform.GetChild(0).gameObject;
        pauseCanvas.SetActive(true);

        Button resumeButton = pauseCanvas.transform.Find("ResumeButton").GetComponent<Button>();
        Button mainMenuButton = pauseCanvas.transform.Find("MainMenu").GetComponent<Button>();
        Button quitButton = pauseCanvas.transform.Find("Quit").GetComponent<Button>();

        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(SceneLoadManger.Instance.LoadMainMenu);
        quitButton.onClick.AddListener(GameManager.Instance.Quit);

        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.SetGameState(GameStates.Game);
    }

    
}
