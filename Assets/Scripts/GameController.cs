using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private bool disablePlayerInput = false;

    private FirstPersonController fpsController;

    private FloorChanges floorChangesScript;

    private GameObject playerGO;

    private void Awake()
    {
        playerGO = GameObject.FindWithTag("Player");
        fpsController = playerGO.GetComponent<FirstPersonController>();
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









  
    public void SetFPSController(FirstPersonController inController)
    {
        fpsController = inController;
    }



    public FloorChanges GetFloorChanges()
    {
        if(floorChangesScript == null) floorChangesScript = GetComponent<FloorChanges>();

        return floorChangesScript;
    }
    
}
