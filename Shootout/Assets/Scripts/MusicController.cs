using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

    private AudioSource audioSource;
    public UserSettings Settings;
    private GameObject player1;
    public List<AudioClip> clipList;
    public float FactorBy = 0.1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if(audioSource != null)
        {
            audioSource.volume = Settings.UserSoundSettings.MusicLevel;
        } 
    }

    // Use this for initialization
    void Start () {
        AudioClip clip = clipList[Random.Range(0, clipList.Count)];

        audioSource.loop = true;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ChangeVolume(Slider vol)
    {
        audioSource.volume = vol.value * FactorBy;

        Settings.UserSoundSettings.MusicLevel = vol.value * FactorBy;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
