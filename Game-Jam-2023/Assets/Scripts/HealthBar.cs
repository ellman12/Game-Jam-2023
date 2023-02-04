using UnityEngine;
using UnityEngine.UI;

//https://youtu.be/BLfNP4Sc_iA
public class HealthBar : MonoBehaviour
{
    public Image fill;
    public Slider slider;
    public Gradient gradient;

    private int health, maxHealth;
    public int Health
    {
        get => health;
        set
        {
            health = value;
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

    private void Start()
    {
        SetMaxHealth(100);
    }

    private void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = Health = newMaxHealth;
        slider.maxValue = newMaxHealth;
    }
}
