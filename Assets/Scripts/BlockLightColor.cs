using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLightColor : MonoBehaviour
{

    // the colors to use for the torches
    [SerializeField]
    private Material redFlame, orangeFlame, blueFlame, greenFlame, yellowFlame, purpleFlame;

    // possible states for the current colour of the torch
    private enum ColorState { Red, Orange, Blue, Green, Yellow, Purple };

    // current color state
    [SerializeField]
    private ColorState currentColorState = ColorState.Orange;

    // mesh renderer of the flame gameobject
    [SerializeField]
    private MeshRenderer flameMR;

    private Material[] flameMRMaterials;
    private Material[] newMaterials;

    [SerializeField]
    private Light flameLight;

    private SphereCollider lightRangeCollider;

    private bool justChanged = false;



    private void Start()
    {
        float lightRange = 15f;

        lightRangeCollider = transform.GetChild(2).GetComponent<SphereCollider>();

        flameLight.range = lightRange;
        lightRangeCollider.radius = lightRange;

        newMaterials = new Material[flameMR.materials.Length];
        
        for(int i=0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = flameMR.materials[i];
        }
    }


    // Update is called once per frame
    void Update()
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
        newMaterials[1] = inMat;

        // implement color changing behaviour here
        flameMR.materials = newMaterials;
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
        if (inState == "LightBulletRed(Clone)") { currentColorState = ColorState.Red; justChanged = true; }
        else if (inState == "LightBulletOrange(Clone)") { currentColorState = ColorState.Orange; justChanged = true; }
        else if (inState == "LightBulletBlue(Clone)") { currentColorState = ColorState.Blue; justChanged = true; }
        else if (inState == "LightBulletGreen(Clone)") { currentColorState = ColorState.Green; justChanged = true; }
        else if (inState == "LightBulletPurple(Clone)") { currentColorState = ColorState.Purple; justChanged = true; }
        else if (inState == "LightBulletYellow(Clone)") { currentColorState = ColorState.Yellow; justChanged = true; }
    }

    public string GetColorState()
    {
        return currentColorState.ToString();
    }

    public bool JustChanged()
    {
        bool changed = justChanged;

        justChanged = false;

        return changed;
    }
}
