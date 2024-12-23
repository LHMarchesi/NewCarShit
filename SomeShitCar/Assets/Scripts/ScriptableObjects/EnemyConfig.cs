using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy")]
public class EnemyConfig : ScriptableObject
{
    public float Health;
    public float CollisionDamage;
    public float Armor;
    public float Speed;
    public float SpawnRatio;
}
