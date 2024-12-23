using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig weaponConfig;

    [SerializeField] protected string targetTag;
    [SerializeField] protected bool drawGizmos;

    protected GameObject bulletPrefab;
    protected int capacity;
    protected float reloadTime;
    protected float timeBetweenBullets;
    protected float visionRange;
    protected float currentAmmo;
    protected float timeSinceLastShot;
    protected bool isReloading;

    [SerializeField] private Slider reloadSlider;

    protected virtual void Start()
    {
        bulletPrefab = weaponConfig.bulletPrefab;
        reloadTime = weaponConfig.reloadTime;
        timeBetweenBullets = weaponConfig.timeBetweenBullets;
        capacity = weaponConfig.capacity;
        visionRange = weaponConfig.visionRange;

        currentAmmo = capacity;
        timeSinceLastShot = 0;
    }

    protected virtual void Update()
    {
        if (isReloading)
            return;

        if (timeSinceLastShot < timeBetweenBullets)
        {
            timeSinceLastShot += Time.deltaTime;
        }

        if (GetCloserTarget())
        {
            Shoot();
        }
    }

    protected abstract void Shoot();

    protected virtual IEnumerator Reload()
    {
        isReloading = true;
        float elapsedTime = 0;

        reloadSlider.maxValue = reloadTime;
        reloadSlider.value = 0f;
        reloadSlider.gameObject.SetActive(true);

        while (elapsedTime < reloadTime && isReloading)
        {
            elapsedTime += Time.deltaTime;
            reloadSlider.value = Mathf.Clamp(elapsedTime, 0f, reloadTime);
            yield return null;
        }

        reloadSlider.gameObject.SetActive(false);
        reloadSlider.value = reloadTime;
        currentAmmo = capacity;
        isReloading = false;
    }

    protected virtual GameObject GetCloserTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject closestTarget = null;

        float closestSqrDist = float.MaxValue;
        float sqrVisionRange = visionRange * visionRange;

        foreach (GameObject target in targets)
        {
            float sqrDist = (transform.position - target.transform.position).sqrMagnitude;
            if (drawGizmos)
            {
                Debug.DrawLine(transform.position, target.transform.position, Color.green);
            }

            if (sqrDist < sqrVisionRange && sqrDist < closestSqrDist)
            {
                closestSqrDist = sqrDist;
                closestTarget = target;
            }
        }
        return closestTarget;
    }

    protected virtual void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, visionRange);
        }
    }
}
