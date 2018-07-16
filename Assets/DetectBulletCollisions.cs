using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBulletCollisions : MonoBehaviour
{
    [SerializeField]
    private BulletBehaviour bulletBehaviour;

    private float detectRange = 1f;


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Walls"))
        {
            Debug.Log("COLLIDED! trigger");
            bulletBehaviour.SetMoveBullet(false);
            transform.parent = other.transform;
        }
    }*/


    private void Update()
    {
        DetectHit();
    }


    private void DetectHit()
    {
        Vector3 rayOrigin = transform.position;

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, transform.forward, out hit, detectRange) || Physics.Raycast(rayOrigin, -transform.right, out hit, detectRange) || Physics.Raycast(rayOrigin, transform.right, out hit, detectRange) || Physics.Raycast(rayOrigin, -transform.up, out hit, detectRange) || Physics.Raycast(rayOrigin, transform.up, out hit, detectRange))
        {
            bulletBehaviour.DecreaseSpeed(3f);

            if (hit.transform.CompareTag("BulletStickCollision"))
            {
                if(hit.distance <= 0.25f) bulletBehaviour.SetMoveBullet(false);

            }
        }

        Debug.Log(bulletBehaviour.GetSpeed());
    }

}
