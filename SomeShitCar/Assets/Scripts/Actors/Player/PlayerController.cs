using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private Health health;

    void OnEnable()
    {
        health = GetComponent<Health>();

        Health.OnDead += HandleDeath;

        health.SetStartingHeal(startingHealth);
    }
    private void HandleDeath()
    {
        GameManager.Instance.SetGameState(GameManager.GameStates.Lose);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Health.OnDead -= HandleDeath;
    }
}
