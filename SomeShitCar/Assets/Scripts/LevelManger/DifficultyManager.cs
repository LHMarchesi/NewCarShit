using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    private float enemySpawnMultiplier = 1; // Affects enemy spawn rate
    private float obstacleSpawnMultiplier = 1; // Affects obstacle spawn rate
    private float obstacleSpeedMultiplier = 1; // Affects obstacle speed
    private float playerSpeedMultiplier = 1; // Affects player speed

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetEnemySpawnRateMultiplier() => enemySpawnMultiplier;
    public float GetObstacleSpawnMultiplier() => obstacleSpawnMultiplier;
    public float GetObstacleSpeedMultiplier() => obstacleSpeedMultiplier;
    public float GetPlayerSpeedMultiplier() => playerSpeedMultiplier;

    public void AdjustDifficulty(float enemySpawnMultiplier, float obstacleSpawnMultiplier, float obstacleSpeedMultiplier, float playerSpeedMultiplier)
    {
        this.enemySpawnMultiplier *= enemySpawnMultiplier;
        this.enemySpawnMultiplier = Mathf.Clamp(this.enemySpawnMultiplier, 0.5f, 4f);

        this.obstacleSpawnMultiplier *= obstacleSpawnMultiplier;
        this.obstacleSpawnMultiplier = Mathf.Clamp(this.obstacleSpawnMultiplier, 0.5f, 4f);

        this.obstacleSpeedMultiplier *= obstacleSpeedMultiplier;
        this.obstacleSpeedMultiplier = Mathf.Clamp(this.obstacleSpeedMultiplier, 0.5f, 4f);

        this.playerSpeedMultiplier *= playerSpeedMultiplier;
        this.playerSpeedMultiplier = Mathf.Clamp(this.playerSpeedMultiplier, 0.5f, 2f);
    }
}
