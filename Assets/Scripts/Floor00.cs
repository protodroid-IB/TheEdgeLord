using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor00 : MonoBehaviour
{
    [SerializeField]
    private GameObject torchesGO;
    
    [Space(20)]

    [SerializeField]
    private DoorSwitch[] doors;

    [Space(20)]

    [SerializeField]
    private PressureSwitch[] pressureSwitches;


    private TorchColor[] torchesColor;
    private TorchSwitch[] torches;

    private bool door02Unlocked = false, door03Unlocked = false;





	// Use this for initialization
	private void Start ()
    {
        torchesColor = new TorchColor[torchesGO.transform.childCount];
        torches = new TorchSwitch[torchesGO.transform.childCount];

		for(int i=0; i < torchesGO.transform.childCount; i++)
        {
            torchesColor[i] = torchesGO.transform.GetChild(i).GetComponent<TorchColor>();
            torches[i] = torchesGO.transform.GetChild(i).GetComponent<TorchSwitch>();
        }
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
    }



	// Update is called once per frame
	void Update ()
    {
        UnlockDoor02();
        UnlockDoor03();
	}
}
