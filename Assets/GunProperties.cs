using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunAim), typeof(GunFire))]
public class GunProperties : MonoBehaviour
{

    [SerializeField]
    private bool debugMode = false;

    [SerializeField]
    private Transform bulletSpawnTransform;

    [SerializeField]
    private Transform bulletsInHierarchyTransform;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private float fireRange;




   

    public Transform GetBulletTransform()
    {
        return bulletSpawnTransform;
    }

    public Transform GetBulletsInHierarchyTransform()
    {
        return bulletsInHierarchyTransform;
    }

    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }

    public float GetFireRange()
    {
        return fireRange;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public bool GetDebugMode()
    {
        return debugMode;
    }





    public void SetBulletPrefab(GameObject inBullet)
    {
        bulletPrefab = inBullet;
    }


}
