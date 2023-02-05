using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField]
    private AudioSource health;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (HealthBar.HB.Health < 100 && collision.name == "Player")
        {
            StartCoroutine(Heal());

            
        }
    }

    IEnumerator Heal()
    {
        HealthBar.HB.GainHealth();
        health.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
