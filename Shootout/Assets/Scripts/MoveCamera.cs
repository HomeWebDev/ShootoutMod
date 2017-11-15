using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Start of camera");
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 change = new Vector3(10, 0);
        Vector3 newDesiredPosition = transform.position + change;

        StartCoroutine(LerpFromTo(transform.position, newDesiredPosition, 1f));

        Debug.Log("Update of camera");
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(pos1, pos2, t / duration);
            yield return 0;
        }
        transform.position = pos2;
    }
}
