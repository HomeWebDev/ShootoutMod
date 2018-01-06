﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftLevel : MonoBehaviour {

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

            if(nextLevel == 5)
            {
                SceneManager.LoadScene("LoadLevelScene");
            }
            else
            {
                SceneManager.LoadScene("EndingScene");
            }
        }  
    }

}
