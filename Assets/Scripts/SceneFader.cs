using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/*
*
*	Scipt Name: SceneFader.cs
*	Script Author: Laurence Valentini
*
*	Script Summary: This script handles the fades between scenes and the splash screen function
*/


public class SceneFader : MonoBehaviour
{


    /*
	*
	*	SERIALIZED FIELDS
	*
	*/

    // reference to the scene faders animator
    [SerializeField]
    private Animator animator;

    // splash screen time (time it stays on screen for)
    [SerializeField]
    private float splashTime = 3f;





    /*
	*
	*	HIDDEN FIELDS
	*
	*/


    // string of the scene to change to
    private string sceneToChangeTo;
    
    // the timer for the splash screen
    private float splashTimer = 0f;






    // this function starts fading the scene using the animator.
    public void FadeIntoScene(string inSceneName)
    {
        sceneToChangeTo = inSceneName;
        animator.SetTrigger("FadeOut");
    }


    // this function is called when an animation is complete and loads a new scene
    public void FadeComplete()
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }


    // this function shows the splash screen
    private void SplashScreen()
    {
        // if the curren scene is the splash screen
        if(SceneManager.GetActiveScene().name.Equals("Splash"))
        {
            // increment the timer
            splashTimer += Time.deltaTime;

            // when the splash screen has been on screen for a set time
            if(splashTimer >= splashTime)
            {
                // fade to the main menu
                FadeIntoScene("MainMenu");
            }
        }
    }





    private void Update()
    {
        SplashScreen();
    }
}
