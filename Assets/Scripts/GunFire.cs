using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    private GunProperties gunProperties;


	// Use this for initialization
	void Start ()
    {
        gunProperties = GetComponent<GunProperties>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        FireGun();
	}


    void FireGun()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire!");
            Instantiate(gunProperties.GetBulletPrefab(), gunProperties.GetBulletTransform().position, gunProperties.GetBulletTransform().rotation, gunProperties.GetBulletsInHierarchyTransform());
        }
    }
}
