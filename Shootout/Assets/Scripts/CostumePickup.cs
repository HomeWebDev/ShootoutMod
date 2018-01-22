using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CostumePickup : MonoBehaviour {

    public Material costumeMaterial;
    public Mesh baseMesh;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameObject player1 = other.gameObject;

            //Find head by reference from player object. Using "Find" by name seems sometimes buggy
            Transform baseTransform = player1.transform.GetChild(0).transform;

            //Change mesh
            SkinnedMeshRenderer skinMesh = baseTransform.GetComponent<SkinnedMeshRenderer>();
            skinMesh.material = costumeMaterial;
            skinMesh.sharedMesh = baseMesh;

            //Copy values from costumes stats to players costumestats
            foreach (FieldInfo f in GetComponent<CostumeStats>().GetType().GetFields())
            {
                f.SetValue(player1.GetComponent<CostumeStats>(), f.GetValue(GetComponent<CostumeStats>()));
            }

            //Destroj jacket
            Destroy(gameObject);
        }
    }
}
