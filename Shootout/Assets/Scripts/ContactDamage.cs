using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to handle player damage from bullets
/// </summary>
public class ContactDamage : MonoBehaviour {

    public int damageImpact;
    public bool pickedUp;

    /// <summary>
    /// Handle what should happen when weapon hits object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (pickedUp)
        {
            //Let crates take damage
            if (other.tag == "ItemCrate" | other.tag == "Crate")
            {
                CrateController crateDamage = other.GetComponent<CrateController>();
                crateDamage.TakeDamage(other, damageImpact);
            }
            else if (other.tag == "Enemy")
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                enemy.TakeDamage(damageImpact);
            }

            if(tag == "Arrow" && other.tag == "Boundary")
            {
                Destroy(gameObject);
            }

            if (tag == "Arrow" && (other.tag == "Obstacle" || other.tag == "Enemy"))
            {
                GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
                if(!player1.GetComponent<PlayerPowerups>().PenetratingShot)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
