using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 4f, deathTime = 5f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        Kill();
    }

    private void Move()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    private void Kill()
    {
        Destroy(gameObject, deathTime);
    }
}
