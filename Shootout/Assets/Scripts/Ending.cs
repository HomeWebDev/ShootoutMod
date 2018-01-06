using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {

    private int nextLevel;
    private GameObject player1;

    private void Awake()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.transform.position = new Vector3(0, 0, 0);
        player1.GetComponent<HeroController>().enabled = false;
    }

    // Use this for initialization
    void Start () {
        nextLevel = GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel;

        //TextMesh textMesh = GameObject.FindGameObjectWithTag("EndingText").GetComponent<TextMesh>();
        //textMesh.text = "Some other text";

        StartCoroutine(DelayedSceneLoadStart());
    }

    IEnumerator DelayedSceneLoadStart()
    {
        yield return new WaitForSeconds(5.0f);

        Destroy(player1);
        Destroy(GameObject.FindGameObjectWithTag("ProgressController"));

        SceneManager.LoadSceneAsync("IntroScene");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
