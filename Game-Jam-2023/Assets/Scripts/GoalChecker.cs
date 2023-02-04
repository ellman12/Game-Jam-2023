using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    [SerializeField]
    private List<Shrine> shrines = new List<Shrine>();

    void Update()
    {
        int completedShrines = 0;
        foreach(Shrine s in shrines)
        {
            if (s.bIsCleansed == true)
                completedShrines++;
        }

        if(completedShrines == shrines.Count)
        {
            //gate is available to go through
        }
    }
}
