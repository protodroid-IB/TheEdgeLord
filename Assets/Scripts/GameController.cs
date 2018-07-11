using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private bool disablePlayerInput = false;

    [SerializeField]
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        DisablePlayerControlsDebug();

        if (disablePlayerInput == true) fpsController.enabled = false;
        else fpsController.enabled = true;
	}

    public bool GetPlayerInputEnabled()
    {
        return !disablePlayerInput;
    }

    public void SetPlayerInputEnabled(bool inActive)
    {
        disablePlayerInput = !inActive;
    }

    private void DisablePlayerControlsDebug()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (GetPlayerInputEnabled() == true) SetPlayerInputEnabled(false);
            else SetPlayerInputEnabled(true);
        }
    }
}
