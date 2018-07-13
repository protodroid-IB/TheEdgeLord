using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GunProperties))]
public class LightGun : MonoBehaviour
{
    private GunProperties gunProperties;
    private GunFire gunFire;

    // the colors to use for the bullets
    [SerializeField]
    private Color red, orange, blue, green, yellow, purple;


    [SerializeField]
    private GameObject[] bulletPrefabs;

    private int colorSelected = 0;

    // possible states for the current colour of the torch
    private enum ColorState { Red, Orange, Blue, Green, Yellow, Purple };
    private int colorStateSize;

    // current color state
    private ColorState currentColorState = ColorState.Red;

    private Image uiColorImage;




    // Use this for initialization
    void Start ()
    {
        gunProperties = GetComponent<GunProperties>();
        gunFire = gunProperties.GetGunFire();
        colorStateSize = System.Enum.GetNames(typeof(ColorState)).Length;
        gunProperties.SetBulletPrefab(bulletPrefabs[colorSelected]);
        uiColorImage = GameObject.FindWithTag("BulletColourUI").GetComponent<Image>();
    }



    // Update is called once per frame
    void Update()
    {
        ChangeColorState();
        ColorStateSwitch();
    }




    // this methods switches between torches color states
    private void ColorStateSwitch()
    {
        switch (currentColorState)
        {
            case ColorState.Red:
                ChangeUIColor(red);
                break;

            case ColorState.Orange:
                ChangeUIColor(orange);
                break;

            case ColorState.Blue:
                ChangeUIColor(blue);
                break;

            case ColorState.Green:
                ChangeUIColor(green);
                break;

            case ColorState.Yellow:
                ChangeUIColor(yellow);
                break;

            case ColorState.Purple:
                ChangeUIColor(purple);
                break;

            default:
                ChangeUIColor(red);
                break;
        }
    }




    private void ChangeUIColor(Color inColor)
    {
        if(uiColorImage.color != inColor)
        {
            uiColorImage.color = inColor;
        }
    }


    private void ChangeColorState()
    {
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        //  scroll up
        if(mouseWheel > 0f)
        {
            colorSelected++;
        }

        // scroll down
        if(mouseWheel < 0f)
        {
            colorSelected--;        
        }

        if (colorSelected % colorStateSize == 0) colorSelected = 0;

        if (colorSelected < 0) colorSelected = colorStateSize + colorSelected;

        currentColorState = (ColorState)colorSelected;

        gunProperties.SetBulletPrefab(bulletPrefabs[colorSelected]);
    }
}
