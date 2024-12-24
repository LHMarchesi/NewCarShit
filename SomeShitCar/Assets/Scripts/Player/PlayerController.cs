using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Health;
    private Health health;

    void OnEnable()
    {
        health = GetComponent<Health>();

        health.OnDead += HandleDeath;

        health.SetStartingHeal(Health);
    }
    private void HandleDeath()
    {
        GameManager.Instance.SetGameState(GameManager.GameStates.Lose);
        gameObject.SetActive(false);
    }
}
