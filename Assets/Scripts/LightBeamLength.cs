using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamLength : MonoBehaviour
{
    private Vector3 rayOriginPoint;

    private enum Axis { x, y, z };

    [SerializeField]
    private Axis currentAxis;

    private float xProportion, yProportion, zProportion;

    private Transform thisTransform;

    private Collider thisCollider;

    private float beamLength;

    private bool intersected = false;
    private float intersectScale;
    private Vector3 intersectPoint;

    [SerializeField]
    private GameObject beamMeshPrefab;

    //Use this for initialization

   void Start()
    {
        thisTransform = transform;
        thisCollider = GetComponent<Collider>();

        xProportion = thisTransform.localPosition.x / thisTransform.localScale.x;

        yProportion = thisTransform.localPosition.y / thisTransform.localScale.y;
        yProportion *= (transform.localScale.y / Mathf.Abs(transform.localScale.y));

        zProportion = thisTransform.localPosition.z / thisTransform.localScale.x;
        zProportion *= (transform.localScale.x / Mathf.Abs(transform.localScale.x));
    }



    private void LateUpdate()
    {


        switch (currentAxis)
        {
            case Axis.x:
                RayOriginPointXAxis();
                ProduceRayCastXAxis();
                break;

            case Axis.y:
                RayOriginPointYAxis();
                ProduceRayCastYAxis();
                break;

            case Axis.z:
                RayOriginPointZAxis();
                ProduceRayCastZAxis();
                break;
        }

        

    }


    private void RayOriginPointXAxis()
    {
        rayOriginPoint = transform.position - transform.right * transform.localPosition.x;
    }

    private void RayOriginPointYAxis()
    {
        if(yProportion > 0) rayOriginPoint = transform.position + transform.up * transform.localPosition.y * 2f * yProportion;
        else rayOriginPoint = transform.position + transform.up * transform.localPosition.y * 2f * yProportion;
    }

    private void RayOriginPointZAxis()
    {
        if(zProportion > 0) rayOriginPoint = transform.position + transform.right * transform.localPosition.z * 2f * zProportion;
        else rayOriginPoint = transform.position - transform.right * transform.localPosition.z * 2f * zProportion;
    }






    private void ProduceRayCastXAxis()
    {
        RaycastHit hit;

        if (Physics.Raycast(rayOriginPoint, transform.right * 2f * xProportion, out hit, 300f))
        {
            Debug.DrawLine(rayOriginPoint, hit.point, Color.red);

            ScaleAndRepositionX(beamLength);

            beamLength = hit.distance;
        }
    }

    private void ScaleAndRepositionX(float scale)
    {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(transform.localScale.x * xProportion, transform.localPosition.y, transform.localPosition.z);
        Debug.Log(intersected);
    }







    private void ProduceRayCastYAxis()
    {
        RaycastHit hit;

        if (Physics.Raycast(rayOriginPoint, transform.up * 2f * yProportion, out hit, 300f))
        {
            Debug.DrawLine(rayOriginPoint, hit.point, Color.red);

            ScaleAndRepositionY(beamLength);

            beamLength = hit.distance;
        }
    }

    private void ScaleAndRepositionY(float scale)
    {
        transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y * yProportion, transform.localPosition.z);
        Debug.Log(intersected);
    }








    private void ProduceRayCastZAxis()
    {
        RaycastHit hit;

        if (Physics.Raycast(rayOriginPoint, -transform.right * 2f * zProportion, out hit, 300f))
        {
            Debug.DrawLine(rayOriginPoint, hit.point, Color.red);

            ScaleAndRepositionZ(beamLength);

            beamLength = hit.distance;
        }
    }

    private void ScaleAndRepositionZ(float scale)
    {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localScale.x * zProportion);
        Debug.Log(intersected);
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("LightBeam"))
        {
            intersectPoint = (other.ClosestPoint(transform.position));

            SetIntersectScale((intersectPoint - rayOriginPoint).magnitude + 2f);
            beamLength = intersectScale;
        }
    }



    public void SetIntersectScale(float inScale)
    {
        intersectScale = inScale;
    }


    public void CreateNewBeam()
    {

    }


}
