﻿using System.Collections;
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

    [SerializeField]
    private GameObject cage;

    [SerializeField]
    private bool canChangeColour = true;

    private SphereCollider lightRangeCollider;

    private bool justChanged = false;

    private TorchSwitch thisTorch;

    private bool justTurnedOn = false;

    private AudioSource changeSound;

    private void Start()
    {
        float lightRange = 15f;

        lightRangeCollider = transform.GetChild(2).GetComponent<SphereCollider>();

        flameLight.range = lightRange;
        lightRangeCollider.radius = lightRange;

        thisTorch = GetComponent<TorchSwitch>();

        if (canChangeColour == true)
            cage.SetActive(false);

        changeSound = GetComponent<AudioSource>();
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
        if(thisTorch.GetTorchState() == false)
        {
            ChangeMaterial(inMat);
            ChangeLightColor(inMat);
        }
        else
        {
            if (canChangeColour == true)
            {
                ChangeMaterial(inMat);
                ChangeLightColor(inMat);
            }
            else
            {
                if(justTurnedOn == false)
                {
                    ChangeMaterial(inMat);
                    ChangeLightColor(inMat);
                    justTurnedOn = true;
                }
            }
        }   
    }

    

    public void ChangeColorState(string inState)
    {
        if (thisTorch.GetTorchState() == false)
        {
            SwitchToState(inState);
        }
        else
        {
            if (canChangeColour == true)
            {
                SwitchToState(inState);
                changeSound.Play();
            }
            else
            {
                if (justTurnedOn == false)
                {
                    SwitchToState(inState);
                    justTurnedOn = true;
                }
            }
        }
    }

    private void SwitchToState(string inState)
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
