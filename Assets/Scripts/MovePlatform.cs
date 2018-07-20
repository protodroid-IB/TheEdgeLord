using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float acceleration;

    private Vector3 direction;

    [SerializeField]
    private Transform startTransform, endTransform;

    private bool moveToStart = false;


	// Use this for initialization
	void Start ()
    {
        direction = (endTransform.position - startTransform.position).normalized;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
	}


    private void Move()
    {
        if(moveToStart)
        {
            transform.position += -direction * speed * Time.deltaTime;
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlatformEnd")
        {
            moveToStart = true;
        }
        
        if(other.gameObject.tag == "PlatformStart")
        {
            moveToStart = false;
        }
    }
}
