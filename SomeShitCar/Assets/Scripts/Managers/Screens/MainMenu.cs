using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    void Start()
    {
        startButton.onClick.AddListener(SceneLoadManger.Instance.LoadGame);
        quitButton.onClick.AddListener(GameManager.Instance.Quit);
    }
}
