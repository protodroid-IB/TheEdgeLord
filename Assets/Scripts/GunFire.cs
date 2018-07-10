using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    private GunProperties gunProperties;

    private float fireRateTimer;


	// Use this for initialization
	void Start ()
    {
        gunProperties = GetComponent<GunProperties>();
        fireRateTimer = gunProperties.GetFireRate();
    }
	
	// Update is called once per frame
	void Update ()
    {
        FireGun();
	}


    void FireGun()
    {
        if(Input.GetButton("Fire1"))
        {
            if(fireRateTimer <= 0f)
            {
                Debug.Log("Fire!");
                GameObject bullet = Instantiate(gunProperties.GetBulletPrefab(), gunProperties.GetBulletTransform().position, gunProperties.GetBulletTransform().rotation, gunProperties.GetBulletsInHierarchyTransform());
                fireRateTimer = gunProperties.GetFireRate();
            }
        }

        fireRateTimer -= Time.deltaTime;
    }
}
