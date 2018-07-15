using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    private GunProperties gunProperties;

    private LineRenderer lineOfSight;
    private Camera fpsCam;

    private Vector3 fireDirectionPoint;

    private float fireRange = 150f;

    private GameObject gameObjectHit;

    private GameObject emptyGO;

    public void Start()
    {
        gunProperties = GetComponent<GunProperties>();
        fpsCam = Camera.main;
        lineOfSight = GetComponent<LineRenderer>();

        emptyGO = new GameObject();
    }

    private void Update()
    {
        AimGun();
    }

    public Vector3 GetFireDirectionPoint()
    {
        return fireDirectionPoint;
    }



    public Vector3 GetFireDirection()
    {
        return (gunProperties.GetBulletTransform().position - fireDirectionPoint).normalized;
    }



    public void AimGun()
    {
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, gunProperties.GetFireRange()))
        {
            fireDirectionPoint = hit.point;
            gameObjectHit = hit.transform.gameObject;
        }
        else
        {
            fireDirectionPoint = rayOrigin + (fpsCam.transform.forward * gunProperties.GetFireRange());
            gameObjectHit = emptyGO;
        }


        if(gunProperties.GetDebugMode() == true)
        {
            if (lineOfSight.enabled == false) lineOfSight.enabled = true;

            lineOfSight.SetPosition(0, gunProperties.GetBulletTransform().position);
            lineOfSight.SetPosition(1, fireDirectionPoint);
        }
        else
        {
            if(lineOfSight == true) lineOfSight.enabled = false;
        }
    }


    public GameObject GetGameObjectHit()
    {
        return gameObjectHit;
    }

}
