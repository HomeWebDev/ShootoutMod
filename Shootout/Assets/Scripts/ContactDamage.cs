using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to handle player damage from bullets
/// </summary>
public class ContactDamage : MonoBehaviour {

    public int damageImpact;

    /// <summary>
    /// Handle what should happen when bullet hits object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Ignore walls
        if (other.tag == "Boundary")
        {
            return;
        }

        //Ignore player who shot
        //if (other.tag == this.tag)
        //{
        //    return;
        //}

        //Ignore all players
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            return;
        }

        //Let crates take damage
        if (other.tag == "ItemCrate" | other.tag == "Crate")
        {
            CrateController crateDamage = other.GetComponent<CrateController>();
            crateDamage.TakeDamage(other, 1);
        }
        //Don't destroy power ups
        else if (other.tag == "AKM" || other.tag == "Shield" || other.tag == "HealthPack")
        {
            Destroy(gameObject);
            return;
        }
        //No friendly fire
        else if (other.tag == "Player1" || other.tag == "Player2")
        {
            //PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //playerHealth.TakeDamage(other, 10, gameObject);
        }

        else if(other.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.TakeDamage(damageImpact);
        }

        //Destroy bullet
        Destroy(gameObject);
    }
}
