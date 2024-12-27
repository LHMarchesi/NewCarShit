using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void Start()
    {
        SetGameState(GameStates.MainMenu);
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
    }

    private void HandleStateChange()
    {
        switch (currentState)
        {
            case GameStates.MainMenu:
                
                break;
            case GameStates.Game:

                break;
            case GameStates.Pause:
               
                break;
            case GameStates.Lose:
                OnLose?.Invoke();
                break;
            case GameStates.Win:
                OnWin?.Invoke();
                break;
            default:
                break;
        }
    }
}
