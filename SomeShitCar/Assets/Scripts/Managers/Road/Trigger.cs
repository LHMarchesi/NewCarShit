using UnityEngine;

public class Trigger : MonoBehaviour
{
    private enum TriggerType { RoadSpawner, RoadDespawner, ObstacleDespawner }
    [SerializeField] private TriggerType triggerType;
    private RoadManager roadManager;

    private void Awake()
    {
        roadManager = GetComponentInParent<RoadManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (triggerType)
        {
            case TriggerType.RoadSpawner:
                HandleRoadSpawn(collision);
                break;

            case TriggerType.RoadDespawner:
                HandleRoadDespawn(collision);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggerType == TriggerType.ObstacleDespawner)
        {
            HandleObstacleDespawn(collision.collider);
        }
    }

    private void HandleRoadSpawn(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roadManager.SpawnRoad();
            gameObject.SetActive(false); // Desactiva el trigger después de usarlo
        }
    }

    private void HandleRoadDespawn(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            roadManager.DespawnRoad();
            gameObject.SetActive(false); // Desactiva el trigger después de usarlo
        }
    }

    private void HandleObstacleDespawn(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            ObstacleController obstacleController = collision.GetComponent<ObstacleController>();

            if (obstacleController != null)
            {
                obstacleController.HandleDespawn();
            }
        }
    }
}
