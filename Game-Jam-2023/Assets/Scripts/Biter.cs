using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biter : MonoBehaviour
{
    [SerializeField]
    private Transform Target;

    [SerializeField]
    private GameObject AttackSpace;

    private Ray2D Eyeline;
    private RaycastHit2D hit;

    [SerializeField]
    private float AttackRange;
    private bool canAttack;
    [SerializeField]
    private float cooldown = 1f;
    [SerializeField]
    private int reachDamage = 45;

    private void Update()
    {
        //if the player is AttackRange or less than AttackRange away, instantiate attack
        Eyeline.origin = transform.position;
        Eyeline.direction = Target.position - transform.position;
        Debug.DrawRay(Eyeline.origin, Eyeline.direction.normalized * AttackRange, Color.red);

        hit = Physics2D.Raycast(Eyeline.origin, Eyeline.direction.normalized * AttackRange, AttackRange);


        if ( hit.collider != null)
        {
            if (canAttack) return;
            StartCoroutine(AttackCooldown());
        }
    }
    IEnumerator AttackCooldown()
    {
        canAttack = true;
        Debug.Log("In the Enumerator");
        HealthBar.HB.TakeDamage(reachDamage);
        yield return new WaitForSeconds(cooldown);
        canAttack = false;
    }
}
