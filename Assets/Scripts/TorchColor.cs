using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchColor : MonoBehaviour
{
    // the colors to use for the torches
    [SerializeField] private Material redFlame, orangeFlame, blueFlame, greenFlame, yellowFlame, purpleFlame;

    // possible states for the current colour of the torch
    private enum ColorState {Red, Orange, Blue, Green, Yellow, Purple};

    // current color state
    [SerializeField]
    private ColorState currentColorState = ColorState.Orange;

    // mesh renderer of the flame gameobject
    [SerializeField] private MeshRenderer flameMR;

    [SerializeField] private Light flameLight;

    private SphereCollider lightRangeCollider;


    private void Start()
    {
        float lightRange = 15f;

        lightRangeCollider = transform.GetChild(2).GetComponent<SphereCollider>();

        flameLight.range = lightRange;
        lightRangeCollider.radius = lightRange;
    }


    // Update is called once per frame
    void Update ()
    {
        ColorStateSwitch();
    }




    // this methods switches between torches color states
    private void ColorStateSwitch()
    {
        switch (currentColorState)
        {
            case ColorState.Red:
                ChangeFlame(redFlame);
                break;

            case ColorState.Orange:
                ChangeFlame(orangeFlame);
                break;

            case ColorState.Blue:
                ChangeFlame(blueFlame);
                break;

            case ColorState.Green:
                ChangeFlame(greenFlame);
                break;

            case ColorState.Yellow:
                ChangeFlame(yellowFlame);
                break;

            case ColorState.Purple:
                ChangeFlame(purpleFlame);
                break;

            default:
                ChangeFlame(orangeFlame);
                break;
        }
    }





    // this method changes the color of the torch
    public void ChangeMaterial(Material inMat)
    {
        // implement color changing behaviour here
        flameMR.material = inMat;
    }

    public void ChangeLightColor(Material inMat)
    {
        flameLight.color = inMat.color;
    }

    public void ChangeFlame(Material inMat)
    {
        ChangeMaterial(inMat);
        ChangeLightColor(inMat);
    }

    

    public void ChangeColorState(string inState)
    {
        if (inState == "LightBulletRed(Clone)") currentColorState = ColorState.Red;
        else if (inState == "LightBulletOrange(Clone)") currentColorState = ColorState.Orange;
        else if (inState == "LightBulletBlue(Clone)") currentColorState = ColorState.Blue;
        else if (inState == "LightBulletGreen(Clone)") currentColorState = ColorState.Green;
        else if (inState == "LightBulletPurple(Clone)") currentColorState = ColorState.Purple;
        else if (inState == "LightBulletYellow(Clone)") currentColorState = ColorState.Yellow;
    }


}
