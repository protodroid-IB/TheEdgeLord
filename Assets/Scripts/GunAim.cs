using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawnTransform;

    private LineRenderer lineOfSight;
    private Camera fpsCam;

    private Vector3 fireDirectionPoint;

    private float fireRange = 150f;


    private void Start()
    {
        
    }



    public void DeclareAimVariables()
    {
        fpsCam = Camera.main;
        lineOfSight = GetComponent<LineRenderer>();
    }



    public Vector3 GetFireDirectionPoint()
    {
        return fireDirectionPoint;
    }



    public Vector3 GetFireDirection()
    {
        return (bulletSpawnTransform.position - fireDirectionPoint).normalized;
    }




    public Transform GetBulletTransform()
    {
        return bulletSpawnTransform;
    }
	



    public void AimGun()
    {
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        RaycastHit hit;

        lineOfSight.SetPosition(0, bulletSpawnTransform.position);

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, fireRange))
        {
            fireDirectionPoint = hit.point;

            lineOfSight.SetPosition(1, fireDirectionPoint);
        }
        else
        {
            fireDirectionPoint = rayOrigin + (fpsCam.transform.forward * fireRange);

            lineOfSight.SetPosition(1, fireDirectionPoint);
        }
    }

}
