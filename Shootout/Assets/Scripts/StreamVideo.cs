using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {
    public RawImage image;
    public VideoClip videoToPlay;
    public AudioClip audioToPlay;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        Application.runInBackground = true;
        GetComponent<RawImage>().color = new Color(0, 0, 0);
        StartCoroutine(PlayVideo());
	}

    IEnumerator PlayVideo()
    {
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        videoPlayer.source = VideoSource.VideoClip;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        audioSource.loop = true;
        audioSource.volume = 1.0f;
        audioSource.clip = audioToPlay;

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.isLooping = true;
        videoPlayer.Prepare();

        //Wait until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(0.4f);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return waitTime;
            break;
        }

        GetComponent<RawImage>().color = new Color(255, 255, 255);

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }
        Debug.Log("Done Playing Video");
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
