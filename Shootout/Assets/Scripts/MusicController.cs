using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    private AudioSource audioSource;
    public List<AudioClip> clipList;

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
	
	// Update is called once per frame
	void Update () {
		
	}
}
