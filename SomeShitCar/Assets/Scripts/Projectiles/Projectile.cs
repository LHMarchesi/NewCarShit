using UnityEngine;

public class Projectile : BaseProjectile
{
    protected override void OnEnable()
    {
        switch (transform.root.tag)
        {
            case "Player":
                gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
                break;
            case "Enemy":
                gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
                break;
            default:
                gameObject.layer = LayerMask.NameToLayer("DefaultProjectile");
                break;
        }
    }
    protected override void Move()
    {
        base.Move();
    }
}
