using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRotator : MonoBehaviour {

    private Quaternion rotation;
    private Vector3 radius = new Vector3(0, 1, 0);
    private float currentRotation;
    private Vector3 startPosition;
    private float radiusFloat;
    public float ThrowForce;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        float time = Time.deltaTime;
        radiusFloat += time * 2.0f;
        radius.x = radiusFloat;
        currentRotation = 10 / (float)System.Math.Sqrt(radiusFloat);
        rotation.eulerAngles -= new Vector3(0, currentRotation, 0);
        transform.position = startPosition + rotation * radius;

        //Clamp ThrowForce between 1 for shortest and 0 for infinite)
        float gravityPull = 1.5f - ThrowForce * 0.02f;
        gravityPull =  Mathf.Clamp(gravityPull, 0, 1.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y - gravityPull, transform.position.z);
        Debug.Log("gravityPull: " + gravityPull);
    }
}
