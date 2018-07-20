using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTorchGoal : MonoBehaviour
{

    private bool goalReached = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            goalReached = true;
        }
    }

    public bool GetGoalReached()
    {
        return goalReached;
    }
}
