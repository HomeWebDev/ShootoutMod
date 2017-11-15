using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to handle pick up of shield
/// </summary>
public class ShieldPickup : MonoBehaviour {

    /// <summary>
    /// Let player pick up shield when entering shield
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Bolt(Clone)")
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.AddShield(50);
            Destroy(gameObject);
        }
    }
}
