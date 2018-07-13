using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class FloorChanges : MonoBehaviour
{


    private GameObject playerGO;

    private Floor00 floor00Script;
    private Floor01 floor01Script;
    private Floor02 floor02Script;
    private Floor03 floor03Script;

    private string lastScene = "Floor00";

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;

    private GameController gameController;






    // Use this for initialization
    void Awake()
    {
        floor00Script = GetComponent<Floor00>();
        floor01Script = GetComponent<Floor01>();
        floor02Script = GetComponent<Floor02>();
        floor03Script = GetComponent<Floor03>();

        NewScene();
    }






    public void NewScene()
    {
        playerGO = GameObject.FindWithTag("Player");
        fpsController = playerGO.GetComponent<FirstPersonController>();
        gameController = GetComponent<GameController>();
        
        gameController.SetFPSController(fpsController);

        ControlSceneScripts();

        Invoke("EnablePlayerControls", 1.5f);
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
                floor00Script.GrabPlayerSpawnPositions();
                FindPlayerPosition(currentScene);

                break;

            case "Floor01":
                floor00Script.enabled = false;
                floor01Script.enabled = true;
                floor02Script.enabled = false;
                floor03Script.enabled = false;
                floor01Script.GrabPlayerSpawnPositions();
                FindPlayerPosition(currentScene);
                break;

            case "Floor02":
                floor00Script.enabled = false;
                floor01Script.enabled = false;
                floor02Script.enabled = true;
                floor03Script.enabled = false;
                floor02Script.GrabPlayerSpawnPositions();
                FindPlayerPosition(currentScene);
                break;

            case "Floor03":
                floor00Script.enabled = false;
                floor01Script.enabled = false;
                floor02Script.enabled = false;
                floor03Script.enabled = true;
                floor03Script.GrabPlayerSpawnPositions();
                FindPlayerPosition(currentScene);
                break;
        }
    }









    public void FindPlayerPosition(string currentScene)
    {
        switch (lastScene)
        {
            case "Floor00":
                if (currentScene == "Floor00")
                {
                    SetPlayerTransform(floor00Script.GetStartTransform());
                    lastScene = "Floor00";
                }
                else if (currentScene == "Floor01")
                {
                    SetPlayerTransform(floor01Script.GetStartTransform());
                    lastScene = "Floor01";
                }
                break;

            case "Floor01":
                if (currentScene == "Floor00")
                {
                    SetPlayerTransform(floor00Script.GetEndTransform());
                    lastScene = "Floor00";
                }
                else if (currentScene == "Floor02")
                {
                    SetPlayerTransform(floor02Script.GetStartTransform());
                    lastScene = "Floor02";
                }
                break;

            case "Floor02":
                if (currentScene == "Floor01")
                {
                    SetPlayerTransform(floor01Script.GetEndTransform());
                    lastScene = "Floor01";
                }
                else if (currentScene == "Floor03")
                {
                    SetPlayerTransform(floor03Script.GetStartTransform());
                    lastScene = "Floor03";
                }
                break;

            case "Floor03":
                if (currentScene == "Floor02")
                {
                    SetPlayerTransform(floor02Script.GetEndTransform());
                    lastScene = "Floor02";
                }
                break;
        }
    }





    private void SetPlayerTransform(Transform inTransform)
    {
        playerGO.transform.position = inTransform.position;
        playerGO.transform.rotation = inTransform.rotation;
    }



    private void EnablePlayerControls()
    {
        gameController.SetPlayerInputEnabled(true);
    }
}
