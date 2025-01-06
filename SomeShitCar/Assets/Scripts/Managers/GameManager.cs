using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    { get { return instance; } }

    public enum GameStates { MainMenu, Game, Pause, Lose, Win }

    [SerializeField] private GameStates currentState;
    public static event Action OnWin;
    public static event Action OnLose;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void SetGameState(GameStates newState)
    {
        currentState = newState;
        HandleStateChange();
    }
    public void SetMenuState()
    {
        SceneLoadManger.Instance.LoadSceneByName("MainMenu");
        SetGameState(GameStates.MainMenu);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        SetGameState(GameStates.Game);
    }

    private void HandleStateChange()
    {
        switch (currentState)
        {
            case GameStates.MainMenu:
                AudioManager.Instance.PauseAllAudio(false);
                break;
            case GameStates.Game:
                AudioManager.Instance.PauseAllAudio(false);
                break;
            case GameStates.Pause:
                AudioManager.Instance.PauseAllAudio(true);
                break;
            case GameStates.Lose:
                OnLose?.Invoke();
                break;
            case GameStates.Win:
                AudioManager.Instance.PauseAllAudio(true);
                OnWin?.Invoke();
                break;
            default:
                break;
        }
    }
}
