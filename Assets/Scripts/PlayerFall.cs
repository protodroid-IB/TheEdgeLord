using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{

    private SceneFader sceneFader;

    private void Start()
    {
        sceneFader = GameObject.FindWithTag("SceneFader").GetComponent<SceneFader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sceneFader.RestartScene();
        }
    }
}
