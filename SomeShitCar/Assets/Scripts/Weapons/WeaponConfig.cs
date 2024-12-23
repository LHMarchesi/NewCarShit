using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Config", menuName = "Weapons/Weapon Config")]

public class WeaponConfig : ScriptableObject
{
    public GameObject bulletPrefab;
    public float reloadTime;
    public float timeBetweenBullets;
    public int capacity;
    public float visionRange;
}
