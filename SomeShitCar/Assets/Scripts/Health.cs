using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float startingHealth;
    public event Action OnDead;

    private float currentHealth;
    private Slider healthSlider;

    private void OnEnable()
    {
       
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth; // Limitar a la salud máxima
        }
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
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

