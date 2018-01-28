using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class HeadStatsHandler : MonoBehaviour {

    public float DefenceMod;
    public float StaminaMod;
    public float SpeedMod;
    public float AttackSpeedMod;

    private Transform headTransform;

    // Use this for initialization
    void Start () {
        headTransform = transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform;
    }
	
	// Update is called once per frame
	void Update () {

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

        DefenceMod = defenceMod;
        StaminaMod = staminaMod;
        SpeedMod = speedMod;
        AttackSpeedMod = attackSpeedMod;
    }
}
