using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    public int health = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Let crate take damage by amount
    /// </summary>
    /// <param name="other"></param>
    /// <param name="amount"></param>
    public void TakeOneDamage()
    {
        // Decrement the obstacles health by amount.
        health -= 1;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
 }
