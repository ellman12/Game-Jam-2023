using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField]
    private int damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
            HealthBar.HB.TakeDamage(damage);
        if (collision.name == "Attack")
            Destroy(gameObject);
    }
}
