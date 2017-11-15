using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to handle crates
/// </summary>
public class CrateController : MonoBehaviour
{
    public int health = 3;

    public Transform AKMSpawn;
    //public Transform HealthSpawn;
    public Transform ShieldSpawn;
    public GameObject Weapon;
    public GameObject HealthPack;
    public GameObject Shield;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Let crate take damage by amount
    /// </summary>
    /// <param name="other"></param>
    /// <param name="amount"></param>
    public void TakeDamage(Collider other, int amount)
    {
        // Decrement the crates health by amount.
        health -= amount;
        int random;

        //Check if crate was destroyed
        if (health <= 0)
        {
            //If item crate spawn weapon, health pack or sheild
            if (other.tag == "ItemCrate")
            {
                random = Random.Range(0,100);
                if (random < 20 )
                {
                    Weapon.tag = "AKM";
                    Vector3 onFLoorPosition = gameObject.transform.position + new Vector3(0, -0.5f, 0);
                    Weapon = Instantiate(Weapon, onFLoorPosition, AKMSpawn.rotation) as GameObject;
                    Weapon.AddComponent<AKMPickUp>();
                    Weapon.AddComponent<BoxCollider>();
                    Weapon.GetComponent<BoxCollider>().isTrigger = true;
                    Weapon.SetActive(true);
                }
                else if (random < 55)
                {
                    HealthPack.tag = "HealthPack";
                    Vector3 onFLoorPosition = gameObject.transform.position + new Vector3(0, -0.3f, 0);
                    HealthPack = Instantiate(HealthPack, onFLoorPosition, transform.rotation ) as GameObject; //HealthSpawn.rotation
                    HealthPack.AddComponent<HeathPackPickup>();
                    HealthPack.AddComponent<BoxCollider>();
                    HealthPack.GetComponent<BoxCollider>().isTrigger = true;
                    HealthPack.SetActive(true);
                }
                else if (random < 85)
                {
                    Shield.tag = "Shield";
                    Vector3 onFLoorPosition = gameObject.transform.position + new Vector3(0, -0.5f, 0);
                    Shield = Instantiate(Shield, onFLoorPosition, ShieldSpawn.rotation) as GameObject;
                    Shield.AddComponent<ShieldPickup>();
                    Shield.AddComponent<BoxCollider>();
                    Shield.GetComponent<BoxCollider>().isTrigger = true;
                    Shield.SetActive(true);
                }
            }

            Destroy(gameObject);
        }

    }
}
