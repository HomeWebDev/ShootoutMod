using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameObject player1 = other.gameObject;

            //Find right hand by reference from player object. Using "Find" by name seems sometimes buggy
            Transform rightHandTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).transform;

            //Destroy all held weapons in this hand except the currently picked up (script will trigger twice)
            var children = new List<GameObject>();
            foreach (Transform child in rightHandTransform)
            {
                if(child.gameObject != gameObject)
                {
                    children.Add(child.gameObject);
                }
            }
            children.ForEach(child => Destroy(child));

            //Add new weapon
            transform.position = new Vector3(rightHandTransform.position.x, rightHandTransform.position.y, rightHandTransform.position.z);
            transform.localEulerAngles = new Vector3(-90, 0, 0);
            transform.parent = rightHandTransform;
        }
    }
}
