using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour {

    public float waitTime;
    public float fadeTime;


	// Use this for initialization
	void Start () {

        StartCoroutine(FadeIn());


    }

    IEnumerator FadeIn()
    {
        var g = this.GetComponent<Graphic>();
        g.GetComponent<CanvasRenderer>().SetAlpha(0f);

        yield return new WaitForSeconds(waitTime);

        g.CrossFadeAlpha(1f, fadeTime, false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
