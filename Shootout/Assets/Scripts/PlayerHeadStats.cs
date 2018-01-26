using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class PlayerHeadStats : MonoBehaviour {

    public List<HeadStats> HeadStats;
    public int NumberOfGroups = 9;

    private void Awake()
    {
        HeadStats = new List<HeadStats>();

        for (int i = 0; i < NumberOfGroups; i++)
        {
            HeadStats.Add(new HeadStats());
            HeadStats[i].SpeedMod = 0;
            HeadStats[i].StaminaMod = 0;
            HeadStats[i].AttackSpeedMod = 0;
            HeadStats[i].DefenceMod = 0;

            //foreach (FieldInfo f in HeadStats[i].GetType().GetFields())
            //{
            //    f.SetValue(HeadStats[i], 0);
            //}
        }
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}
}
