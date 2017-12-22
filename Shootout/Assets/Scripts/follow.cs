using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GameObject g = GameObject.FindWithTag("Player1");

        if(g != null)
        {
            transform.position = new Vector3(g.transform.position.x, transform.position.y, g.transform.position.z);
        }

    }
}
