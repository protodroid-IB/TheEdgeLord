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
    private GameObject fireParticlesGO, beforeLitMeshGO;

    private GameObject lightHitColliderGO;


    private void Awake()
    {
        lightHitColliderGO = transform.GetChild(2).gameObject;

        if (torchOn == false)
        {
            Off();
        }
        else
        {
            On();
        }
    }

    // turns the torch on
    public void On()
    {
        torchOn = true;
        fireParticlesGO.SetActive(torchOn);
        lightHitColliderGO.SetActive(true);
        beforeLitMeshGO.SetActive(false);
    } 

    // turns the torch off
    public void Off()
    {
        torchOn = false;
        fireParticlesGO.SetActive(torchOn);
        lightHitColliderGO.SetActive(false);
        beforeLitMeshGO.SetActive(true);
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
        //TestSwitch();
    }
}
