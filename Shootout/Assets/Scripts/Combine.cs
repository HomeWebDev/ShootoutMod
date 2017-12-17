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
        if (colliders.Length == 0)
        {
            return;
        }
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            //combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            var c = gameObject.AddComponent<BoxCollider>();

            c.center = new Vector3(colliders[i].transform.position.x,
                         colliders[i].transform.position.y,
                         colliders[i].transform.position.z);
            c.size = new Vector3(colliders[i].size.x * 0.3f,
                                   colliders[i].size.y * 0.28f,
                                   colliders[i].size.z * 0.25f);
            i++;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CombineStuff(GameObject MeshList)
    {
        MeshFilter[] meshFilters = MeshList.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        var colliders = MeshList.GetComponentsInChildren<BoxCollider>();
        if (colliders.Length == 0)
        {
            return;
        }
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            var c = MeshList.AddComponent<BoxCollider>();
            var u = new BoxCollider();
            c.center = new Vector3(colliders[i].transform.position.x,
                         colliders[i].transform.position.y,
                         colliders[i].transform.position.z);
            c.size = new Vector3(colliders[i].size.x,
                         colliders[i].size.y,
                         colliders[i].size.z);
            i++;
        }

        MeshList.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        MeshList.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        MeshList.transform.gameObject.SetActive(true);
    }
}
