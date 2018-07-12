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

    private GameController gameController;

    /*
	*
	*	HIDDEN FIELDS
	*
	*/


    // string of the scene to change to
    private string sceneToChangeTo;


    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        gameController.SetNewScene(true);
        gameController.NewScene();
    }




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


}
