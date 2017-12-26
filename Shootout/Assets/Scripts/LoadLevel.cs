using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    private int nextLevel;

    // Use this for initialization
    void Start () {
        nextLevel = GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel;

        Text text = GameObject.FindGameObjectWithTag("LoadingLevelText").GetComponent<Text>();
        text.text = "Loading Level " + nextLevel;

        GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel++;

        StartCoroutine(DelayedSceneLoadStart());
    }

    IEnumerator DelayedSceneLoadStart()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("LevelScene" + nextLevel);
    }

    // Update is called once per frame
    void Update () {
		
	}

}
