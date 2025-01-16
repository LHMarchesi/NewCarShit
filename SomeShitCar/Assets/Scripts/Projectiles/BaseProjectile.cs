using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileConfig projectileConfig;

    protected Vector2 startPosition;

    protected virtual void OnEnable()
    {
        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        Move();
        ReturnToPoolOnDistance();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.up * projectileConfig.speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
        if (iDamagable != null)
            iDamagable.Damage(projectileConfig.Damage);

        BaseWeapon weapon = GetComponentInParent<BaseWeapon>();
        weapon.ReturnToPool(gameObject);
    }

    protected void ReturnToPoolOnDistance()
    {
        float maxDist = 10f;

        float distance = Vector2.Distance(startPosition, transform.position);

        // Si la distancia supera el máximo, devolvemos el proyectil al pool
        if (distance > maxDist)
        {
            BaseWeapon weapon = GetComponentInParent<BaseWeapon>();
            weapon.ReturnToPool(gameObject);
        }
    }
}
