using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor00 : Floor
{

    [SerializeField]
    private GameObject torchesGO, doorsGO, pressureSwitchesGO;

    private DoorSwitch[] doors;

    private PressureSwitch[] pressureSwitches;

    private TorchColor[] torchesColor;
    private TorchSwitch[] torches;

    private bool door02Unlocked = false, door03Unlocked = false;



    // Use this for initialization
    private void Start()
    {
        UpdateLevelStartVariables();
    }
	


    private void UnlockDoor02()
    {
        if(door02Unlocked == false)
        {
            if (pressureSwitches[0].Activated() && doors[1].isLocked())
            {
                doors[1].Unlock();
                door02Unlocked = true;
            }
        }

        if(pressureSwitches[0].Activated() == false)
        {
            door02Unlocked = false;
            doors[1].Lock();
        }
        
    }

    private void UnlockDoor03()
    {
        if (door03Unlocked == false)
        {
            if (pressureSwitches[1].Activated() && doors[2].isLocked())
            {
                doors[2].Unlock();
                door03Unlocked = true;
            }
        }

        if (pressureSwitches[1].Activated() == false)
        {
            door03Unlocked = false;
            doors[2].Lock();
        }
    }

    private void OnEnable()
    {
        UpdateLevelStartVariables();

        if(door02Unlocked == true)
        {
            doors[1].Unlock();
        }

        if(door03Unlocked == true)
        {
            doors[2].Unlock();
        }
    }



    // Update is called once per frame
    void Update ()
    {
        UnlockDoor02();
        UnlockDoor03();
	}


    private void UpdateLevelStartVariables()
    {
        torchesGO = GameObject.FindWithTag("Torches");
        doorsGO = GameObject.FindWithTag("Doors");
        pressureSwitchesGO = GameObject.FindWithTag("PressureSwitches");

        torchesColor = new TorchColor[torchesGO.transform.childCount];
        torches = new TorchSwitch[torchesGO.transform.childCount];

        for (int i = 0; i < torchesGO.transform.childCount; i++)
        {
            torchesColor[i] = torchesGO.transform.GetChild(i).GetComponent<TorchColor>();
            torches[i] = torchesGO.transform.GetChild(i).GetComponent<TorchSwitch>();
        }

        doors = new DoorSwitch[doorsGO.transform.childCount];

        for (int i = 0; i < doorsGO.transform.childCount; i++)
        {
            doors[i] = doorsGO.transform.GetChild(i).GetComponent<DoorSwitch>();
        }

        pressureSwitches = new PressureSwitch[pressureSwitchesGO.transform.childCount];

        for (int i = 0; i < pressureSwitchesGO.transform.childCount; i++)
        {
            pressureSwitches[i] = pressureSwitchesGO.transform.GetChild(i).GetChild(1).GetComponent<PressureSwitch>();
        }
    } 
}
