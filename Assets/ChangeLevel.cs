using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField]
    private string scene;

    private SceneFader sceneFader;


    // Use this for initialization
    void Start()
    {
        sceneFader = GameObject.FindWithTag("SceneFader").GetComponent<SceneFader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneFader.FadeIntoScene(scene);
        }
    }
}
