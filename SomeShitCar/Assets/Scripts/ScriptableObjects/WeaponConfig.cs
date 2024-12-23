using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Weapon")]

public class WeaponConfig : ScriptableObject
{
    public GameObject bulletPrefab;
    public float reloadTime;
    public float timeBetweenBullets;
    public int capacity;
    public float visionRange;
}
