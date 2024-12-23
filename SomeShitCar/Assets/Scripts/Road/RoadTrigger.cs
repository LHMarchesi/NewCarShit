using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    private enum TriggerType { Spawn, Despawn }
    [SerializeField] private TriggerType triggerType;
    private RoadManager roadManager;

    private void Awake()
    {
        roadManager = GetComponentInParent<RoadManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Spawn)
            {
                roadManager.SpawnRoad();
                gameObject.SetActive(false);
            }
            else if (triggerType == TriggerType.Despawn)
            {
                roadManager.DespawnRoad();
                gameObject.SetActive(false);
            }
        }

        if (collision.CompareTag("Obstacle"))
        {
            GameObject obj = collision.gameObject;
            ObstacleSpawner obstacleSpawner = obj.GetComponentInParent<ObstacleSpawner>();
            obstacleSpawner.ReturnToPool(obj);
        }
    }
}
