using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealMessage : MonoBehaviour
{
    private enum ColorState { Red, Orange, Blue, Green, Yellow, Purple, Any};

    [SerializeField]
    private ColorState revealWhen = ColorState.Any;

    [SerializeField]
    private float messageRevealRange = 10f;

    private Text message;

    private float closestLightDistance = 1000f;

    private float closestBulletLightDistance = 1000f;

    private float alpha = 0f;
    private float originalAlpha = 0f;

    private List<TorchColor> torches;


	// Use this for initialization
	void Start ()
    {
        torches = new List<TorchColor>();

        message = transform.GetChild(0).GetComponent<Text>();
        alpha = 0f;
        originalAlpha = alpha;

        Color newColor = new Color(message.color.r, message.color.g, message.color.b, alpha);

        message.color = newColor;
    }



    private void OnTriggerEnter(Collider other)
    {
        // collided with torch range collider
        if(other.gameObject.tag == "LightRangeCollider")
        {
            float distance = Distance(other.transform.position);

            // distance is lower than closest torch
            if(distance < closestLightDistance)
            {
                closestLightDistance = distance;

                if (closestLightDistance <= messageRevealRange && closestLightDistance >= 0f)
                {

                    if(other.transform.parent.GetComponent<TorchSwitch>().GetTorchState())
                    {
                        TorchColor thisTorch = other.transform.parent.GetComponent<TorchColor>();

                        torches.Add(thisTorch);

                        if (thisTorch.GetColorState() == revealWhen.ToString())
                        {
                            float ratio = ((messageRevealRange - closestLightDistance) / messageRevealRange);

                            AddToAlpha(ratio);
                        }
                    }

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

    private void Update()
    {
        for (int i = 0; i < torches.Count; i++)
        {
            bool correctColour = torches[i].GetColorState() == revealWhen.ToString();

            if (correctColour)
            {
                if(torches[i].JustChanged())
                {
                    UpdateMessageReveal(correctColour);
                }
                
            }
            else
            {
                if(torches[i].JustChanged())
                {
                    UpdateMessageReveal(correctColour);
                }
                
            }
        }

        if (alpha > originalAlpha) alpha -= 0.1f * Time.deltaTime;


    }


    private void UpdateMessageReveal(bool add)
    {
        float ratio = ((messageRevealRange - closestLightDistance) / messageRevealRange);

        if (add == true) AddToAlpha(ratio);
        else SubtractFromAlpha(ratio);

        Color newColor = new Color(message.color.r, message.color.g, message.color.b, alpha);

        message.color = newColor;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "LightBulletRange")
        {
            //Debug.Log("IN RANGE!");
            float distance = Distance(other.transform.position);

            if (distance <= messageRevealRange && distance >= 0f)
            {
                    if (other.transform.parent.name == ("LightBullet" + revealWhen.ToString() + "(Clone)"))
                    {                     
                        float ratio = ((messageRevealRange - distance) / messageRevealRange);

                        //Debug.Log(ratio);

                        if (ratio > originalAlpha) alpha = ratio;
                        else alpha = originalAlpha;

                        Color newColor = new Color(message.color.r, message.color.g, message.color.b, alpha);

                        message.color = newColor;
                    }
                }
            }
        }



    private void AddToAlpha(float inNum)
    {
        alpha += inNum;

        if (alpha >= 1f) alpha = 1f;
        else if (alpha <= 0f) alpha = 0f;

        originalAlpha = alpha;

        Debug.Log(name + " OG ALPHA: " + originalAlpha);
    }

    private void SubtractFromAlpha(float inNum)
    {
        alpha -= inNum;

        if (alpha >= 1f) alpha = 1f;
        else if (alpha <= 0f) alpha = 0f;

        originalAlpha = alpha;
    }
 }





