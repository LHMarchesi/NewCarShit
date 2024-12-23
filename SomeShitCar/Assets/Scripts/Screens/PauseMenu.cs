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

        mainMenuButton.onClick.RemoveAllListeners(); // Limpiar listeners anteriores
        resumeButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GameManager.Instance.SetMenuState);
        quitButton.onClick.AddListener(GameManager.Instance.Quit);

        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        GameManager.Instance.SetGameState(GameStates.Game);
        Time.timeScale = 1f;
    }
}
