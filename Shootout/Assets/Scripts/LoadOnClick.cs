using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{

    // Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    //public void LoadScene(int level)
    //{
    //    print("test");
    //    Application.LoadLevel(level);

    //}

    public void LoadScene(string sceneName)
    {
        //Debug.Log(sceneName);

        if (sceneName == "scene4players")
        {
            GameControllerOld.nrOfPlayers = 4;
        }
        if (sceneName == "scene3players")
        {
            GameControllerOld.nrOfPlayers = 3;
        }
        if (sceneName == "scene2players")
        {
            GameControllerOld.nrOfPlayers = 2;
        }
        if (sceneName == "IntroScene")
        {
            //Reset number of kills
            GameControllerOld.Player1Kills = 0;
            GameControllerOld.Player2Kills = 0;
            GameControllerOld.Player3Kills = 0;
            GameControllerOld.Player4Kills = 0;

            UnityEngine.SceneManagement.SceneManager.LoadScene("IntroScene");
        }
        else if (sceneName == "ControlsScene")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ControlsScene");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoaderScene");
        }
    }
}