using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    void Start()
    {
        startButton.onClick.AddListener(PressStart);
        quitButton.onClick.AddListener(GameManager.Instance.Quit);
    }

    private void PressStart()
    {
        GameManager.Instance.SetGameState(GameManager.GameStates.Game);
    }
}
