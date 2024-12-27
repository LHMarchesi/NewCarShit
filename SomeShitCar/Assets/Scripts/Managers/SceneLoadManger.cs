using System.Collections;
using System.Collections.Generic;
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

    public void LoadMainMenu()
    {

        StartCoroutine(LoadTransition("MainMenu"));
    }

    public void LoadGame()
    {
        StartCoroutine(LoadTransition("Game"));
    }

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadTransition(sceneName));
    }

    private IEnumerator LoadTransition(string sceneName)
    {
        animator.SetTrigger("StartTransition");
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

}
