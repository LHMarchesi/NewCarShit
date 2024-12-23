using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyConfig enemyConfig;

    private Health health;
    private ObstacleSpawner spawner;


    void OnEnable()
    {
        health = GetComponent<Health>();
        health.OnDead += HandleDeath;
        health.SetStartingHeal(enemyConfig.Health);
    }

    void OnDisable()
    {
        health.OnDead -= HandleDeath;
    }

    public void SetSpawner(ObstacleSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void HandleDeath()
    {
        if (spawner != null)
        {
            spawner.ReturnToPool(gameObject);
        }
    }

}
