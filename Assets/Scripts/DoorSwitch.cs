using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    // possible states for the current colour of the torch
    private enum State { Open, Closed, Locked };

    // current color state
    [SerializeField]
    private State currentDoorState;

    [SerializeField]
    private GameObject doorGO, lockedBarsGO;


	
	// Update is called once per frame
	void Update ()
    {
        DoorStateSwitch();

    }





    private void DoorStateSwitch()
    {
        switch(currentDoorState)
        {
            case State.Open:
                DoorOpen();
                break;

            case State.Closed:
                DoorClosed();
                break;

            case State.Locked:
                DoorLocked();
                break;

            default:
                DoorOpen();
                break;
        }
    }



    private void DoorOpen()
    {
        
        doorGO.SetActive(false);
        lockedBarsGO.SetActive(false);
    }

    private void DoorClosed()
    {
        
        doorGO.SetActive(true);
        lockedBarsGO.SetActive(false);
    }

    private void DoorLocked()
    {
        doorGO.SetActive(true);
        lockedBarsGO.SetActive(true);
    }


    public void Open()
    {
        Debug.Log("DOOR OPEN!");
        currentDoorState = State.Open;
    }

    public void Close()
    {
        Debug.Log("DOOR CLOSE!");
        currentDoorState = State.Closed;
    }

    public void Lock()
    {
        currentDoorState = State.Locked;
    }

    public void Unlock()
    {
        currentDoorState = State.Closed;
    }

    public bool isLocked()
    {
        if (currentDoorState == State.Locked) return true;
        else return false;
    }

    public bool isClosed()
    {
        if (currentDoorState == State.Closed) return true;
        else return false;
    }

    public bool isOpen()
    {
        if (currentDoorState == State.Open) return true;
        else return false;
    }




}
