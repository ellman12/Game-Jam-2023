using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector3 direction;

    [SerializeField]
    private float speed = 1;
    private void Update()
    {
        if(transform.position.x - target.position.x > 0)
        {
            direction = new Vector3(-1, 0, 0);
        } 
        else if (transform.position.x - target.position.x < 0)
        {
            direction = new Vector3(1, 0, 0);
        }

        transform.position += direction * Time.deltaTime * speed;
    }
}
