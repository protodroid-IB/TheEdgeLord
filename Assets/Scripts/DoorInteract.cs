using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoorSwitch))]
public class DoorInteract : MonoBehaviour
{
    private DoorSwitch doorSwitch;

    [SerializeField]
    private PlayerInteract playerInteract;

    private void Start()
    {
        doorSwitch = GetComponent<DoorSwitch>();
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if (playerInteract.Interact())
        {
            if (doorSwitch.isLocked() == false)
            {
                if (doorSwitch.isClosed()) doorSwitch.Open();
                else if (doorSwitch.isOpen()) doorSwitch.Close();
            }
        }
    }
}
