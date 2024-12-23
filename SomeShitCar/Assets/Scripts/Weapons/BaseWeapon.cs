using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : Weapon
{
    private Queue<GameObject> bulletPool = new Queue<GameObject>();

    protected override void Start()
    {
        base.Start();
        InitializePool();
    }

    protected override void Shoot()
    {
        if (currentAmmo > 0 && timeSinceLastShot >= timeBetweenBullets)
        {
            GameObject target = GetCloserTarget();
            if (target != null)
            {
                Vector2 direction = (target.transform.position - transform.position).normalized;

                GameObject bullet = GetFromPool();
                if (bullet != null)
                    bullet.transform.up = direction;
            }
            currentAmmo--;
            timeSinceLastShot = 0;
        }
        else if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, transform);
            obj.SetActive(false);
            bulletPool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        if (bulletPool.Count == 0 || isReloading) return null;

        GameObject obj = bulletPool.Dequeue();
        obj.transform.position = transform.position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        bulletPool.Enqueue(obj);
    }


}
