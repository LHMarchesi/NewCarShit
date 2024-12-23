using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private bool canSpawn;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool setSpeed;
    [SerializeField] private float speed;
    [SerializeField] private bool randomSprites;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int poolSize = 10;
    private Queue<GameObject> obstaclePool = new Queue<GameObject>();
    [SerializeField] private bool initializeAsChild;
    private float timeSinceLastSpawn = 0;
    public static event Action<GameObject> OnObjectReturnToPool;


    private void Start()
    {
        InitializePool();
    }

    void Update()
    {
        if (canSpawn && timeSinceLastSpawn >= spawnRate)
        {
            SpawnObstacle();
            timeSinceLastSpawn = 0;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = null;
            if (initializeAsChild) // initialize as child
                obj = Instantiate(obstaclePrefab, transform);
            else
                obj = Instantiate(obstaclePrefab);

            obj.SetActive(false);
            obstaclePool.Enqueue(obj);
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePool.Count == 0) return;

        GameObject obj = obstaclePool.Dequeue();

        obj.transform.position = GetRndPosition();
        ObstacleMovement obstacleMovement = obj.GetComponent<ObstacleMovement>();

        if (setSpeed)
        {
            obstacleMovement.SetSpeed(speed);
        }

        if (randomSprites)
        {
            SetRndSprite(obj);
        }

        if (obj.CompareTag("Enemy")) // Set spawner dependency
        {
            EnemyController enemyController = obj.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.SetSpawner(this);
                spawnRate = enemyController.enemyConfig.SpawnRatio;
            }
        }

        obj.SetActive(true);
    }

    public void ReturnToPool(GameObject obj)
    {
        OnObjectReturnToPool?.Invoke(obj);
        obj.SetActive(false);
        obstaclePool.Enqueue(obj);
    }

    private Vector3 GetRndPosition()
    {
        int i = UnityEngine.Random.Range(0, spawnPositions.Length);
        return spawnPositions[i].position;
    }

    private void SetRndSprite(GameObject prefab)
    {
        int i = UnityEngine.Random.Range(0, sprites.Length);
        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[i];
    }
}
