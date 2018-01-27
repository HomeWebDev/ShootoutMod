using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class HeadPickup : MonoBehaviour
{
    private Transform headTransform;
    private GameObject player1;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            Debug.Log("pickup");

            //GameObject player1 = other.gameObject;

            //Find head by reference from player object. Using "Find" by name seems sometimes buggy
            headTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform;

            //Remove all old back items if any
            foreach (Transform child in headTransform)
            {
                if (child.tag == "HeadGear" && child != transform && child.GetComponent<HeadStats>().Group == GetComponent<HeadStats>().Group)
                {
                    //Debug.Log("delete");
                    Destroy(child.gameObject);
                }
            }

            transform.position = new Vector3(headTransform.position.x, headTransform.position.y, headTransform.position.z);
            transform.localEulerAngles = new Vector3(-90, 0, 0);
            transform.parent = headTransform;

            UpdateHeadStats();

            //int group = GetComponent<HeadStats>().Group;

            ////Copy values from head stats to players headstats
            //foreach (FieldInfo f in GetComponent<HeadStats>().GetType().GetFields())
            //{
            //    f.SetValue(player1.GetComponent<PlayerHeadStats>().HeadStats[group], f.GetValue(GetComponent<HeadStats>()));
            //}

            //foreach (FieldInfo f in GetComponent<HeadStats>().GetType().GetFields())
            //{
            //    f.SetValue(player1.GetComponent<HeadStats>(), f.GetValue(GetComponent<HeadStats>()));
            //}
        }
    }

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");

        UpdateHeadStats();
    }

    private void UpdateHeadStats()
    {
        headTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform;

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
}
