using UnityEngine;
using System.Collections;

public class AKMPickUp : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Bolt(Clone)")
        {
            //other.GetComponent<Animation_Controller>().weapon = new GameObject();
            other.GetComponent<Animation_Controller>().akm.SetActive(true);
            other.GetComponent<Animation_Controller>().fireRate -= (float)0.05;
            Destroy(gameObject);
        }
    }
}
