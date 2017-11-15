using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public GameObject crate1;

    // Use this for initialization
    void Start () {

        GenerateLevel();

        SceneManager.LoadScene("LevelScene");
    }

	private void GenerateLevel()
    {
        GameObject crate = Instantiate(crate1, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    // Update is called once per frame
    void Update () {
		
	}

}
