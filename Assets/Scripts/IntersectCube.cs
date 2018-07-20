using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectCube : MonoBehaviour
{
    private Rigidbody thisRB;

    private int numIntersects = 0;

	// Use this for initialization
	void Start ()
    {
        thisRB = gameObject.AddComponent<Rigidbody>();
        thisRB.isKinematic = true;
        thisRB.useGravity = false;
	}

    public void AddNumIntersects()
    {
        numIntersects++;
    }

    public void RemoveNumIntersects()
    {
        numIntersects--;
    }

    public int GetNumIntersects()
    {
        return numIntersects;
    }


    private void Update()
    {
        //Debug.Log(numIntersects);

        //if(numIntersects <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }


}
