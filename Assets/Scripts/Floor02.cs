using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor02 : Floor
{

    [SerializeField]
    private GameObject timedTorchesGO, doorsGO;

    private DoorSwitch[] doors;
    private TorchSwitch[] torches;

    private DoorSwitch door;

    private TorchSwitch torch;

    private bool reachedDoor = false; 

    private bool door07Unlocked = false;

    [SerializeField]
    private bool debugUnlockAllDoors = false;


    // Use this for initialization
    void Start()
    {
        UpdateLevelStartVariables();

    }



    private void OnEnable()
    {
        UpdateLevelStartVariables();

        if (door07Unlocked == true)
        {
            door.Unlock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UnlockDoor07();
    }




    private void UnlockDoor07()
    {
        if (torch == null) UpdateLevelStartVariables();

        if(torch.GetTorchState() || reachedDoor == true)
        {
            door07Unlocked = true;
        }
        else
        {
            door07Unlocked = false;
        }

        DoorLogic(door, door07Unlocked);
    }







    private void DoorLogic(DoorSwitch inDoor, bool doorState)
    {
        if (inDoor.isLocked())
        {
            if (doorState == true)
            {
                inDoor.Unlock();
            }
        }
        else
        {
            if (doorState == false)
            {
                inDoor.Lock();
            }
        }
    }


    private void UpdateLevelStartVariables()
    {
        GrabPlayerSpawnPositions();

        timedTorchesGO = GameObject.FindWithTag("TimedTorches");
        doorsGO = GameObject.FindWithTag("Doors");

        torches = new TorchSwitch[timedTorchesGO.transform.childCount];

        for (int i = 0; i < timedTorchesGO.transform.childCount; i++)
        {
            torches[i] = timedTorchesGO.transform.GetChild(i).GetComponent<TorchSwitch>();
        }

        torch = torches[0];

        doors = new DoorSwitch[doorsGO.transform.childCount];

        for (int i = 0; i < doorsGO.transform.childCount; i++)
        {
            doors[i] = doorsGO.transform.GetChild(i).GetComponent<DoorSwitch>();
        }

        door = doors[1];
    }
}
