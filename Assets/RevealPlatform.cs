using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealPlatform : MonoBehaviour
{

    [SerializeField]
    private MeshRenderer platformMR;

    private enum ColorState { Red, Orange, Blue, Green, Yellow, Purple, Any };

    [SerializeField]
    private ColorState revealWhen = ColorState.Any;

    [SerializeField]
    private float platformRevealRange = 10f;

    private float platformRevealRate = 1f;

    [SerializeField]
    private bool platformRevealed = false;

    [SerializeField]
    private bool alphaOneToZero = false;

    private float alpha = 0f;


    private List<TorchColor> torchesColor;
    private List<TorchSwitch> torchesSwitch;


    // Use this for initialization
    void Start()
    {
        torchesColor = new List<TorchColor>();
        torchesSwitch = new List<TorchSwitch>();

        if (alphaOneToZero == false) alpha = 0f;
        else alpha = 1f;

        Color newColor = new Color(platformMR.material.color.r, platformMR.material.color.g, platformMR.material.color.b, alpha);

        platformMR.material.color = newColor;
    }






    private void Update()
    {
        if (alphaOneToZero == false) FadeAlphaToZero();
        else FadeAlphaToOne();

        RevealFromTorch();

        ClampAlpha();

        CheckMessageRevealed();

        SetAlpha();

        //Debug.Log(alpha);
    }







    private void FadeAlphaToZero()
    {
        alpha -= platformRevealRate * Time.deltaTime;
    }

    private void FadeAlphaToOne()
    {
        alpha += platformRevealRate * Time.deltaTime;
    }

    private void ClampAlpha()
    {
        if (alpha >= 1f) alpha = 1f;
        if (alpha <= 0f) alpha = 0f;
    }

    private void SetAlpha()
    {
        Color newColor = new Color(platformMR.material.color.r, platformMR.material.color.g, platformMR.material.color.b, alpha);
        Material newMaterial = new Material(platformMR.material);
        newMaterial.name = "INIVISBLE PLATFORM MAT";
        newMaterial.color = newColor;

        platformMR.material = newMaterial;


    }



    private void CheckMessageRevealed()
    {
        if (alpha >= 0.18f) platformRevealed = true;
        else platformRevealed = false;

    }



    public bool Revealed()
    {
        return platformRevealed;
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
    }




    private void RevealFromTorch()
    {
        for (int i = 0; i < torchesColor.Count; i++)
        {
            float torchDistance = Distance(torchesColor[i].transform.position);

            // if torch within distance to reveal message
            if (torchDistance <= platformRevealRange)
            {
                // if the torch is on
                if (torchesSwitch[i].GetTorchState())
                {
                    //Debug.Log("Torch On!");
                    // if the torch is the right colour
                    if (torchesColor[i].GetColorState().Equals(revealWhen.ToString()) || revealWhen == ColorState.Any)
                    {
                        //Debug.Log("Correct Colour!");
                        float distanceRatio = (platformRevealRange - torchDistance) / platformRevealRange;

                        if (alpha <= distanceRatio) alpha += distanceRatio * platformRevealRate * 5f * Time.deltaTime;
                    }
                }
            }
        }
    }



    private void OnTriggerStay(Collider other)
    {
        // if in the light range of a bullet
        if (other.gameObject.tag == "LightBulletRange")
        {
            Debug.Log("COLLIDED!");
            // if the bullet is close enough to the message
            float bulletDistance = Distance(other.transform.parent.position);

            if (bulletDistance <= platformRevealRange)
            {
                string messageColor = "LightBullet" + revealWhen.ToString() + "(Clone)";

                // if the colour of the bullet is equal to the colour needed to reveal the message
                if (messageColor.Equals(other.transform.parent.name) || revealWhen == ColorState.Any)
                {
                    float distanceRatio = (platformRevealRange - bulletDistance) / platformRevealRange;

                    if(alphaOneToZero == false) alpha += distanceRatio * platformRevealRate * 2f * Time.deltaTime;
                    else alpha -= distanceRatio * platformRevealRate * 2f * Time.deltaTime;
                }
            }
        }
    }
}
