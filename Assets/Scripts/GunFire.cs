using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : GunAim
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform bulletsInHierarchyTransform;


	// Use this for initialization
	void Start ()
    {
        DeclareAimVariables();

    }
	
	// Update is called once per frame
	void Update ()
    {
        AimGun();
        FireGun();
	}




    void FireGun()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire!");
            Instantiate(bullet, GetBulletTransform().position, GetBulletTransform().rotation, bulletsInHierarchyTransform);
        }
    }
}
