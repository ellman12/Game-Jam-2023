using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private void Update()
    {
        if (gameObject)
            StartCoroutine(AttackTriggerDelete());
    }
    IEnumerator AttackTriggerDelete()

    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
