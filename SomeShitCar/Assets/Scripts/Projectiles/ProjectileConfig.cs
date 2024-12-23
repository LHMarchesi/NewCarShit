using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Projectiles/Projectile Config")]
public class ProjectileConfig : ScriptableObject
{
    public float damage;
    public float speed;
    public float lifeTime;
}
