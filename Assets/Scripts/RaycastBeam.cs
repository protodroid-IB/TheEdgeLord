using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBeam : MonoBehaviour
{

    private enum Axis { x, y, z };

    [SerializeField]
    private Axis currentAxis;

    private Vector3 raycastDirection;

    private float raycastDistance;

    private Vector3 raycastOrigin;

    [SerializeField]
    private GameObject lightBeamGO;

    private BoxCollider thisCollider;

    private GameObject intersectCube = null;
    private IntersectCube intersectCubeScript;
    private bool intersectAdded = false;



    private void Awake()
    {
        thisCollider = GetComponent<BoxCollider>();
    }


    // Update is called once per frame
    void FixedUpdate ()
    {
        raycastOrigin = transform.position;

        switch (currentAxis)
        {
            case Axis.x:
                if (transform.parent.name == "Right") raycastDirection = transform.right;
                else raycastDirection = -transform.right;
                break;

            case Axis.y:
                if (transform.parent.name == "Top") raycastDirection = -transform.up;
                else raycastDirection = transform.up;
                break;

            case Axis.z:
                if (transform.parent.name == "Front") raycastDirection = -transform.forward;
                else raycastDirection = transform.forward;
                break;
        }

        ProduceRaycast();
        ScaleAndPositionBeam();


    }


    private void ProduceRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, 300f))
        {

            Debug.DrawLine(raycastOrigin, hit.point, Color.red);

            if (hit.transform.childCount >= 4)
            {
                if (hit.transform.GetChild(3) != null)
                {
                    Transform otherBeam = hit.transform.GetChild(3).Find(transform.parent.name).GetChild(0);

                    Debug.Log(otherBeam.parent.name);

                    if (otherBeam.CompareTag("BeamCast") && intersectCube == null)
                    {
                        Debug.Log(transform.parent.name + "  - CREATE CUBE!");
                        intersectCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        intersectCube.transform.position = hit.point + raycastDirection * 0.5f;
                        intersectCube.transform.rotation = transform.rotation;
                        intersectCube.GetComponent<Collider>().isTrigger = true;
                        intersectCube.layer = 0;
                        intersectCube.tag = "intersectCube";
                        intersectCubeScript = intersectCube.AddComponent<IntersectCube>();



                        /*intersectCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        intersectCube.transform.position = hit.point + raycastDirection * 0.5f;
                        intersectCube.transform.rotation = transform.rotation;
                        intersectCube.GetComponent<Collider>().isTrigger = true;
                        intersectCube.layer = 0;
                        intersectCube.tag = "intersectCube";
                        intersectCubeScript = intersectCube.AddComponent<IntersectCube>();*/



                        otherBeam.GetComponent<RaycastBeam>().SetIntersectCube(intersectCube);
                        SetIntersectCube(intersectCube);

                        intersectCubeScript.AddNumIntersects();
                        //intersectCubeScript.AddNumIntersects();


                    }
                }
            }
            /*else if (hit.transform.CompareTag("intersectCube"))
            {
                if(intersectCubeScript != null)
                {
                    if(intersectCubeScript.GetNumIntersects() == 1)
                    {
                        intersectCubeScript.RemoveNumIntersects();
                    }
                }
                else
                {
                    intersectCubeScript = hit.transform.GetComponent<IntersectCube>();
                    intersectCubeScript.AddNumIntersects();
                }
            }
            else
            {
                if (intersectCubeScript != null)
                {
                        intersectCubeScript.RemoveNumIntersects();
                }
            }*/

            raycastDistance = hit.distance;
        }
        else
        {
            Debug.DrawLine(raycastOrigin, raycastDirection * 300f, Color.red);
        }

    }


    private void ScaleAndPositionBeam()
    {
        switch (currentAxis)
        {
            case Axis.x:
                if (transform.parent.name == "Right")
                {
                    lightBeamGO.transform.localScale = new Vector3(raycastDistance, lightBeamGO.transform.localScale.y, lightBeamGO.transform.localScale.z);
                    lightBeamGO.transform.localPosition = new Vector3((transform.localPosition.x - 0.25f + raycastDistance/2), lightBeamGO.transform.localPosition.y, lightBeamGO.transform.localPosition.z);
                    thisCollider.size = new Vector3(lightBeamGO.transform.localScale.x, 1f, 1f);
                    thisCollider.center = new Vector3(thisCollider.size.x / 2f - 0.25f, 0f, 0f);
                }
                else
                {
                    lightBeamGO.transform.localScale = new Vector3(raycastDistance, lightBeamGO.transform.localScale.y, lightBeamGO.transform.localScale.z);
                    lightBeamGO.transform.localPosition = new Vector3((transform.localPosition.x + 0.25f - raycastDistance / 2), lightBeamGO.transform.localPosition.y, lightBeamGO.transform.localPosition.z);
                    thisCollider.size = new Vector3(lightBeamGO.transform.localScale.x * -1f, 1f, 1f);
                    thisCollider.center = new Vector3(thisCollider.size.x / 2f + 0.25f, 0f, 0f);
                }
                break;

            case Axis.y:
                if (transform.parent.name == "Top")
                {
                    lightBeamGO.transform.localScale = new Vector3(lightBeamGO.transform.localScale.x, raycastDistance, lightBeamGO.transform.localScale.z);
                    lightBeamGO.transform.localPosition = new Vector3(lightBeamGO.transform.localPosition.x, (transform.localPosition.y + 0.25f - raycastDistance / 2), lightBeamGO.transform.localPosition.z);
                    thisCollider.size = new Vector3(1f, lightBeamGO.transform.localScale.y * -1f, 1f);
                    thisCollider.center = new Vector3(0f, thisCollider.size.y / 2f + 0.25f, 0f);
                }
                else
                {
                    lightBeamGO.transform.localScale = new Vector3(lightBeamGO.transform.localScale.x, raycastDistance, lightBeamGO.transform.localScale.z);
                    lightBeamGO.transform.localPosition = new Vector3(lightBeamGO.transform.localPosition.x, (transform.localPosition.y - 0.25f + raycastDistance / 2), lightBeamGO.transform.localPosition.z);
                    thisCollider.size = new Vector3(1f, lightBeamGO.transform.localScale.y, 1f);
                    thisCollider.center = new Vector3(0f, thisCollider.size.y / 2f - 0.25f, 0f);
                }
                break;

            case Axis.z:
                if (transform.parent.name == "Front")
                {
                    lightBeamGO.transform.localScale = new Vector3(lightBeamGO.transform.localScale.x, lightBeamGO.transform.localScale.y, raycastDistance);
                    lightBeamGO.transform.localPosition = new Vector3(lightBeamGO.transform.localPosition.x, lightBeamGO.transform.localPosition.y, (transform.localPosition.z + 0.25f - raycastDistance / 2));
                    thisCollider.size = new Vector3(1f, 1f, lightBeamGO.transform.localScale.z * -1f);
                    thisCollider.center = new Vector3(0f, 0f, thisCollider.size.z / 2f + 0.25f);
                }
                else
                {
                    lightBeamGO.transform.localScale = new Vector3(lightBeamGO.transform.localScale.x, lightBeamGO.transform.localScale.y, raycastDistance);
                    lightBeamGO.transform.localPosition = new Vector3(lightBeamGO.transform.localPosition.x, lightBeamGO.transform.localPosition.y, (transform.localPosition.z - 0.25f + raycastDistance / 2));
                    thisCollider.size = new Vector3(1f, 1f, lightBeamGO.transform.localScale.z);
                    thisCollider.center = new Vector3(0f, 0f, thisCollider.size.z / 2f - 0.25f);
                }
                break;
        }
    }

    public void SetIntersectCube(GameObject inIntersectCube)
    {
        intersectCube = inIntersectCube;
    }




    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.CompareTag("BeamCast"))
        {

        }
    }
}
