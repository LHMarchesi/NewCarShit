using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private bool canSpawn;
    [SerializeField] private bool randomSprites;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float startingSpawnRate;
    [SerializeField] private int poolSize;
    [SerializeField] private bool initializeAsChild;

    private Queue<GameObject> obstaclePool = new Queue<GameObject>();
    private float timeSinceLastSpawn = 0;


    private void Start()
    {
        InitializePool();
    }

    void Update()
    {
        if (canSpawn && timeSinceLastSpawn >= startingSpawnRate)
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
            GameObject obj = initializeAsChild ? Instantiate(obstaclePrefab, transform) : Instantiate(obstaclePrefab);
            obj.SetActive(false);
            obstaclePool.Enqueue(obj);
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePool.Count == 0) return;

        GameObject obj = obstaclePool.Dequeue();
        if (obj.activeSelf) return;

        obj.transform.position = GetRndPosition();

        if (obj.CompareTag("Obstacle")) 
        {
            ObstacleController obstacleController = obj.GetComponent<ObstacleController>();
            obstacleController.SetSpawner(this); // Set spawner dependency

            ObstacleMovement obstacleMovement = obj.GetComponent<ObstacleMovement>();
            obstacleMovement.SetSpeed(obstacleController.Config.Speed * DifficultyManager.Instance.GetObstacleSpeedMultiplier()); // Set Speed from config
            startingSpawnRate = obstacleController.Config.SpawnRatio / DifficultyManager.Instance.GetObstacleSpawnMultiplier(); // Set SpawnRatio from config
        }

        if (obj.CompareTag("Enemy")) 
        {
            EnemyController enemyController = obj.GetComponent<EnemyController>();
            Debug.Log("spawn enemy");
            if (enemyController != null)
            {
                enemyController.SetSpawner(this); 
                startingSpawnRate = enemyController.Config.SpawnRatio / DifficultyManager.Instance.GetEnemySpawnRateMultiplier();
            }
        }

        if (randomSprites)
            SetRndSprite(obj);

        obj.SetActive(true);
    }

    public void ReturnToPool(GameObject obj)
    {
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
        if (sprites.Length == 0) return;

        int i = UnityEngine.Random.Range(0, sprites.Length);
        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[i];
    }
}
