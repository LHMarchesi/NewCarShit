using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleConfig", menuName = "Configs/Obstacle")]
public class ObstacleConfig : ScriptableObject
{
    public float Damage;
    public float Speed;
    public float SpawnRatio;
}