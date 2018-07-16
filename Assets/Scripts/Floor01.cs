using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor01 : Floor
{

    [SerializeField]
    private GameObject torchesGO, doorsGO, messagesGO;

    private DoorSwitch[] doors;

    private RevealMessage[] messages;

    private TorchColor[] torchesColor;
    private TorchSwitch[] torches;

    private bool door04Unlocked = false, door06Unlocked = false;

    private DoorSwitch door04, door06;
    private TorchSwitch torchSwitch16, torchSwitch17;
    private TorchColor torchColor16, torchColor17;
    private RevealMessage message04a, message04b, message05a, message05b, message06a, message06b;

    [SerializeField]
    private bool debugUnlockAllDoors = false;


    // Use this for initialization
    void Start ()
    {
        UpdateLevelStartVariables();

    }



    private void OnEnable()
    {
        UpdateLevelStartVariables();

        if (door04Unlocked == true)
        {
            door04.Unlock();
        }

        if (door06Unlocked == true)
        {
            door06.Unlock();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        
        UnlockDoor04();
        UnlockDoor06();
    }




    private void UnlockDoor04()
    {
        if(torchSwitch16.GetTorchState() && torchSwitch17.GetTorchState())
        {
            if (torchColor16.GetColorState() == "Red")
            {
                if (torchColor17.GetColorState() == "Yellow")
                {
                    door04Unlocked = true;
                }
                else door04Unlocked = false;
            }
            else if (torchColor16.GetColorState() == "Yellow")
            {
                if (torchColor17.GetColorState() == "Red")
                {
                    door04Unlocked = true;
                }
                else door04Unlocked = false;
            }
            else door04Unlocked = false;
        }
        else door04Unlocked = false;

        if (debugUnlockAllDoors == true) door04Unlocked = true;
        DoorLogic(door04, door04Unlocked);
    }




    private void UnlockDoor06()
    {
        bool msg1 = message04a.Revealed() && message04b.Revealed();
        bool msg2 = message05a.Revealed() && message05b.Revealed();
        bool msg3 = message06a.Revealed() && message06b.Revealed();

        if (msg1 && msg2 && msg3) door06Unlocked = true;
        else door06Unlocked = false;

        if (debugUnlockAllDoors == true) door06Unlocked = true;

        DoorLogic(door06, door06Unlocked);
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
            if(doorState == false)
            {
                inDoor.Lock();
            }
        }
    }


    private void UpdateLevelStartVariables()
    {
        GrabPlayerSpawnPositions();

        torchesGO = GameObject.FindWithTag("Torches");
        doorsGO = GameObject.FindWithTag("Doors");
        messagesGO = GameObject.FindWithTag("Messages");

        torchesColor = new TorchColor[torchesGO.transform.childCount];
        torches = new TorchSwitch[torchesGO.transform.childCount];

        for (int i = 0; i < torchesGO.transform.childCount; i++)
        {
            torchesColor[i] = torchesGO.transform.GetChild(i).GetComponent<TorchColor>();
            torches[i] = torchesGO.transform.GetChild(i).GetComponent<TorchSwitch>();
        }

        torchSwitch16 = torches[2];
        torchSwitch17 = torches[3];

        torchColor16 = torchesColor[2];
        torchColor17 = torchesColor[3];

        doors = new DoorSwitch[doorsGO.transform.childCount];

        for (int i = 0; i < doorsGO.transform.childCount; i++)
        {
            doors[i] = doorsGO.transform.GetChild(i).GetComponent<DoorSwitch>();
        }

        door04 = doors[1];
        door06 = doors[3];

        messages = new RevealMessage[messagesGO.transform.childCount];

        for (int i = 0; i < messagesGO.transform.childCount; i++)
        {
            messages[i] = messagesGO.transform.GetChild(i).GetComponent<RevealMessage>();
        }

        message04a = messages[4];
        message04b = messages[5];
        message05a = messages[6];
        message05b = messages[7];
        message06a = messages[8];
        message06b = messages[9];
    }
}
