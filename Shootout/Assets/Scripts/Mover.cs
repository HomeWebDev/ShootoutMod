using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to move bullet
/// </summary>
public class Mover : MonoBehaviour {

    //Set speed of bullet
    public float speed;

    /// <summary>
    /// Instantiate bullet
    /// </summary>
    void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
	
}
