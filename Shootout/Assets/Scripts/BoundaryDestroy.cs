using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to destroy bullets when boundary is hit
/// </summary>
public class BoundaryDestroy : MonoBehaviour {

    /// <summary>
    /// Destroy bullet when boundary is hit
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
    }
}
