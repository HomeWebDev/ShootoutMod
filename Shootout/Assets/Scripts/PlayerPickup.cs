using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerPickup : MonoBehaviour {

    public GameObject RightHandWepon;
    public GameObject LeftHandWepon;
    public GameObject DoublehandWepon;
    public GameObject RangeWepon;
    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject Head;
    public GameObject Back;


    public int numberOfPickups = 0;
    public bool weaponEQ = false;

    public GameObject weapon = null;
    public GameObject headGear = null;
    public GameObject backGear = null;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Weapon"))
        {
            if ((RightHand == null) || (LeftHand == null))
            {
                return;
            }

            Debug.Log("Weapon: " + gameObject);

            numberOfPickups++;
            //if (numberOfPickups > 1)
            //{
            //    Destroy(GetComponent<WeaponPickup>());
            //}

            weapon = other.gameObject;
            Physics.IgnoreCollision(weapon.GetComponent<Collider>(), GetComponent<Collider>());
            WeaponConfig wc = weapon.GetComponent<WeaponConfig>();

            //rightHandTransform = GameObject.FindWithTag("RightHand").transform;
            //leftHandTransform = GameObject.FindWithTag("LeftHand").transform;

            Debug.Log("RightHand: " + RightHand.transform);
            Debug.Log("LeftHand: " + LeftHand.transform);

            RemoveItemsFrom(RightHand.transform);
            RemoveItemsFrom(LeftHand.transform);

            if (weapon.name.Contains("Longbow"))
            {

                PutItemsPlace(weapon, LeftHand.transform, weapon.GetComponent<WeaponConfig>().WeaponRotationLeftHand);
            }
            else if (weapon.GetComponent<WeaponConfig>().RightHandCopy)
            {

            }
            else if (weapon.GetComponent<WeaponConfig>().LeftHandCopy)
            {

                PutItemsPlace(weapon, LeftHand.transform, weapon.GetComponent<WeaponConfig>().WeaponRotationLeftHand);
                PutItemsPlace(CreateWeponCopy(), RightHand.transform, weapon.GetComponent<WeaponConfig>().WeaponRotationRightHand);
            }
            else if (weapon.GetComponent<WeaponConfig>().DoubleHanded)
            {

                PutItemsPlace(weapon, RightHand.transform, weapon.GetComponent<WeaponConfig>().WeaponRotationRightHand);
            }
            else
            {

                PutItemsPlace(weapon, RightHand.transform, weapon.GetComponent<WeaponConfig>().WeaponRotationRightHand);
                weaponEQ = true;
            }
        }
        if (other.tag == "BackGear")
        {
            if (Back == null)
            {
                return;
            }
            backGear = other.gameObject;

            //RemoveItemsFrom(Back.transform);
            PutItemsPlace(backGear, Back.transform, backGear.GetComponent<GearConfig>().ItemRotation);
        }
        if (other.tag == "HeadGear")
        {
            if (Head == null)
            {
                return;
            }
            headGear = other.gameObject;

            //RemoveItemsFrom(Head.transform);

            PutItemsPlace(headGear, Head.transform, headGear.GetComponent<GearConfig>().ItemRotation);
        }
    }

    private void RemoveItemsFrom(Transform LocationTransform)
    {
        //Destroy all held weapons in this hand except the currently picked up (script will trigger twice)
        var children = new List<GameObject>();
        foreach (Transform child in LocationTransform)
        {
            if (child.gameObject != weapon.gameObject)
            {
                children.Add(child.gameObject);
            }
        }
        children.ForEach(child => Destroy(child));
    }

    private void PutItemsPlace(GameObject bodypart, Transform locationTransform, Vector3 itemRotation)
    {

        bodypart.transform.parent = locationTransform;
        bodypart.transform.position = new Vector3(locationTransform.position.x, locationTransform.position.y, locationTransform.position.z);
        bodypart.transform.localEulerAngles = itemRotation;


    }

    private GameObject CreateWeponCopy()
    {
        Debug.Log("PutCopyInRightHand: ");

        //Add new weapon in right hand
        GameObject newWeapon = Instantiate<GameObject>(weapon);
        Physics.IgnoreCollision(newWeapon.GetComponent<Collider>(), GetComponent<Collider>());
        return newWeapon;
    }

}