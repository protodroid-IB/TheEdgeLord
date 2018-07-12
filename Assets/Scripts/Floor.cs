using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    [SerializeField]
    private Transform playerStart, playerEnd;

    private void OnEnable()
    {
        playerStart = GameObject.FindWithTag("Start").transform;
        playerEnd = GameObject.FindWithTag("End").transform;
    }

    public Transform GetStartTransform()
    {
        return playerStart;
    }

    public Transform GetEndTransform()
    {
        return playerEnd;
    }
}
