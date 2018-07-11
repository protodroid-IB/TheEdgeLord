using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlockInteract : MonoBehaviour
{
    [SerializeField]
    private PlayerInteract playerInteract;

    [SerializeField]
    private GameObject playerGunGO;

    [SerializeField]
    private Transform playerGrabTransform;

    [SerializeField]
    private Transform stoneBlocksTransform;

    private bool isGrounded = true;

    private Rigidbody thisRB;



    private void Start()
    {
        thisRB = GetComponent<Rigidbody>();
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            BlockInteract();
        }
    }




    private void BlockInteract()
    {
        if(playerInteract.Interact())
        {
            if(isGrounded == true)
            {
                GrabBlock();
            }
            else
            {
                PlaceBlock();
            }
        }
    }




    private void GrabBlock()
    {
        isGrounded = false;
        playerGunGO.SetActive(false);
        transform.SetParent(playerGrabTransform);
        thisRB.useGravity = false;
        thisRB.isKinematic = true;
        Debug.Log("Grab Block!");
    }



    private void PlaceBlock()
    {
        isGrounded = true;
        playerGunGO.SetActive(true);
        transform.SetParent(stoneBlocksTransform);
        thisRB.useGravity = true;
        thisRB.isKinematic = false;
        Debug.Log("Place Block!");
    }
}
