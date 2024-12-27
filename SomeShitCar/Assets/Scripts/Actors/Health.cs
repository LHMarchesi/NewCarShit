using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float startingHealth;
    public static event Action OnDead;
    public static event Action OnTakeDamage;

    private float currentHealth;
    private Slider healthSlider;

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        healthSlider.value = currentHealth;

        if (currentHealth > startingHealth)
            currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        OnTakeDamage.Invoke();
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            OnDead.Invoke();
        }
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
}

