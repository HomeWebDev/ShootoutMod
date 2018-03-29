using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    private int nextLevel;
    private GameObject player1;

    private List<string> levelStringList = new List<string> { "Grass Plains", "Flower Field", "Friendly Forest", "Stony Forest", "Autumn", "Late Autumn",
                                                              "Stone and Sand", "Sand", "More Sand", "Desert", "Drought", "Metal", "Pavement", "More Pavement",
                                                              "Rock", "Tiles", "Stones", "Cracked Stone", "Less Cracked Stone", "Stone", "Dirt", "Ash",
                                                              "Indoor Pavement", "Further Indoor Pavement", "Bricks", "Indoor", "Snow", "Ice", "Water", "Lava"};

    private void Awake()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.transform.position = new Vector3(0, 0, 0);
        player1.GetComponent<HeroController>().enabled = false;
    }

    // Use this for initialization
    void Start () {

        nextLevel = GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel;

        TextMesh textMesh = GameObject.FindGameObjectWithTag("LoadingLevelText").GetComponent<TextMesh>();
        //textMesh.text = "Loading Level " + nextLevel;
        textMesh.text = levelStringList[nextLevel-1];

        GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel++;

        StartCoroutine(DelayedSceneLoadStart());

        //player1.GetComponent<HeroController>().enabled = true;
    }

    IEnumerator DelayedSceneLoadStart()
    {
        yield return new WaitForSeconds(0.1f);

        player1.GetComponent<PlayerStamina>().SetMaxStamina();
        player1.GetComponent<PlayerMagic>().SetMaxMagic();

        SceneManager.LoadSceneAsync("LevelScene");
    }

    // Update is called once per frame
    void Update () {
		
	}

}
