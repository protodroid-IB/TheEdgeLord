using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSwitch : MonoBehaviour
{
    // boolean that determines if torch is on
    [SerializeField]
    private bool torchOn = false;

    // the gameobject for the torch's fire particles
    [SerializeField]
    private GameObject fireParticlesGO;


    private void Start()
    {
        if(torchOn == false)
        {
            Off();
        }
    }

    // turns the torch on
    public void On()
    {
        torchOn = true;
        fireParticlesGO.SetActive(torchOn);
    } 

    // turns the torch off
    public void Off()
    {
        torchOn = false;
        fireParticlesGO.SetActive(torchOn);
    }

    // grabs the torch state 
    public bool GetTorchState()
    {
        return torchOn;
    }




    private void TestSwitch()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            if (torchOn == false)
            {
                On();
            }
            else
            {
                Off();
            }
        }
    }

    private void Update()
    {
        TestSwitch();
    }
}
