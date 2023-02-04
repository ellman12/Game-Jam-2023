using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HealthBar.HB.Health < 100 && collision.name == "Player")
        {
            HealthBar.HB.GainHealth();
            Destroy(gameObject);
        }
    }
}
