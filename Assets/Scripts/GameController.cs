using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // boolean that checks if the gameobejct this script is attached to exists or not
    private bool gameControllerDoesExist = false;

    [SerializeField]
    private bool disablePlayerInput = false;

    private bool newScene = true;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;

    private Floor00 floor00Script;
    private Floor01 floor01Script;
    private Floor02 floor02Script;
    private Floor03 floor03Script;

    private string lastScene = "Floor00";

    private GameObject playerGO;

    // Use this for initialization
    void Awake ()
    {
        floor00Script = GetComponent<Floor00>();
        floor01Script = GetComponent<Floor01>();
        floor02Script = GetComponent<Floor02>();
        floor03Script = GetComponent<Floor03>();

        CheckGameControllerExists();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(newScene == true)
        {
            
            newScene = false;
        }

        DisablePlayerControlsDebug();

        if (disablePlayerInput == true) fpsController.enabled = false;
        else fpsController.enabled = true;
	}





    public bool GetPlayerInputEnabled()
    {
        return !disablePlayerInput;
    }

    public void SetPlayerInputEnabled(bool inActive)
    {
        disablePlayerInput = !inActive;
    }

    private void EnablePlayerControls()
    {
        SetPlayerInputEnabled(true);
    }

    private void DisablePlayerControlsDebug()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (GetPlayerInputEnabled() == true) SetPlayerInputEnabled(false);
            else SetPlayerInputEnabled(true);
        }
    }









    private void CheckGameControllerExists()
    {
        // if the does not exist
        if (gameControllerDoesExist == false)
        {
            // do not destroy this gameobject when a new scene is loaded
            DontDestroyOnLoad(this.gameObject);

            // this gameobject does exist
            gameControllerDoesExist = true;
        }

        // if more than one of this type of object exists
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // destroy the gameobject
            Destroy(gameObject);
        }
    }


    private void ControlSceneScripts()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Floor00":
                floor00Script.enabled = true;
                floor01Script.enabled = false;
                floor02Script.enabled = false;
                floor03Script.enabled = false;
                FindPlayerPosition(currentScene);
                
                break;

            case "Floor01":
                floor00Script.enabled = false;
                floor01Script.enabled = true;
                floor02Script.enabled = false;
                floor03Script.enabled = false;
                FindPlayerPosition(currentScene);
                break;

            case "Floor02":
                floor00Script.enabled = false;
                floor01Script.enabled = false;
                floor02Script.enabled = true;
                floor03Script.enabled = false;
                FindPlayerPosition(currentScene);
                break;

            case "Floor03":
                floor00Script.enabled = false;
                floor01Script.enabled = false;
                floor02Script.enabled = false;
                floor03Script.enabled = true;
                FindPlayerPosition(currentScene);
                break;
        }
    }

    public void SetNewScene(bool inNewScene)
    {
        newScene = inNewScene;
    }

    public void NewScene()
    {
        playerGO = GameObject.FindWithTag("Player");
        fpsController = playerGO.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        ControlSceneScripts();

        Invoke("EnablePlayerControls", 1.5f);
    }


    public void FindPlayerPosition(string currentScene)
    {
        switch(lastScene)
        {
            case "Floor00":
                if (currentScene == "Floor00") SetPlayerTransform(floor00Script.GetStartTransform());
                else if (currentScene == "Floor01") SetPlayerTransform(floor01Script.GetStartTransform());
                lastScene = "Floor01";
                break;

            case "Floor01":
                if (currentScene == "Floor00") SetPlayerTransform(floor00Script.GetEndTransform());
                else if (currentScene == "Floor02") SetPlayerTransform(floor02Script.GetStartTransform());
                lastScene = "Floor02";
                break;

            case "Floor02":
                if (currentScene == "Floor01") SetPlayerTransform(floor01Script.GetEndTransform());
                else if (currentScene == "Floor03") SetPlayerTransform(floor03Script.GetStartTransform());
                lastScene = "Floor03";
                break;

            case "Floor03":
                if (currentScene == "Floor02") SetPlayerTransform(floor02Script.GetEndTransform());
                lastScene = "Floor04";
                break;
        }
    }

    private void SetPlayerTransform(Transform inTransform)
    {
        playerGO.transform.position = inTransform.position;
        playerGO.transform.rotation = inTransform.rotation;
    }
}
