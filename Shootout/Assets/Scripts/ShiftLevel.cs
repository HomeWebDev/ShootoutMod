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
            else if (nextLevel == 11)
            {
                //Set ForestCleared to true
                Collectibles.current.collectibleItemsList.Where(i => i.name == "DesertCleared").FirstOrDefault().collected = true;
                SaveLoad.Save();

                SceneManager.LoadScene("EndingScene");
            }
            else if (nextLevel == 17)
            {
                //Set ForestCleared to true
                Collectibles.current.collectibleItemsList.Where(i => i.name == "CastleGroundsCleared").FirstOrDefault().collected = true;
                SaveLoad.Save();

                SceneManager.LoadScene("EndingScene");
            }
            else if (nextLevel == 27)
            {
                //Set ForestCleared to true
                Collectibles.current.collectibleItemsList.Where(i => i.name == "CastleCleared").FirstOrDefault().collected = true;
                SaveLoad.Save();

                SceneManager.LoadScene("EndingScene");
            }
            else if (nextLevel == 30)
            {
                //Set ForestCleared to true
                Collectibles.current.collectibleItemsList.Where(i => i.name == "MountainCleared").FirstOrDefault().collected = true;
                SaveLoad.Save();

                SceneManager.LoadScene("EndingScene");
            }
            else if (nextLevel == 31)
            {
                //Set ForestCleared to true
                Collectibles.current.collectibleItemsList.Where(i => i.name == "HellCleared").FirstOrDefault().collected = true;
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
