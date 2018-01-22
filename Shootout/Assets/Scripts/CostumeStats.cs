using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeStats : MonoBehaviour {

    public float DefenceMod;
    public float StaminaMod;
    public float SpeedMod;
    public float AttackSpeedMod;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetAll()
    {
        DefenceMod = 0;
        StaminaMod = 0;
        SpeedMod = 0;
        AttackSpeedMod = 0;
    }
}
