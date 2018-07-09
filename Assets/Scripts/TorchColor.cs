using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchColor : MonoBehaviour
{
    // the colors to use for the torches
    [SerializeField] private Color redFlame, orangeFlame, blueFlame, greenFlame, yellowFlame, purpleFlame;

    // possible states for the current colour of the torch
    private enum ColorState {Red, Orange, Blue, Green, Yellow, Purple};

    // current color state
    private ColorState currentColorState = ColorState.Orange;

    // mesh renderer of the flame gameobject
    [SerializeField] private MeshRenderer flameMR;


    private void Start()
    {
        CollideWithBullet();
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
                ChangeColor(redFlame);
                break;

            case ColorState.Orange:
                ChangeColor(orangeFlame);
                break;

            case ColorState.Blue:
                ChangeColor(blueFlame);
                break;

            case ColorState.Green:
                ChangeColor(greenFlame);
                break;

            case ColorState.Yellow:
                ChangeColor(yellowFlame);
                break;

            case ColorState.Purple:
                ChangeColor(purpleFlame);
                break;

            default:
                ChangeColor(orangeFlame);
                break;
        }
    }





    // this method changes the color of the torch
    public void ChangeColor(Color inColor)
    {
        // implement color changing behaviour here
        flameMR.material.color = inColor;

    }




    public void CollideWithBullet()
    {
        currentColorState = ColorState.Green;
        // implement bullet collision behaviour here
    }
}
