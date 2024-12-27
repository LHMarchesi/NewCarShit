using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/Projectile")]
public class ProjectileConfig : ScriptableObject
{
    public float damage;
    public float speed;
    public float lifeTime;
}
