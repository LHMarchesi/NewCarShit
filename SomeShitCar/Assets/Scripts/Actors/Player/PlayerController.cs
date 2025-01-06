using FMODUnity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private EventReference onHitSound;
    [SerializeField] private EventReference onDeadSound;
    private Health health;
    private CameraShake cameraShake;

    void OnEnable()
    {
        health = GetComponent<Health>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        health.OnDead += HandleDeath;
        health.OnTakeDamage += cameraShake.Shake;
        health.OnTakeDamage += DamageEffect;

        health.SetStartingHeal(startingHealth);
    }
    private void HandleDeath()
    {
        AudioManager.Instance.PlaySfx(onDeadSound);
        GameManager.Instance.SetGameState(GameManager.GameStates.Lose);
        gameObject.SetActive(false);
    }

    private void DamageEffect()
    {
        AudioManager.Instance.PlaySfx(onHitSound);
        StartCoroutine(UIManager.Instance.StartPanelEffect(Color.red));
    }

    private void OnDisable()
    {
        health.OnDead -= HandleDeath;
        health.OnTakeDamage -= cameraShake.Shake;
    }
}
