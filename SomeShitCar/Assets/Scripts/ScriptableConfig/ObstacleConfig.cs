using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "ObstacleConfig", menuName = "Configs/Obstacle")]
public class ObstacleConfig : ScriptableObject
{
    public float Damage;
    public float Speed;
    public float SpawnRatio;
    public EventReference CrashSound;
}