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
            if (playerInteract.Interact())
            {
                if (playerInteract.InteractedUpon().name == gameObject.name)
                {
                    Debug.Log(playerInteract.InteractedUpon().name + "\t=\t" + gameObject.name);

                    if (isGrounded == true)
                    {
                        if (playerGrabTransform.childCount == 0)
                        {
                            GrabBlock();
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (playerInteract.Interact())
        {
            if (isGrounded == false)
            {
                PlaceBlock();
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
        Debug.Log("Grab Block!");
    }




    private void PlaceBlock()
    {
        playerGunGO.SetActive(true);
        transform.SetParent(stoneBlocksTransform);
        thisRB.useGravity = true;
        thisRB.isKinematic = false;
        Debug.Log("Place Block!");
    }


    public bool Grounded()
    {
        return isGrounded;
    }

}
