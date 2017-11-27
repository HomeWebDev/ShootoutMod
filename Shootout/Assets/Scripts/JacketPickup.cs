using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacketPickup : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameObject player1 = other.gameObject;

            //Find head by reference from player object. Using "Find" by name seems sometimes buggy
            Transform baseTransform = player1.transform.GetChild(0).transform;

            //Change mesh
            SkinnedMeshRenderer skinMesh = baseTransform.GetComponent<SkinnedMeshRenderer>();
            skinMesh.material = Resources.Load("Materials/Female White Knight 03 White", typeof(Material)) as Material;

            //Destroj jacket
            Destroy(gameObject);
        }
    }
}
