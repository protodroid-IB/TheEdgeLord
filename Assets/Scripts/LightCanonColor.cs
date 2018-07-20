using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCanonColor : MonoBehaviour
{

    // the colors to use for the torches
    [SerializeField]
    private Material redFlame, orangeFlame, blueFlame, greenFlame, yellowFlame, purpleFlame;

    [SerializeField]
    private Material redBeam, orangeBeam, blueBeam, greenBeam, yellowBeam, purpleBeam;

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

    [SerializeField]
    private LightCanonLightFace lightCanonFaces;



    private void Start()
    {
        float lightRange = 15f;

        lightRangeCollider = transform.GetChild(2).GetComponent<SphereCollider>();

        flameLight.range = lightRange;
        lightRangeCollider.radius = lightRange;

        if(flameMR.materials.Length > 1)
        {
            newMaterials = new Material[flameMR.materials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = flameMR.materials[i];
            }
        }
        else
        {
            newMaterials = new Material[1];

            newMaterials[0] = flameMR.material;
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
                ChangeFlame(redFlame, redBeam);
                break;

            case ColorState.Orange:
                ChangeFlame(orangeFlame, orangeBeam);
                break;

            case ColorState.Blue:
                ChangeFlame(blueFlame, blueBeam);
                break;

            case ColorState.Green:
                ChangeFlame(greenFlame, greenBeam);
                break;

            case ColorState.Yellow:
                ChangeFlame(yellowFlame, yellowBeam);
                break;

            case ColorState.Purple:
                ChangeFlame(purpleFlame, purpleBeam);
                break;

            default:
                ChangeFlame(orangeFlame, orangeBeam);
                break;
        }
    }





    // this method changes the color of the torch
    public void ChangeMaterial(Material inMat)
    {
        if (flameMR.materials.Length > 1)
        {
            newMaterials[1] = inMat;

            flameMR.materials = newMaterials;
        }
        else
        {
            newMaterials[0] = inMat;

            flameMR.material = newMaterials[0];
        }

        
    }

    public void ChangeLightColor(Material inMat)
    {
        flameLight.color = inMat.color;
    }

    public void ChangeFlame(Material inFaceMat, Material inBeamMat)
    {
        ChangeMaterial(inFaceMat);
        ChangeLightColor(inFaceMat);
        
        if(lightCanonFaces.GetBeamActive()) lightCanonFaces.ActivateFaceAndBeam(inFaceMat, inBeamMat);
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
