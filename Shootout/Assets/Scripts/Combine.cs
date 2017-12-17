using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Combine : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        var colliders = GetComponentsInChildren<BoxCollider>();
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            var c =gameObject.AddComponent<BoxCollider>();
            var u = new BoxCollider();
            c.center = new Vector3(colliders[i].transform.position.x,
                         colliders[i].transform.position.y,
                         colliders[i].transform.position.z);
            c.size = new Vector3(colliders[i].size.x,
                         colliders[i].size.y,
                         colliders[i].size.z);
            i++;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
