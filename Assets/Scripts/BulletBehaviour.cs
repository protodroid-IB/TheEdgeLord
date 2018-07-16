using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 4f, deathTime = 5f;

    private bool moveBullet = true;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(moveBullet) Move();
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

    public void SetMoveBullet(bool inMove)
    {
        moveBullet = inMove;
    }

    public void DecreaseSpeed(float inAcceleration)
    {
        bulletSpeed -= inAcceleration * Time.deltaTime;
    }

    public float GetSpeed()
    {
        return bulletSpeed;
    }


   
}
