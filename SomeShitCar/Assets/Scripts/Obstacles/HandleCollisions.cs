using System;
using UnityEngine;

public class HandleCollisions : MonoBehaviour
{
    public event Action OnColision;
    
    [SerializeField] bool isTrigger;
    [SerializeField] float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger)
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTrigger)
        {
            collision.gameObject.GetComponent<Health>()?.TakeDamage(damage);
        }
    }
}


