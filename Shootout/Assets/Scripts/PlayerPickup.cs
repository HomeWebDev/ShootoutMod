using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

public class PlayerPickup : MonoBehaviour {


    public Equipment ActiveGear;
    private GameObject player1;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
    }

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

            other.gameObject.GetComponent<ContactDamage>().pickedUp = true;

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
        //if (other.tag == "BackGear")
        //{
        //    if (ActiveGear.Back == null)
        //    {
        //        return;
        //    }
        //    ActiveGear.backGear = other.gameObject;

        //    //RemoveItemsFrom(Back.transform);
        //    PutItemsPlace(ActiveGear.backGear, ActiveGear.Back.transform,
        //        ActiveGear.backGear.GetComponent<GearConfig>().ItemRotation);
        //}
        //if (other.tag == "HeadGear")
        //{
        //    if (ActiveGear.Head == null)
        //    {
        //        return;
        //    }
        //    ActiveGear.headGear = other.gameObject;

        //    //RemoveItemsFrom(Head.transform);

        //    PutItemsPlace(ActiveGear.headGear, ActiveGear.Head.transform,
        //        ActiveGear.headGear.GetComponent<GearConfig>().ItemRotation);
        //}

        if (other.tag == "Powerup")
        {
            //Debug.Log("Powerup");
            //GameObject player1 = GameObject.FindGameObjectWithTag("Player1");

            player1.GetComponent<PowerupStats>().HealthIncrease += other.GetComponent<PowerupStats>().HealthIncrease;
            player1.GetComponent<PlayerHealth>().maxHealth += other.GetComponent<PowerupStats>().HealthIncrease;
            player1.GetComponent<PlayerHealth>().health += other.GetComponent<PowerupStats>().HealthIncrease;
            player1.GetComponent<PlayerHealth>().UpdateStatusBar();

            player1.GetComponent<PowerupStats>().StaminaIncrease += other.GetComponent<PowerupStats>().StaminaIncrease;
            player1.GetComponent<PlayerStamina>().maxStamina += other.GetComponent<PowerupStats>().StaminaIncrease;
            player1.GetComponent<PlayerStamina>().Stamina += other.GetComponent<PowerupStats>().StaminaIncrease;
            player1.GetComponent<PlayerStamina>().UpdateStatusBar();

            player1.GetComponent<PowerupStats>().MovementSpeedIncrease += other.GetComponent<PowerupStats>().MovementSpeedIncrease;
            player1.GetComponent<HeroController>().movementSpeed += other.GetComponent<PowerupStats>().MovementSpeedIncrease;

            player1.GetComponent<PowerupStats>().AttackSpeedIncrease += other.GetComponent<PowerupStats>().AttackSpeedIncrease;
            player1.GetComponent<HeroController>().attackSpeed += other.GetComponent<PowerupStats>().AttackSpeedIncrease;

            player1.GetComponent<PowerupStats>().ThrowForceIncrease += other.GetComponent<PowerupStats>().ThrowForceIncrease;
            player1.GetComponent<HeroController>().ThrowForce += other.GetComponent<PowerupStats>().ThrowForceIncrease;

            player1.GetComponent<PowerupStats>().SizeIncrease += other.GetComponent<PowerupStats>().SizeIncrease;
            Vector3 adjustedScale = new Vector3(player1.transform.localScale.x + other.GetComponent<PowerupStats>().SizeIncrease, player1.transform.localScale.y + other.GetComponent<PowerupStats>().SizeIncrease, player1.transform.localScale.z + other.GetComponent<PowerupStats>().SizeIncrease);
            player1.transform.localScale = adjustedScale;


            player1.GetComponent<PowerupStats>().DoubleShot |= other.GetComponent<PowerupStats>().DoubleShot;
            player1.GetComponent<PowerupStats>().TripleShot |= other.GetComponent<PowerupStats>().TripleShot;
            player1.GetComponent<PowerupStats>().QuadShot |= other.GetComponent<PowerupStats>().QuadShot;
            player1.GetComponent<PowerupStats>().PenetratingShot |= other.GetComponent<PowerupStats>().PenetratingShot;
            player1.GetComponent<PowerupStats>().CircularShot |= other.GetComponent<PowerupStats>().CircularShot;
            player1.GetComponent<PowerupStats>().SpinningShot |= other.GetComponent<PowerupStats>().SpinningShot;


            Destroy(other.gameObject);
        }

        if (other.tag == "HeadGear")
        {
            Debug.Log("Headpickup");
            //Find head by reference from player object. Using "Find" by name seems sometimes buggy
            Transform headTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform;

            //Remove all old head items if any
            foreach (Transform child in headTransform)
            {
                if (child.tag == "HeadGear" && child != other.transform && child.GetComponent<HeadStats>().Group == other.GetComponent<HeadStats>().Group)
                {
                    //Debug.Log("delete");
                    Destroy(child.gameObject);
                }
            }

            other.transform.position = new Vector3(headTransform.position.x, headTransform.position.y, headTransform.position.z);
            other.transform.localEulerAngles = new Vector3(-90, 0, 0);
            other.transform.parent = headTransform;

            UpdateHeadStats();
        }

        if (other.tag == "BackGear")
        {
            //Find back by reference from player object. Using "Find" by name seems sometimes buggy
            Transform backTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(0).transform;

            //Remove all old back items if any
            foreach (Transform child in backTransform)
            {
                if (child != other.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            other.transform.position = new Vector3(backTransform.position.x, backTransform.position.y, backTransform.position.z);
            other.transform.localEulerAngles = new Vector3(0, 0, 0);
            other.transform.parent = backTransform;

            //Copy values from back stats to players backstats
            foreach (FieldInfo f in other.GetComponent<BackStats>().GetType().GetFields())
            {
                f.SetValue(player1.GetComponent<BackStats>(), f.GetValue(other.GetComponent<BackStats>()));
            }
        }

        if (other.tag == "Magic")
        {
            player1.gameObject.GetComponent<MagicStats>().Projectile = other.gameObject.GetComponent<MagicStats>().Projectile;
            player1.gameObject.GetComponent<MagicStats>().Aura = other.gameObject.GetComponent<MagicStats>().Aura;

            Destroy(other.gameObject);
        }

        if (other.tag.StartsWith("Weapon") || other.tag == "BackGear" || other.tag == "HeadGear")
        {
            if (other.gameObject.GetComponent<ItemName>().Group != string.Empty)
            {
                //Rescale weapon to match current player size
                other.transform.localScale = new Vector3(1, 1, 1);
                //Debug.Log("Item rescaled");
                Debug.Log("item: " + other);
            }
        }

        //Remove other items in group
        if (other.tag.StartsWith("Weapon") || other.tag == "BackGear" || other.tag == "HeadGear" || other.tag == "Powerup" || other.tag == "Costume")
        {
            if (other.gameObject.GetComponent<ItemName>().Group != string.Empty)
            {
                //Activate found item canvas
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                gameController.GetComponent<PauseController>().FoundItem(other.gameObject.GetComponent<ItemName>().Name, other.gameObject.GetComponent<ItemName>().Description);

                string group = other.gameObject.GetComponent<ItemName>().Group;
                other.gameObject.GetComponent<ItemName>().Group = string.Empty;

                GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
                GameObject[] backGear = GameObject.FindGameObjectsWithTag("BackGear");
                GameObject[] headGear = GameObject.FindGameObjectsWithTag("HeadGear");
                GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
                GameObject[] costumes = GameObject.FindGameObjectsWithTag("Costume");

                GameObject[] items = weapons.Concat(backGear).ToArray().Concat(headGear).ToArray().Concat(powerups).ToArray().Concat(costumes).ToArray();

                foreach (GameObject item in items)
                {
                    Debug.Log("item: " + item + " group: " + group);
                    try
                    {
                        if (item.GetComponent<ItemName>().Group == group)
                        {
                            Debug.Log("deleteditem: " + item + " group: " + group);
                            Destroy(item);
                        }
                    }
                    catch(Exception ex)
                    { }
                }
            }
        }
    }

    private void UpdateHeadStats()
    {
        Transform headTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform;

        float defenceMod = 0;
        float staminaMod = 0;
        float speedMod = 0;
        float attackSpeedMod = 0;

        foreach (Transform child in headTransform)
        {
            if (child.tag == "HeadGear")
            {
                defenceMod += child.GetComponent<HeadStats>().DefenceMod;
                staminaMod += child.GetComponent<HeadStats>().StaminaMod;
                speedMod += child.GetComponent<HeadStats>().SpeedMod;
                attackSpeedMod += child.GetComponent<HeadStats>().AttackSpeedMod;
            }
        }

        player1.GetComponent<HeadStats>().DefenceMod = defenceMod;
        player1.GetComponent<HeadStats>().StaminaMod = staminaMod;
        player1.GetComponent<HeadStats>().SpeedMod = speedMod;
        player1.GetComponent<HeadStats>().AttackSpeedMod = attackSpeedMod;
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