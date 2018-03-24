using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

    private AudioSource audioSource;
    private GameObject player1;
    public List<AudioClip> clipList;
    public float FactorBy = 0.1f;
    public string UserVolume = "";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player1 = GameObject.FindGameObjectWithTag("Player1");

        if(audioSource != null)
        {
            audioSource.volume = player1.GetComponent<HeroController>().MusicVolume;
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

        GameObject.FindGameObjectWithTag("Player1").GetComponent<HeroController>().MusicVolume = vol.value * FactorBy;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
