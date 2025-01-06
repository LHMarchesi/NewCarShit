using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileConfig projectileConfig;

    protected Vector2 startPosition;

    private bool hasSpawned;

    protected virtual void OnEnable()
    {
        startPosition = transform.position;
        PlaySfxOnSpawn();
    }

    protected virtual void Update()
    {
        Move();
        ReturnToPoolOnDistance();
    }

    private void PlaySfxOnSpawn()
    {
        if (!hasSpawned) 
        {
            AudioManager.Instance.PlaySfx(projectileConfig.shootSound);
            hasSpawned = true; 
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.up * projectileConfig.speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Health>()?.TakeDamage(projectileConfig.Damage);

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
