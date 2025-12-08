using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private int maxHealth;

    private int currentHealth
    {

        get
        {
           
            return (int)slider.value;
        }
        set { slider.value = value; }
    }

    public void changeHealth(int amount)
    {
        currentHealth += amount;
    }
    
    
}
