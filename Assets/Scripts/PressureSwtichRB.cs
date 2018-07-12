using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwtichRB : MonoBehaviour
{

    [SerializeField]
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("StoneBlock"))
        {
            activated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StoneBlock"))
        {
            activated = false;
        }
    }



    public bool Activated()
    {
        return activated;
    }
}
