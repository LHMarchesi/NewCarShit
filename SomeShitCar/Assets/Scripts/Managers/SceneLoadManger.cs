using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManger : MonoBehaviour
{
    private static SceneLoadManger instance;
    public static SceneLoadManger Instance
    { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] private float transitionTime;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public IEnumerator TransitionToScene(string sceneName, GameManager.GameStates newState)
    {
        Time.timeScale = 1.0f;

        // Fade in / out master audio
        yield return StartCoroutine(AudioManager.Instance.FadeOut(1.0f));

        AudioManager.Instance.PauseAllAudio(true);
        animator.SetTrigger("StartTransition");

        SceneManager.LoadScene(sceneName);
        GameManager.Instance.SetGameState(newState);

        yield return StartCoroutine(AudioManager.Instance.FadeIn(1.0f));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(TransitionToScene("MainMenu", GameManager.GameStates.MainMenu));
    }

    public void LoadGame()
    {
        StartCoroutine(TransitionToScene("Game", GameManager.GameStates.Game));
    }

    public void Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        StartCoroutine(TransitionToScene(currentScene.name, GameManager.GameStates.Game));
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
