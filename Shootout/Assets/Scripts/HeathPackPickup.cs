using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to handle pick up of health pack
/// </summary>
public class HeathPackPickup : MonoBehaviour {

    /// <summary>
    /// Let player pick up health pack when entering health pack
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Bolt(Clone)")
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.AddHealth(50);
            Destroy(gameObject);
        }
    }
}
