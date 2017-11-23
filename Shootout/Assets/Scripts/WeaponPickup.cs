using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            Debug.Log("Weapon: " + gameObject);

            GameObject player1 = other.gameObject;

            //Find hand by reference from player object. Using "Find" by name seems sometimes buggy
            Transform rightHandTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).transform;
            Transform leftHandTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).transform;

            if (gameObject.name.Contains("Longbow"))
            {
                RemoveItemsFromHand(rightHandTransform);
                RemoveItemsFromHand(leftHandTransform);
                PutWeaponInHand(leftHandTransform);
            }
            if (gameObject.name.Contains("Knuckles") | gameObject.name.Contains("Hand Aura") | gameObject.name.Contains("Claw"))
            {
                RemoveItemsFromHand(rightHandTransform);
                RemoveItemsFromHand(leftHandTransform);
                PutHandWeaponInHand(leftHandTransform);
                PutCopyInHand(rightHandTransform);
                Destroy(this);
            }
            else
            {
                //Destroy all held weapons in right hand except the currently picked up (script will trigger twice)
                var children = new List<GameObject>();
                foreach (Transform child in rightHandTransform)
                {
                    if (child.gameObject != gameObject)
                    {
                        children.Add(child.gameObject);
                    }
                }
                //children.ForEach(child => Destroy(child));

                //Destroy any bow in left hand
                foreach (Transform child in leftHandTransform)
                {
                    if (child.gameObject != gameObject && child.gameObject.name.Contains("Longbow"))
                    {
                        children.Add(child.gameObject);
                    }
                }
                children.ForEach(child => Destroy(child));

                //Add new weapon
                transform.position = new Vector3(rightHandTransform.position.x, rightHandTransform.position.y, rightHandTransform.position.z);
                if (gameObject.name.Contains("Spear 03")) //This weapon is not rotated as the others
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                if (gameObject.name.Contains("Crossbow")) //This weapon is not rotated as the others
                {
                    transform.localEulerAngles = new Vector3(-90, 0, 180);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(-90, 0, 0);
                }
                transform.parent = rightHandTransform;
            }
        }
    }

    private void RemoveItemsFromHand(Transform handTransform)
    {
        //Destroy all held weapons in this hand except the currently picked up (script will trigger twice)
        var children = new List<GameObject>();
        foreach (Transform child in handTransform)
        {
            if (child.gameObject != gameObject)
            {
                children.Add(child.gameObject);
            }
        }
        children.ForEach(child => Destroy(child));
    }

    private void PutWeaponInHand(Transform handTransform)
    {
        //Add new weapon
        transform.position = new Vector3(handTransform.position.x, handTransform.position.y, handTransform.position.z);
        transform.localEulerAngles = new Vector3(-90, 0, 0);
        transform.parent = handTransform;
    }

    private void PutHandWeaponInHand(Transform handTransform)
    {
        //Add new weapon
        transform.position = new Vector3(handTransform.position.x, handTransform.position.y, handTransform.position.z);
        transform.localEulerAngles = new Vector3(0, -90, -45);
        transform.parent = handTransform;
    }

    private void PutCopyInHand(Transform handTransform)
    {
        //Add new weapon
        GameObject copy = Instantiate(gameObject);
        copy.transform.position = new Vector3(handTransform.position.x, handTransform.position.y, handTransform.position.z);
        copy.transform.localEulerAngles = new Vector3(0, -90, 225);
        copy.transform.parent = handTransform;
    }
}
