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
        Health objHealth = collisionObject.GetComponent<Health>();
        if (objHealth != null)
        {
            objHealth?.TakeDamage(config.Damage);
            AudioManager.Instance.PlaySfx(config.CrashSound);

        }
        else if (collisionObject.CompareTag("DeSpawnTrigger"))
        {
            spawner.ReturnToPool(this.gameObject);
        }
    }
}
