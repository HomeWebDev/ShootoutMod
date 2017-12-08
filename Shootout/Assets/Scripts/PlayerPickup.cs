using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerPickup : MonoBehaviour {


    public Equipment ActiveGear; 

    void OnTriggerEnter(Collider other)
    {

        ActiveGear = GetComponent<Equipment>();

        if (other.tag.StartsWith("Weapon"))
        {
            if ((ActiveGear.RightHand == null) || (ActiveGear.LeftHand == null))
            {
                return;
            }

            Debug.Log("Weapon: " + gameObject);

            ActiveGear.weapon = other.gameObject;
            Physics.IgnoreCollision(ActiveGear.weapon.GetComponent<Collider>(), GetComponent<Collider>());
            WeaponConfig wc = ActiveGear.weapon.GetComponent<WeaponConfig>();

            Debug.Log("RightHand: " + ActiveGear.RightHand.transform);
            Debug.Log("LeftHand: " + ActiveGear.LeftHand.transform);

            RemoveItemsFrom(ActiveGear.RightHand.transform);
            RemoveItemsFrom(ActiveGear.LeftHand.transform);

            if (ActiveGear.weapon.name.Contains("Longbow"))
            {

                PutItemsPlace(ActiveGear.weapon, ActiveGear.LeftHand.transform, 
                    ActiveGear.weapon.GetComponent<WeaponConfig>().WeaponRotationLeftHand);
            }
            else if (ActiveGear.weapon.GetComponent<WeaponConfig>().RightHandCopy)
            {

            }
            else if (ActiveGear.weapon.GetComponent<WeaponConfig>().LeftHandCopy)
            {

                PutItemsPlace(ActiveGear.weapon, 
                    ActiveGear.LeftHand.transform, 
                    ActiveGear.weapon.GetComponent<WeaponConfig>().WeaponRotationLeftHand);
                PutItemsPlace(CreateWeponCopy(), 
                    ActiveGear.RightHand.transform, 
                    ActiveGear.weapon.GetComponent<WeaponConfig>().WeaponRotationRightHand);
            }
            else if (ActiveGear.weapon.GetComponent<WeaponConfig>().DoubleHanded)
            {

                PutItemsPlace(ActiveGear.weapon, ActiveGear.RightHand.transform, 
                    ActiveGear.weapon.GetComponent<WeaponConfig>().WeaponRotationRightHand);
            }
            else
            {

                PutItemsPlace(ActiveGear.weapon, 
                    ActiveGear.RightHand.transform,
                    ActiveGear.weapon.GetComponent<WeaponConfig>().WeaponRotationRightHand);
            }
        }
        if (other.tag == "BackGear")
        {
            if (ActiveGear.Back == null)
            {
                return;
            }
            ActiveGear.backGear = other.gameObject;

            //RemoveItemsFrom(Back.transform);
            PutItemsPlace(ActiveGear.backGear, ActiveGear.Back.transform, 
                ActiveGear.backGear.GetComponent<GearConfig>().ItemRotation);
        }
        if (other.tag == "HeadGear")
        {
            if (ActiveGear.Head == null)
            {
                return;
            }
            ActiveGear.headGear = other.gameObject;

            //RemoveItemsFrom(Head.transform);

            PutItemsPlace(ActiveGear.headGear, ActiveGear.Head.transform, 
                ActiveGear.headGear.GetComponent<GearConfig>().ItemRotation);
        }
    }

    private void RemoveItemsFrom(Transform LocationTransform)
    {
        //Destroy all held weapons in this hand except the currently picked up (script will trigger twice)
        var children = new List<GameObject>();
        foreach (Transform child in LocationTransform)
        {
            if (child.gameObject != ActiveGear.weapon.gameObject)
            {
                children.Add(child.gameObject);
            }
        }
        children.ForEach(child => Destroy(child));
    }

    private void PutItemsPlace(GameObject bodypart, Transform locationTransform, Vector3 itemRotation)
    {

        bodypart.transform.parent = locationTransform;
        bodypart.transform.position = new Vector3(locationTransform.position.x, 
                                                    locationTransform.position.y, 
                                                    locationTransform.position.z);
        bodypart.transform.localEulerAngles = itemRotation;


    }

    private GameObject CreateWeponCopy()
    {
        Debug.Log("PutCopyInRightHand: ");

        //Add new weapon in right hand
        GameObject newWeapon = Instantiate<GameObject>(ActiveGear.weapon);
        Physics.IgnoreCollision(newWeapon.GetComponent<Collider>(), GetComponent<Collider>());
        return newWeapon;
    }

}