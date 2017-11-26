using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPickup : MonoBehaviour
{

    private Transform headTransform;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameObject player1 = other.gameObject;

            //Find head by reference from player object. Using "Find" by name seems sometimes buggy
            headTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform;

            transform.position = new Vector3(headTransform.position.x, headTransform.position.y, headTransform.position.z);
            transform.localEulerAngles = new Vector3(-90, 0, 0);
            transform.parent = headTransform;
        }
    }
}
