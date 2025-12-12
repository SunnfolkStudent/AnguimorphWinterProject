using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }
    private int currentHealth
    {
        get
        {
           
            return (int)slider.value;
        }
        set { slider.value = value; }
    }

    
    
}
