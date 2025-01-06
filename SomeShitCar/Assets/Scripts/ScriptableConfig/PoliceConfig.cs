using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Police")]
public class PoliceConfig : ScriptableObject
{
    public float Health;
    public float CollisionDamage;
    public float Armor;
    public float Speed;
    public float SpawnRatio;
    public EventReference DamageSound;
    public EventReference DestroySound;
    public EventReference SirenSound;
    public EventReference WhoopWhopSound;
}
