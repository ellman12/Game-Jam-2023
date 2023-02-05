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

    [SerializeField] private Image fill;
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private GameObject player, gameOver;
    [SerializeField] private AudioSource hurt, death;
    private bool cooldown;
    private int health, maxHealth;
    public int Health
    {
        get => health;
        private set
        {
            if (player == null) return;
            
            health = value;
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);

            if (value <= 0)
            {
                Time.timeScale = 0;
                player.SetActive(false);
                gameOver.SetActive(true);
            }
        }
    }

    private void Start()
    {
        SetMaxHealth(100);
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = Health = newMaxHealth;
        slider.maxValue = newMaxHealth;
    }
    
    public void TakeDamage(int dmg)
    {
        if (!cooldown)
        {
            Health -= dmg;
            if(health > 0)
            {
                hurt.Play();
            } else
            {
                death.Play();
            }
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

    private IEnumerator DamageCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(.5f);
        cooldown = false;
    }
}
