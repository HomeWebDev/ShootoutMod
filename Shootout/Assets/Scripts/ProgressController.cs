using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProgressController : MonoBehaviour
{
    public int NextLevel = 1;



    //Test for save / load
    private bool alreadyLoaded = false;
    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            //Set ForestCleared to true
            Collectibles.current.collectibleItemsList.Where(item => item.name == "ForestCleared").FirstOrDefault().collected = true;

            SaveLoad.Save();
        }

        if (Input.GetKey(KeyCode.Z))
        {
            //Set ForestCleared to false
            Collectibles.current.collectibleItemsList.Where(item => item.name == "ForestCleared").FirstOrDefault().collected = false;

            SaveLoad.Save();
        }

        if (Input.GetKey(KeyCode.C))
        {
            Debug.Log("ForestCleared: " + Collectibles.current.collectibleItemsList.Where(i => i.name == "ForestCleared").FirstOrDefault().collected);
        }

        if (Input.GetKey(KeyCode.V) && !alreadyLoaded)
        {
            alreadyLoaded = true;
            Debug.Log("Loading");
            SaveLoad.Load();
        }
    }
}
