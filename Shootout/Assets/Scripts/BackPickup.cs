using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class BackPickup : MonoBehaviour {

    private Transform backTransform;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameObject player1 = other.gameObject;

            //Find back by reference from player object. Using "Find" by name seems sometimes buggy
            backTransform = player1.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(0).transform;

            //Remove all old back items if any
            foreach (Transform child in backTransform)
            {
                if (child != this.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            transform.position = new Vector3(backTransform.position.x, backTransform.position.y, backTransform.position.z);
            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.parent = backTransform;

            //Copy values from back stats to players backstats
            foreach (FieldInfo f in GetComponent<BackStats>().GetType().GetFields())
            {
                f.SetValue(player1.GetComponent<BackStats>(), f.GetValue(GetComponent<BackStats>()));
            }
        }
    }
}
