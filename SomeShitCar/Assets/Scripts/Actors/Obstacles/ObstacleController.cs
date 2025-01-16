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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject other)
    {
        IDamagable iDamagable = other.gameObject.GetComponent<IDamagable>();
        if (iDamagable != null)
        {
            iDamagable.Damage(config.Damage);
            AudioManager.Instance.PlaySfx(config.CrashSound);
        }
    }
    public void HandleDespawn()
    {
        spawner.ReturnToPool(gameObject);
    }
}
