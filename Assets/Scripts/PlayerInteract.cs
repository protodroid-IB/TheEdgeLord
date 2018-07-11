using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private GameController gameController;

    private bool interact = false;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(gameController.GetPlayerInputEnabled())
        {
            if(Input.GetButtonDown("Interact"))
            {
                Debug.Log("Interact!");
                interact = true;
            }
            else
            {
                interact = false;
            }
        }
	}

    public bool Interact()
    {  
        return interact;
    }
}
