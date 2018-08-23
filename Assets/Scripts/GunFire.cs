using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    private GunProperties gunProperties;

    private float fireRateTimer;

    private AudioSource shootAudio;


	// Use this for initialization
	void Start ()
    {
        gunProperties = GetComponent<GunProperties>();
        fireRateTimer = gunProperties.GetFireRate();
        shootAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(gunProperties.GetGameController().GetPlayerInputEnabled()) FireGun();
    }


    void FireGun()
    {
        if(Input.GetButton("Fire1"))
        {
            if(fireRateTimer <= 0f)
            {
                //Debug.Log("Fire!");
                GameObject bullet = Instantiate(gunProperties.GetBulletPrefab(), gunProperties.GetBulletTransform().position, gunProperties.GetBulletTransform().rotation, gunProperties.GetBulletsInHierarchyTransform());
                fireRateTimer = gunProperties.GetFireRate();
                shootAudio.Play();
            }
        }

        fireRateTimer -= Time.deltaTime;
    }
}
