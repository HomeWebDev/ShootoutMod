using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

    private AudioSource audioSource;
    public List<AudioClip> clipList;
    public string UserVolume = "";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        AudioClip clip = clipList[Random.Range(0, clipList.Count)];

        audioSource.loop = true;
        audioSource.volume = 0.2f;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ChangeVolume(Slider vol)
    {
        audioSource.volume = vol.value*0.1f;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
