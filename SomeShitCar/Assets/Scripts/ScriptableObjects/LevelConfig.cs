using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level")]
public class LevelConfig : ScriptableObject
{
    public float timer;
    public float playerSpeedMultiplier;
    public float obstacleSpeedMultiplier;
    public float enemySpawnMultiplier;
    public float obstacleSpawnMultiplier;
    public AnimationClip startAnimation;
}
