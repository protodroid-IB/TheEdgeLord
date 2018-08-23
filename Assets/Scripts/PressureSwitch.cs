using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureSwitch : MonoBehaviour
{
    [SerializeField]
    private bool activated = false;

    [SerializeField]
    private MeshRenderer switchMR;

    [SerializeField]
    private Material onSwitchMat;
    private Material originalSwitchMat;

    private Material[] mats;

    private AudioSource thisAudio;

    [SerializeField]
    private AudioClip activateSound, deactivateSound;

    private void Start()
    {
        thisAudio = GetComponent<AudioSource>();
        
        originalSwitchMat = switchMR.materials[1];

        mats = new Material[switchMR.materials.Length];

        for(int i=0; i < mats.Length; i++)
        {
            mats[i] = switchMR.materials[i];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Activate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("StoneBlock"))
        {
            if (activated == false)
            {
                if (other.transform.parent.GetComponent<StoneBlockInteract>().Grounded())
                {
                    Activate();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("StoneBlock"))
        {
            Deactivate();
        }
    }


    private void Activate()
    {
        activated = true;
        mats[1] = onSwitchMat;
        switchMR.materials = mats;
        thisAudio.clip = activateSound;
        thisAudio.Play();
    }

    private void Deactivate()
    {
        activated = false;
        mats[1] = originalSwitchMat;
        switchMR.materials = mats;
        thisAudio.clip = deactivateSound;
        thisAudio.Play();
    }


    public bool Activated()
    {
        return activated;
    }
}
