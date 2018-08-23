using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private GameController gameController;

    private bool interact = false;

    [SerializeField]
    private GunAim gunAim;

    private AudioSource interactSource;

    [SerializeField]
    private AudioClip interactSound;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        interactSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(gameController.GetPlayerInputEnabled())
        {
            if(Input.GetButtonDown("Interact"))
            {
                interactSource.clip = interactSound;
                interactSource.Play();
                interact = true;
                Debug.Log(InteractedUpon().name);
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

    public GameObject InteractedUpon()
    {
        return gunAim.GetGameObjectHit();
    }
}
