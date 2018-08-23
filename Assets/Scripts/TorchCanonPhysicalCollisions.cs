using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCanonPhysicalCollisions : MonoBehaviour
{
    LightCanonInteract lightCanonInteract;

	// Use this for initialization
	void Start ()
    {
        lightCanonInteract = transform.parent.GetComponent<LightCanonInteract>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("BulletStickCollision"))
        {
            lightCanonInteract.PlaceBlock();
        }
    }
}
