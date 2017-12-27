using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    private int nextLevel;

    private void Awake()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.transform.position = new Vector3(0, 0, 0);
        player1.GetComponent<HeroController>().enabled = false;
    }

    // Use this for initialization
    void Start () {

        nextLevel = GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel;

        TextMesh textMesh = GameObject.FindGameObjectWithTag("LoadingLevelText").GetComponent<TextMesh>();
        textMesh.text = "Loading Level " + nextLevel;

        GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel++;

        StartCoroutine(DelayedSceneLoadStart());

        //player1.GetComponent<HeroController>().enabled = true;
    }

    IEnumerator DelayedSceneLoadStart()
    {
        yield return new WaitForSeconds(0.1f);
        //SceneManager.LoadScene("LevelScene");// + nextLevel);
        SceneManager.LoadSceneAsync("LevelScene");
    }

    // Update is called once per frame
    void Update () {
		
	}

}
