using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControllerOld : MonoBehaviour {

    public static int Player1Kills;
    public static int Player2Kills;
    public static int Player3Kills;
    public static int Player4Kills;

    public static int nrOfPlayers;

    public int player1HealthValue;
    public int player2HealthValue;
    public int player3HealthValue;
    public int player4HealthValue;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private UnityEngine.UI.Text player1KillsText;
    private UnityEngine.UI.Text player2KillsText;
    private UnityEngine.UI.Text player3KillsText;
    private UnityEngine.UI.Text player4KillsText;

    public GameObject crate1;
    public GameObject crate2;
    public GameObject crate3;
    public GameObject crate4;
    public Transform crateTransform;

    // Use this for initialization
    void Start () {

        //player1Clone = Instantiate(player1, new Vector3(9, 0, -9), Quaternion.identity) as GameObject;
        //player2Clone = Instantiate(player2, new Vector3(-9, 0, 9), Quaternion.identity) as GameObject;
        //player3Clone = Instantiate(player3, new Vector3(-9, 0, -9), Quaternion.identity) as GameObject;
        //player4Clone = Instantiate(player4, new Vector3(9, 0, 9),  Quaternion.identity) as GameObject;

        //player1Health.health = player1HealthValue;

        //guiText = GameObject.Find("GUI_TEXT_NAME").guiText;

        //guiText = GetComponent<GUIText>;

        GameObject player1KillsTextGameObject = GameObject.Find("Player1Kills");
        player1KillsText = player1KillsTextGameObject.GetComponent<UnityEngine.UI.Text>();

        GameObject player2KillsTextGameObject = GameObject.Find("Player2Kills");
        player2KillsText = player2KillsTextGameObject.GetComponent<UnityEngine.UI.Text>();

        if (nrOfPlayers > 2)
        {
            GameObject player3KillsTextGameObject = GameObject.Find("Player3Kills");
            player3KillsText = player3KillsTextGameObject.GetComponent<UnityEngine.UI.Text>();
        }

        //Spawn random crates over playing field
        SpawnRandomCrates();
    }

    private void SpawnRandomCrates()
    {
        
        //crate1.tag = "ItemCrate";
        //Vector3 onFLoorPosition = gameObject.transform.position + new Vector3(0, -0.5f, 0);

        List<Vector3> vectorList = new List<Vector3>();

        for (int i = 0; i < 100; i++)
        {
            int randx;
            int randz;

            while (true)
            {
                randx = Random.Range(-14, 14);
                randz = Random.Range(-14, 14);

                if (!(Mathf.Abs(randx) > 8 & Mathf.Abs(randz) > 8))
                {
                    break;
                }
            }

            Vector3 tempVector = new Vector3(randx, 0.5f, randz);
            vectorList.Add(tempVector);
        }

        foreach (Vector3 position in vectorList)
        {
            int type = Random.Range(0, 100);
            if (type < 32)
            {
                GameObject crate = Instantiate(crate1, position, crateTransform.rotation) as GameObject;
            }
            else if (type < 55)
            {
                GameObject crate = Instantiate(crate3, position, crateTransform.rotation) as GameObject;
            }
            else if (type < 80)
            {
                GameObject crate = Instantiate(crate4, position, crateTransform.rotation) as GameObject;
            }
            else
            {
                GameObject crate = Instantiate(crate2, position, crateTransform.rotation) as GameObject;
            }
        }

        //Vector3 onFLoorPosition1 = new Vector3(5, 0.5f, 5);
        //Vector3 onFLoorPosition2 = new Vector3(-5, 0.5f, -5);

        //crate = Instantiate(crate, onFLoorPosition1, crateTransform.rotation) as GameObject;
        //crate = Instantiate(crate, onFLoorPosition2, crateTransform.rotation) as GameObject;
        //CrateItem.AddComponent<BoxCollider>();
        //crate.AddComponent<AKMPickUp>();
        //crate.AddComponent<BoxCollider>();
        //crate.GetComponent<BoxCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update () {
        UpdateKillCount();
    }

    private void UpdateKillCount()
    {
        player1KillsText.text = "Kills: " + Player1Kills;
        player2KillsText.text = "Kills: " + Player2Kills;
        if (nrOfPlayers > 2)
        {
            player3KillsText.text = "Kills: " + Player3Kills;

        }
        if (nrOfPlayers > 3)
        {
            player4KillsText.text = "Kills: " + Player4Kills;
        }
    }

    public void KillPlayer(int playerID)
    {
        print("Player killed:" + Time.time);
    }
}
