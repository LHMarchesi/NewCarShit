using System.Collections;
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
        winCanvas.SetActive(true);  // Show Win Canvas

        Button nextButton = winCanvas.transform.Find("NextButton").GetComponent<Button>();
        Button mainMenuButton = winCanvas.transform.Find("MainMenuButton").GetComponent<Button>();
        Button quitButton = winCanvas.transform.Find("QuitButton").GetComponent<Button>();

        mainMenuButton.onClick.RemoveAllListeners(); 
        nextButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        nextButton.onClick.AddListener(GameManager.Instance.Retry);
        mainMenuButton.onClick.AddListener(SceneLoadManger.Instance.LoadMainMenu);
        quitButton.onClick.AddListener(GameManager.Instance.Quit);

        Time.timeScale = 0f;
    }

    private void toggleLoseCanvas()
    {
        StartCoroutine(ShowLoseCanvasWithDelay(1f)); // Calls with Delay
    }

    private IEnumerator ShowLoseCanvasWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 

        loseCanvas.SetActive(true); // Show Lose Canvas

        Button retryButton = loseCanvas.transform.Find("RetryButton").GetComponent<Button>();
        Button mainMenuButton = loseCanvas.transform.Find("MainMenuButton").GetComponent<Button>();
        Button quitButton = loseCanvas.transform.Find("QuitButton").GetComponent<Button>();

        mainMenuButton.onClick.RemoveAllListeners(); 
        retryButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        retryButton.onClick.AddListener(GameManager.Instance.Retry);
        mainMenuButton.onClick.AddListener(GoToMenu);
        quitButton.onClick.AddListener(GameManager.Instance.Quit);

        AudioManager.Instance.PauseAllAudio(true);

        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        GameManager.OnWin -= toggleWinCanvas;
        GameManager.OnLose -= toggleLoseCanvas;
    }

    private void GoToMenu()
    {
        GameManager.Instance.SetGameState(GameManager.GameStates.MainMenu);
        SceneLoadManger.Instance.LoadMainMenu();
    }
}
