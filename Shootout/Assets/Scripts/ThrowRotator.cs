using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRotator : MonoBehaviour {

    private Quaternion rotation;
    private Vector3 radius = new Vector3(2, 1, 0);
    private float currentRotation;
    private Vector3 startPosition;
    private float radiusFloat;
    public float ThrowForce;
    public int Behaviour;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
        currentRotation = 0;

        if (Behaviour == 1)
        {
            radiusFloat = 2;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Behaviour == 0)
        {
            Circular();
        }
        if (Behaviour == 1)
        {
            Spinning();
        }
    }

    private void Spinning()
    {
        float time = Time.deltaTime;
        radiusFloat -= time * 2.0f;
        radius.x = radiusFloat;
        startPosition.x += time * 5.0f;
        currentRotation = (radiusFloat);
        rotation.eulerAngles += new Vector3(0, currentRotation, 0);
        transform.position = startPosition + rotation * radius;

        ////Forward rotate increasing speed
        //startPosition.x += Time.deltaTime * 1.1f;
        //currentRotation += Time.deltaTime * 10 / (rotation.x * rotation.x + 1);
        //rotation.eulerAngles += new Vector3(0, currentRotation, 0);
        //transform.position = startPosition + rotation * radius;



        //float time = Time.deltaTime;
        //radius.x += time * 4.0f;
        //currentRotation -= time * 200;
        //rotation.eulerAngles = new Vector3(0, currentRotation, 0);
        //transform.position = rotation * radius;
        //transform.position = startPosition + rotation * radius;


        //float time = Time.deltaTime;
        ////radiusFloat += time * 2.0f;
        ////radius.x = radiusFloat;
        //currentRotation = 10 / (float)System.Math.Sqrt(radiusFloat);
        //rotation.eulerAngles -= new Vector3(0, currentRotation, 0);
        //transform.position += transform.position + rotation * radius;

        ////Clamp ThrowForce between 1 for shortest and 0 for infinite)
        //float gravityPull = 1.5f - ThrowForce * 0.02f;
        //gravityPull = Mathf.Clamp(gravityPull, 0, 1.5f);
        //transform.position = new Vector3(transform.position.x, transform.position.y - gravityPull, transform.position.z);
        ////Debug.Log("gravityPull: " + gravityPull);
    }

    private void Circular()
    {
        float time = Time.deltaTime;
        radiusFloat += time * 2.0f;
        radius.x = radiusFloat;
        currentRotation = 10 / (float)System.Math.Sqrt(radiusFloat);
        rotation.eulerAngles -= new Vector3(0, currentRotation, 0);
        transform.position = startPosition + rotation * radius;

        //Clamp ThrowForce between 1 for shortest and 0 for infinite)
        float gravityPull = 1.5f - ThrowForce * 0.02f;
        gravityPull = Mathf.Clamp(gravityPull, 0, 1.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y - gravityPull, transform.position.z);
        //Debug.Log("gravityPull: " + gravityPull);
    }
}
