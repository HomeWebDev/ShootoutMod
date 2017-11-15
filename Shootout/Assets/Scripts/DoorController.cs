using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseAllDoors()
    {
        GameObject[] fences = GameObject.FindGameObjectsWithTag("Fence");
    }

    public void OpenAllDoors()
    {

    }
}
