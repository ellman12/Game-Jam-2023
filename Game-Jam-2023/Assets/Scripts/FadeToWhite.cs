using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToWhite : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    private float rate;
    [SerializeField]
    private Color color;
    private void Update()
    {
        rate += Time.deltaTime;
        if (rate >= 1)
        {
            rate = 0;
        }
    }

}
