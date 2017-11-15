using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCameraOld : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Shift started");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = new Vector3(1, 1);
        Vector3 newDesiredPosition = transform.position - diff;
        StartCoroutine(LerpFromTo(transform.position, newDesiredPosition, 5f));
        Debug.Log("Shift updated");
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
