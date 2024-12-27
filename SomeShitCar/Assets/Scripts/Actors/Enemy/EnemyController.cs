using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    public EnemyConfig Config => config;

    private Health health;
    private ObstacleSpawner spawner;


    void OnEnable()
    {
        Health.OnDead += HandleDeath;
        health = GetComponent<Health>();
        health.SetStartingHeal(config.Health);
    }

    public void SetSpawner(ObstacleSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void HandleDeath()
    {
        if (spawner != null)
            spawner.ReturnToPool(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(config.CollisionDamage);
        }
        else if (collision.collider.CompareTag("DeSpawnTrigger"))
        {
            // Si el obstáculo sale de la pantalla, regresarlo al pool
            spawner.ReturnToPool(this.gameObject);
        }
    }

    void OnDisable()
    {
        Health.OnDead -= HandleDeath;
    }

}
