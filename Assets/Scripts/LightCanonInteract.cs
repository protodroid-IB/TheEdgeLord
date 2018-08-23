using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCanonInteract : MonoBehaviour
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


   

    private void Interact()
    {
        if (DetectPlayer())
        {
            Debug.Log("Player Detected"); 

            if (playerInteract.Interact())
            {
                if (playerInteract.InteractedUpon().name == transform.GetChild(1).name)
                {
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
        Interact();

        if (playerInteract.Interact())
        {
            if (isGrounded == false)
            {
                PlaceBlock();
            }
        }


        if (transform.parent == stoneBlocksTransform) isGrounded = true;
        else isGrounded = false;

        if (isGrounded == false)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        
        if(transform.position.y < -1.3f)
        {
            PlaceBlock();
            transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
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




    public void PlaceBlock()
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




    bool DetectPlayer()
    {
        bool detected = false;

        if(transform.GetChild(0).GetComponent<BoxCollider>().bounds.Contains(playerInteract.transform.position))
        {
            detected = true;
        }

        return detected;
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.CompareTag("BulletStickCollision"))
    //    {
    //        lightCanonInteract.PlaceBlock();
    //    }
    //}

}
