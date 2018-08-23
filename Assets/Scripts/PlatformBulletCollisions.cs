using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBulletCollisions : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "LightBullet")
        {
            other.transform.parent = transform;
            Debug.Log("BULLET W/ PLATFORM");
        }
    }
}
