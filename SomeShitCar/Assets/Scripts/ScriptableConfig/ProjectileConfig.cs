using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/Projectile")]
public class ProjectileConfig : ScriptableObject
{
    public float Damage;
    public float speed;
    public float lifeTime;
    public EventReference shootSound;
 //   public EventReference impactSound;
}
