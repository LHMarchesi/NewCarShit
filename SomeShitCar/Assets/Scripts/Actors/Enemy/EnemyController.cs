using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    public EnemyConfig Config => config;

    private Health health;
    private Animator animator;
    private ObstacleSpawner spawner;


    void OnEnable()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();

        health.OnDead += HandleDeath;
        health.OnTakeDamage += DamageTrigger;
        health.SetStartingHeal(config.Health);
    }

    public void SetSpawner(ObstacleSpawner spawner)
    {
        this.spawner = spawner;
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
        Health objHealth = collision.gameObject.GetComponent<Health>();
        if (objHealth != null)
        {
            objHealth?.TakeDamage(config.CollisionDamage);

        }
    }

    void OnDisable()
    {
        health.OnDead -= HandleDeath;
        health.OnTakeDamage -= DamageTrigger;
    }

}
