using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public int numberOfPickups = 0;
    public bool leftHandCopy = false;
    public bool rightHandCopy = false;

    private Transform rightHandTransform, leftHandTransform;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            Debug.Log("Weapon: " + gameObject);

            numberOfPickups++;
            if (numberOfPickups > 1)
            {
                Destroy(GetComponent<WeaponPickup>());
            }

            GameObject player1 = other.gameObject;

            //Find hand by reference from player object. Using "Find" by name seems sometimes buggy
            rightHandTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).transform;
            leftHandTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).transform;

            if (gameObject.name.Contains("Longbow"))
            {
                RemoveItemsFromHand(rightHandTransform);
                RemoveItemsFromHand(leftHandTransform);
                PutWeaponInHand(leftHandTransform);
            }
            else if (gameObject.name.Contains("Shield"))
            {
                RemoveItemsFromHand(leftHandTransform);
                PutWeaponInHand(leftHandTransform);
            }
            else if (gameObject.name.Contains("Knuckles") | gameObject.name.Contains("Hand Aura") | gameObject.name.Contains("Claw"))
            {
                if (rightHandCopy)
                {
                    RemoveItemsFromHand(rightHandTransform);
                    PutThisInRightHand();
                }
                else if (leftHandCopy)
                {
                    RemoveItemsFromHand(leftHandTransform);
                    PutThisInLeftHand();
                }
                else
                {
                    RemoveItemsFromHand(leftHandTransform);
                    PutCopyInLeftHand();
                    RemoveItemsFromHand(rightHandTransform);
                    PutCopyInRightHand();
                    Destroy(gameObject);
                }
            }
            else if (gameObject.name.Contains("TH Axe") | gameObject.name.Contains("TH Sword") | gameObject.name.Contains("Spear"))
            {
                RemoveItemsFromHand(rightHandTransform);
                RemoveItemsFromHand(leftHandTransform);
                PutWeaponInHand(rightHandTransform);
            }
            else
            {
                RemoveItemsFromHand(rightHandTransform);
                PutWeaponInHand(rightHandTransform);
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
        if (gameObject.name.Contains("Spear 03")) //This weapon is not rotated as the others
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (gameObject.name.Contains("Crossbow")) //This weapon is not rotated as the others
        {
            transform.localEulerAngles = new Vector3(-90, 0, 180);
        }
        else
        {
            transform.localEulerAngles = new Vector3(-90, 0, 0);
        }
        transform.parent = handTransform;
    }

    private void PutCopyInLeftHand()
    {
        //Add new weapon in left hand
        GameObject copy = Instantiate(gameObject);
        copy.GetComponent<WeaponPickup>().leftHandCopy = true;
        copy.transform.position = new Vector3(leftHandTransform.position.x, leftHandTransform.position.y, leftHandTransform.position.z);
        if (gameObject.name.Contains("Claw") | gameObject.name.Contains("Knuckles"))
        {
            transform.localEulerAngles = new Vector3(-90, 45, 120);
        }
        if (gameObject.name.Contains("Hand Aura"))
        {
            copy.transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        copy.transform.parent = leftHandTransform;
    }

    private void PutThisInLeftHand()
    {
        transform.position = new Vector3(leftHandTransform.position.x, leftHandTransform.position.y, leftHandTransform.position.z);
        if (gameObject.name.Contains("Claw") | gameObject.name.Contains("Knuckles"))
        {
            transform.localEulerAngles = new Vector3(-90, 45, 120);
        }
        if (gameObject.name.Contains("Hand Aura"))
        {
            transform.localEulerAngles = new Vector3(0, 0, 90);
        }
        transform.parent = leftHandTransform;
    }

    private void PutCopyInRightHand()
    {
        //Add new weapon in right hand
        GameObject copy = Instantiate(gameObject);
        copy.GetComponent<WeaponPickup>().rightHandCopy = true;
        copy.transform.position = new Vector3(rightHandTransform.position.x, rightHandTransform.position.y, rightHandTransform.position.z);
        if (gameObject.name.Contains("Claw") | gameObject.name.Contains("Knuckles"))
        {
            copy.transform.localEulerAngles = new Vector3(-90, -45, 60);
        }
        if (gameObject.name.Contains("Hand Aura"))
        {
            copy.transform.localEulerAngles = new Vector3(0, 0, -90);
        }
        copy.transform.parent = rightHandTransform;
    }

    private void PutThisInRightHand()
    {
        transform.position = new Vector3(rightHandTransform.position.x, rightHandTransform.position.y, rightHandTransform.position.z);
        if (gameObject.name.Contains("Claw") | gameObject.name.Contains("Knuckles"))
        {
            transform.localEulerAngles = new Vector3(-90, -45, 60);
        }
        if (gameObject.name.Contains("Hand Aura"))
        {
            transform.localEulerAngles = new Vector3(0, 0, -90);
        }
        transform.parent = rightHandTransform;
    }
}
