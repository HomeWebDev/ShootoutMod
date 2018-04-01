using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettingController : MonoBehaviour {

    public UserSettings Settings;

	// Use this for initialization
	void Start () {
        Settings.EnableLoad = true;
        Settings.EnableSave = true;
        Settings = Settings.Load();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
