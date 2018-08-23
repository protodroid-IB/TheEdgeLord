using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoorSwitch))]
public class DoorInteract : MonoBehaviour
{
    private DoorSwitch doorSwitch;

    [SerializeField]
    private PlayerInteract playerInteract;

    private bool interacted = false;

    private void Start()
    {
        doorSwitch = GetComponent<DoorSwitch>();
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            if(interacted == false)
            {
                OpenDoor();
            }
                
        }
    }

    private void OpenDoor()
    {
        if (playerInteract.Interact())
        {
            interacted = true;

            if (doorSwitch.isLocked() == false)
            {
                if (doorSwitch.isClosed()) doorSwitch.Open();
                else if (doorSwitch.isOpen()) doorSwitch.Close();          
            }   
        }
    }


    private void Update()
    {
        if(playerInteract.InteractReleased())
        {
            interacted = false;
        }
    }
}
