using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private ObstacleConfig config;
    public ObstacleConfig Config => config;

    private ObstacleSpawner spawner;

    public void SetSpawner(ObstacleSpawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject collisionObject)
    {
        if (collisionObject.CompareTag("Player"))
        {
            collisionObject.GetComponent<Health>()?.TakeDamage(config.Damage);
        }
        else if (collisionObject.CompareTag("DeSpawnTrigger"))
        {
            spawner.ReturnToPool(this.gameObject);
        }
    }
}
