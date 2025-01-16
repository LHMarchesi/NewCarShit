using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamagable
{
    private float startingHealth;
    public event Action OnDead;
    public event Action OnTakeDamage;

    private float currentHealth;
    private Slider healthSlider;

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        healthSlider.value = currentHealth;

        if (currentHealth > startingHealth)
            currentHealth = startingHealth;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetStartingHeal(float hp)
    {
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = hp;
        healthSlider.value = hp;
        currentHealth = hp;
    }

    public void Damage(float damageAmount)
    {
        OnTakeDamage?.Invoke();
        currentHealth -= damageAmount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            OnDead?.Invoke();
        }
    }
}

