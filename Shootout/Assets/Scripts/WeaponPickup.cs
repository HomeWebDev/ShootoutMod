using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameObject player1 = GameObject.Find("Player1");
            GameObject leftHand = GameObject.Find("Dummy Prop Left");

            //Destroy all held weapons in this hand except the currently picked up (script will trigger twice)
            var children = new List<GameObject>();
            foreach (Transform child in leftHand.transform)
            {
                if(child.gameObject != gameObject)
                {
                    children.Add(child.gameObject);
                }
            }
            children.ForEach(child => Destroy(child));

            //Add new weapon
            transform.position = new Vector3(leftHand.transform.position.x, leftHand.transform.position.y, leftHand.transform.position.z);
            transform.localEulerAngles = new Vector3(-90, 0, 0);
            transform.parent = leftHand.transform;
        }
    }
}
