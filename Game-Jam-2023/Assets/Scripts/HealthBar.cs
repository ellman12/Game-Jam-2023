using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//https://youtu.be/BLfNP4Sc_iA
public class HealthBar : MonoBehaviour
{
    public static HealthBar HB { get; private set; }

    private void Awake()
    {
        if (HB != null && HB != this) Destroy(gameObject);
        else HB = this;
    }
    
    public Image fill;
    public Slider slider;
    public Gradient gradient;
    private bool cooldown;
    private int health, maxHealth;
    public int Health
    {
        get => health;
        private set
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
    public void TakeDamage(int dmg)
    {
        if (!cooldown)
        {
            Health -= dmg;
            StartCoroutine(DamageCooldown());
        }
    }
    public void GainHealth()
    {
        if (Health < maxHealth)
            Health += 50;
        if(Health > maxHealth)
            Health = maxHealth;
    }
    IEnumerator DamageCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(.5f);
        cooldown = false;
    }
}
