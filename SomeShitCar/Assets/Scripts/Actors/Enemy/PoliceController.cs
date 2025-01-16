using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PoliceController : MonoBehaviour
{
    [SerializeField] private PoliceConfig config;
    public PoliceConfig Config => config;

    private Health health;
    private Animator animator;
    private ObstacleSpawner spawner;

    private bool hasSpawned;
    private bool isActive;

    private EventInstance sirenInstance;

    void OnEnable()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();

        health.OnDead += HandleDeath;
        health.OnTakeDamage += DamageTrigger;
        health.SetStartingHeal(config.Health);


        sirenInstance = RuntimeManager.CreateInstance(config.SirenSound);

    }

    private void Update()
    {
        PlaySfxOnSpawn();
    }

    private void PlaySfxOnSpawn()
    {
        if (!isActive)
        {
            if (hasSpawned)
            {
                sirenInstance.start();
                AudioManager.Instance.PlaySfx(config.WhoopWhopSound);
            }
            isActive = true; // Evitar que se reproduzca repetidamente
        }
    }

    public void SetSpawner(ObstacleSpawner spawner)
    {
        this.spawner = spawner;
        hasSpawned = true;
    }

    private void HandleDeath()
    {
        AudioManager.Instance.PlaySfx(config.DestroySound);

        if (spawner != null)
            spawner.ReturnToPool(this.gameObject);
    }

    private void DamageTrigger()
    {
        AudioManager.Instance.PlaySfx(config.DamageSound);
        animator.SetTrigger("Damage");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
        if (iDamagable != null)
            iDamagable.Damage(config.CollisionDamage);
    }

    void OnDisable()
    {
        health.OnDead -= HandleDeath;
        health.OnTakeDamage -= DamageTrigger;

        if (sirenInstance.isValid())
        {
            sirenInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            sirenInstance.release();
        }
    }

}
