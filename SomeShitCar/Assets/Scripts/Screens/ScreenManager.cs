using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject loseCanvas;

    private void OnEnable()
    {
        winCanvas = transform.GetChild(0).gameObject;
        loseCanvas = transform.GetChild(1).gameObject;

        GameManager.OnWin += toggleWinCanvas;
        GameManager.OnLose += toggleLoseCanvas;
    }
    void Start()
    {
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }

    private void toggleWinCanvas()
    {
        winCanvas.SetActive(true);

        Button retryButton = winCanvas.transform.Find("RetryButton").GetComponent<Button>();
        Button mainMenuButton = winCanvas.transform.Find("MainMenuButton").GetComponent<Button>();
        Button quitButton = winCanvas.transform.Find("QuitButton").GetComponent<Button>();

        mainMenuButton.onClick.RemoveAllListeners(); // Limpiar listeners anteriores
        retryButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        retryButton.onClick.AddListener(GameManager.Instance.Retry);
        mainMenuButton.onClick.AddListener(GameManager.Instance.SetMenuState);
        quitButton.onClick.AddListener(GameManager.Instance.Quit);

        Time.timeScale = 0f;
    }

    private void toggleLoseCanvas()
    {
        loseCanvas.SetActive(true);

        Button retryButton = loseCanvas.transform.Find("RetryButton").GetComponent<Button>();
        Button mainMenuButton = loseCanvas.transform.Find("MainMenuButton").GetComponent<Button>();
        Button quitButton = loseCanvas.transform.Find("QuitButton").GetComponent<Button>();

        mainMenuButton.onClick.RemoveAllListeners(); // Limpiar listeners anteriores
        retryButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        retryButton.onClick.AddListener(GameManager.Instance.Retry);
        mainMenuButton.onClick.AddListener(GameManager.Instance.SetMenuState);
        quitButton.onClick.AddListener(GameManager.Instance.Quit);

        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        GameManager.OnWin -= toggleWinCanvas;
        GameManager.OnLose -= toggleLoseCanvas;
    }
}
