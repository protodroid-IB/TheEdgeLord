using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GunAim), typeof(GunFire))]
public class GunProperties : MonoBehaviour
{

    [SerializeField]
    private bool debugMode = false;

    private GameController gameController;

    private GunFire gunFire;

    private GunAim gunAim;

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




    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gunFire = GetComponent<GunFire>();
        gunAim = GetComponent<GunAim>();
    }

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

    public GameController GetGameController()
    {
        return gameController;
    }

    public GunFire GetGunFire()
    {
        return gunFire;
    }

    public GunAim GetGunAim()
    {
        return gunAim;
    }





    public void SetBulletPrefab(GameObject inBullet)
    {
        bulletPrefab = inBullet;
    }


}
