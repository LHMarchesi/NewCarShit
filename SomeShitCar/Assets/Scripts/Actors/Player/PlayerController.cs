using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private Health health;
    private CameraShake cameraShake;

    void OnEnable()
    {
        health = GetComponent<Health>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        health.OnDead += HandleDeath;
        health.OnTakeDamage += cameraShake.Shake;

        health.SetStartingHeal(startingHealth);
    }
    private void HandleDeath()
    {
        GameManager.Instance.SetGameState(GameManager.GameStates.Lose);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        health.OnDead -= HandleDeath;
        health.OnTakeDamage -= cameraShake.Shake;
    }
}
