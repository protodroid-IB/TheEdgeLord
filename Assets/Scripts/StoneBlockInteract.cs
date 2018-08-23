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

    private bool interacted = false;



    private void Start()
    {
        thisRB = GetComponent<Rigidbody>();
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (playerInteract.Interact())
            {
                if (playerInteract.InteractedUpon().name == gameObject.name)
                {
                    if (isGrounded == true)
                    {
                        if (playerGrabTransform.childCount == 0)
                        {
                            if(interacted == false)
                            {
                                GrabBlock();
                            }
                            
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        Debug.Log(isGrounded);

        if(playerInteract.InteractReleased())
        {
            interacted = false;
        }


        if (playerInteract.Interact())
        {
            if (isGrounded == false)
            {
                if(interacted == false)
                {
                    PlaceBlock();
                }
                
            }
        }


        if (transform.parent == stoneBlocksTransform) isGrounded = true;
        else isGrounded = false;

        if(isGrounded == false)
        {
            transform.localPosition = Vector3.zero;
        }
    }




    private void GrabBlock()
    {
        playerGunGO.SetActive(false);
        transform.SetParent(playerGrabTransform);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = Vector3.zero;
        thisRB.useGravity = false;
        thisRB.isKinematic = true;
        interacted = true;
        Debug.Log("Grab Block!");
    }




    private void PlaceBlock()
    {
        playerGunGO.SetActive(true);
        transform.SetParent(stoneBlocksTransform);
        thisRB.useGravity = true;
        thisRB.isKinematic = false;
        interacted = true;
        Debug.Log("Place Block!");
    }


    public bool Grounded()
    {
        return isGrounded;
    }


    private void CanGrabAgain()
    {
        isGrounded = true;
    }

}
