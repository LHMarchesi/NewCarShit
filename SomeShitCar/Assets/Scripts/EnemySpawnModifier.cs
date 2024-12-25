using UnityEngine;
public class EnemySpawnModifier : IDifficultyModifier
{
    private float multiplier;

    public EnemySpawnModifier(float multiplier)
    {
        this.multiplier = multiplier;
    }

    public void ApplyDifficulty(LevelConfig levelConfig)
    {
        levelConfig.enemySpawnMultiplier *= multiplier;
        Debug.Log($"Spawn de enemigos aumentado a {levelConfig.enemySpawnMultiplier}");
    }
}