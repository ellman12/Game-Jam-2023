using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector3 direction;

    [SerializeField]
    private float detectionDistance;
    private Ray2D range;
    private RaycastHit2D hit;
    private bool moving;
    [SerializeField]
    private LayerMask seekerLayer;
    [SerializeField]
    private SpriteRenderer sRen;

    [SerializeField]
    private float speed = 1;
    private void Update()
    {
        range.direction = target.position - transform.position;
        range.origin = transform.position;
        direction = target.position - transform.position;
        Debug.DrawRay(range.origin, range.direction.normalized * detectionDistance, Color.red);
        hit = Physics2D.Raycast(transform.position, target.position - transform.position, detectionDistance, seekerLayer);

        if (hit.collider != null)
        {
            if (transform.position.x - target.position.x > 0)
            {
                direction = new Vector3(-1, 0, 0);  //left
                sRen.flipX = false ;
            }
            else if (transform.position.x - target.position.x < 0)
            {
                direction = new Vector3(1, 0, 0);   //right
                sRen.flipX = true;
            }

            transform.position += direction * Time.deltaTime * speed;
        }
    }
}
