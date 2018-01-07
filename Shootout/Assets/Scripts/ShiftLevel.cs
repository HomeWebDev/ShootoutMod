using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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

            if(nextLevel == 6)
            {
                //Set ForestCleared to true
                Collectibles.current.collectibleItemsList.Where(i => i.name == "ForestCleared").FirstOrDefault().collected = true;
                SaveLoad.Save();

                SceneManager.LoadScene("EndingScene");
            }
            else
            {
                SceneManager.LoadScene("LoadLevelScene");
            }
        }  
    }

}
