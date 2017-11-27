using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacketPickup : MonoBehaviour {

    private Transform baseTransform;
    private Transform jacketTransform;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            GameObject player1 = other.gameObject;

            //Find head by reference from player object. Using "Find" by name seems sometimes buggy
            baseTransform = player1.transform.GetChild(0).transform;
            jacketTransform = transform.GetChild(0).transform;

            GameObject clothes = Instantiate(Resources.Load("Prefabs/Costume/Female/Knight/Female White Knight 03 White", typeof(GameObject)) as GameObject);

            player1.GetComponent<SkinnedMeshRenderer>().materials = clothes.GetComponent<SkinnedMeshRenderer>().materials;


            //GetComponent<SkinnedMeshRenderer>().materials;




            //player1.renderer.GetComponent<Material>().SetTexture(renderer.GetComponent<Material>().GetTexture()



            //player1.transform.GetChild(0).GetComponent<Material>() = GetComponent<Material>();


            //player1.renderer.material.maintexture = TextureToUse;


            //baseTransform

            //player1.transform.GetChild(0).


            //baseTransform = jacketTransform;

            //transform.position = new Vector3(baseTransform.position.x, baseTransform.position.y, baseTransform.position.z);
            //transform.localEulerAngles = new Vector3(-90, 0, 0);
            //transform.parent = baseTransform;
        }
    }
}
