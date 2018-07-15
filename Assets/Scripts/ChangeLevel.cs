using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField]
    private string scene;

    private SceneFader sceneFader;
    private GameController gameController;


    // Use this for initialization
    void Start()
    {
        sceneFader = GameObject.FindWithTag("SceneFader").GetComponent<SceneFader>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameController.SetPlayerInputEnabled(false);
            sceneFader.FadeIntoScene(scene);
        }
    }
}
