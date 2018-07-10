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


    public void Start()
    {
        gunProperties = GetComponent<GunProperties>();
        fpsCam = Camera.main;
        lineOfSight = GetComponent<LineRenderer>();
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
  
        }
        else
        {
            fireDirectionPoint = rayOrigin + (fpsCam.transform.forward * gunProperties.GetFireRange());

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

}
