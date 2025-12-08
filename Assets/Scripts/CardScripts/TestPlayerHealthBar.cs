using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class TestPlayerHealthBar : NetworkBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider slider;

    void Start()
    {
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died");
    }
}
