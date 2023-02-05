using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lurker : MonoBehaviour
{
    [SerializeField]
    private Transform PointA;
    [SerializeField]
    private Transform PointB;
    [SerializeField]
    private AnimationCurve smoothLurk;
    private bool animFlip;
    [SerializeField]
    private SpriteRenderer sRen;
    [SerializeField]
    private float speed = 1f;
    private float rate;
    void Update()
    {
        rate += Time.deltaTime * speed;

        transform.position = Vector3.Lerp(PointA.position, PointB.position, smoothLurk.Evaluate(rate));

        if(rate >= .5f)
            sRen.flipX = true;
        if (rate >= 1)
        {
            rate = 0;
            sRen.flipX = false;
        }
    }
}
