using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class ShiftHiddenLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            other.GetComponent<HeroController>().enabled = false;

            int nextLevel = GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel;
            
            if (nextLevel == 4)
            {
                //Path from Forest to Desert
                GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel = 7;
            }
            if (nextLevel == 5)
            {
                //Path from Forest to Castle Grounds
                GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel = 13;
            }
            if (nextLevel == 3)
            {
                //Shortcut from Forest to Castle
                GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel = 24;
            }
            if (nextLevel == 9)
            {
                //Path from Desert to Mountain
                GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel = 27;
            }
            if (nextLevel == 30)
            {
                //Path from Mountain to Hell, currently no change of nextlevel
                //GameObject.FindGameObjectWithTag("ProgressController").GetComponent<ProgressController>().NextLevel = 30;
            }

            SceneManager.LoadScene("LoadLevelScene");
        }
    }
}
