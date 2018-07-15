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

    private float messageRevealRate = 1f;

    [SerializeField]
    private bool messageRevealed = false;


    private float alpha = 0f;


    private List<TorchColor> torchesColor;
    private List<TorchSwitch> torchesSwitch;

    private List<BlockLightColor> blocksColor;


	// Use this for initialization
	void Start ()
    {
        torchesColor = new List<TorchColor>();
        torchesSwitch = new List<TorchSwitch>();

        blocksColor = new List<BlockLightColor>();

        message = transform.GetChild(0).GetComponent<Text>();

        alpha = 0f;

        Color newColor = new Color(message.color.r, message.color.g, message.color.b, alpha);

        message.color = newColor;
    }






    private void Update()
    {
        FadeAlphaToZero();

        RevealFromTorch();

        RevealFromBlock();

        ClampAlpha();

        CheckMessageRevealed();

        SetAlpha();
    }







    private void FadeAlphaToZero()
    {
        alpha -= messageRevealRate * Time.deltaTime;
    }

    private void ClampAlpha()
    {
        if (alpha >= 1f) alpha = 1f;
        if (alpha <= 0f) alpha = 0f;
    }

    private void SetAlpha()
    {
        Color newColor = new Color(message.color.r, message.color.g, message.color.b, alpha);

        message.color = newColor;
    }



    private void CheckMessageRevealed()
    {
        if (alpha >= 0.18f) messageRevealed = true;
        else messageRevealed = false;

    }



    public bool Revealed()
    {
        return messageRevealed;
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












    private void OnTriggerEnter(Collider other)
    {
        // collided with torch range collider
        if (other.gameObject.tag == "LightRangeCollider")
        {
            // add both torch scripts to lists for later use
            TorchColor thisTorchColor = other.transform.parent.GetComponent<TorchColor>();
            TorchSwitch thisTorchSwitch = other.transform.parent.GetComponent<TorchSwitch>();

            torchesColor.Add(thisTorchColor);
            torchesSwitch.Add(thisTorchSwitch);
        }

        // collided with torch range collider
        if (other.gameObject.tag == "LightRangeColliderBlock")
        {
            // add both torch scripts to lists for later use
            BlockLightColor thisBlockColor = other.transform.parent.GetComponent<BlockLightColor>();

            blocksColor.Add(thisBlockColor);
        }




    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LightRangeCollider")
        {
            // add both torch scripts to lists for later use
            TorchColor thisTorchColor = other.transform.parent.GetComponent<TorchColor>();
            TorchSwitch thisTorchSwitch = other.transform.parent.GetComponent<TorchSwitch>();

            torchesColor.Remove(thisTorchColor);
            torchesSwitch.Remove(thisTorchSwitch);
        }

        // collided with torch range collider
        if (other.gameObject.tag == "LightRangeColliderBlock")
        {
            // add both torch scripts to lists for later use
            BlockLightColor thisBlockColor = other.transform.parent.GetComponent<BlockLightColor>();

            blocksColor.Remove(thisBlockColor);
        }
    }




    private void RevealFromTorch()
    {
        for(int i=0; i < torchesColor.Count; i++)
        {
            float torchDistance = Distance(torchesColor[i].transform.position);

            // if torch within distance to reveal message
            if(torchDistance <= messageRevealRange)
            {
                //Debug.Log("In Range!");
                // if the torch is on
                if(torchesSwitch[i].GetTorchState())
                {
                    //Debug.Log("Torch On!");
                    // if the torch is the right colour
                    if (torchesColor[i].GetColorState().Equals(revealWhen.ToString()) || revealWhen == ColorState.Any)
                    {
                        //Debug.Log("Correct Colour!");
                        float distanceRatio = (messageRevealRange - torchDistance) / messageRevealRange;

                        if(alpha <= distanceRatio) alpha += distanceRatio * messageRevealRate * 5f * Time.deltaTime;
                    }
                }
            }
        }
    }



    private void RevealFromBlock()
    {
        for (int i = 0; i < blocksColor.Count; i++)
        {
            float blockDistance = Distance(blocksColor[i].transform.position);

            // if block within distance to reveal message
            if (blockDistance <= messageRevealRange)
            {
                // if the block is the right colour
                if (blocksColor[i].GetColorState().Equals(revealWhen.ToString()) || revealWhen == ColorState.Any)
                {
                    float distanceRatio = (messageRevealRange - blockDistance) / messageRevealRange;

                    if (alpha <= distanceRatio) alpha += distanceRatio * messageRevealRate * 5f * Time.deltaTime;
                }
            }
        }
    }






    private void OnTriggerStay(Collider other)
    {
        // if in the light range of a bullet
        if(other.gameObject.tag == "LightBulletRange")
        {
            // if the bullet is close enough to the message
            float bulletDistance = Distance(other.transform.parent.position);

            if (bulletDistance <= messageRevealRange)
            {
                string messageColor = "LightBullet" + revealWhen.ToString() + "(Clone)";

                // if the colour of the bullet is equal to the colour needed to reveal the message
                if (messageColor.Equals(other.transform.parent.name) || revealWhen == ColorState.Any)
                {
                    float distanceRatio = (messageRevealRange - bulletDistance) / messageRevealRange;

                    alpha += distanceRatio * messageRevealRate * 2f * Time.deltaTime;
                }
            }
        }
    }





    private void PrintList(List<TorchColor> t)
    {
        for(int i=0; i < t.Count; i++)
        {
            Debug.Log(t[i].name);
        }
    }

    private void PrintList(List<TorchSwitch> t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            Debug.Log(t[i].name);
        }
    }


}





