using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealMessage : MonoBehaviour
{
    [SerializeField]
    private float messageRevealRange = 10f;

    private Text message;

    private float closestLightDistance = 1000f;

    private float alpha = 0f;

	// Use this for initialization
	void Start ()
    {
        message = transform.GetChild(0).GetComponent<Text>();
        alpha = 0f;

        Color newColor = new Color(message.color.r, message.color.g, message.color.b, alpha);

        message.color = newColor;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LightRangeCollider")
        {
            float distance = Distance(other.transform.position);


            if(distance < closestLightDistance)
            {
                closestLightDistance = distance;

                if (closestLightDistance <= messageRevealRange && closestLightDistance >= 0f)
                {
                    Debug.Log(name + " " + distance + " away from " + other.transform.parent.name);

                    if(other.transform.parent.GetComponent<TorchSwitch>().GetTorchState())
                    {
                        alpha = 1f;
                    }

                    float ratio = ((messageRevealRange - closestLightDistance) / messageRevealRange);

                    alpha *= ratio;

                    //Debug.Log(alpha);

                    Color newColor = new Color(message.color.r, message.color.g, message.color.b, alpha);

                    message.color = newColor;
                }
            }

            
        }
    }


    private float Distance(Vector3 objectPosition)
    {
        float distance;

        distance = (objectPosition - transform.position).magnitude;

        return distance;
    }

    private Vector3 Direction(Vector3 objectPosition)
    {
        Vector3 direction = (objectPosition - transform.position).normalized;

        return direction;
    }
}
