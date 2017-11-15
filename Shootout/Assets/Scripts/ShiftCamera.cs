using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftCamera : MonoBehaviour {

    public int xShift;
    public int zShift;
    private bool buzyShifting = false;

    private void OnTriggerEnter(Collider other)

    {
        //Debug.Log("EnterTrigger");
        GameObject player1 = GameObject.Find("Player1");
        GameObject player2 = GameObject.Find("Player2");

        //var pos = player1.transform.position;
        //pos.z += 1;
        //player1.transform.position = pos;

        //Debug.Log("EnterTrigger, shouldMove = " + shouldShiftCamera(other) + " zShift = " + zShift);

        if ((other.tag == "Player1" || other.tag == "Player2") && ShouldShiftCamera(other))
        {
            Vector3 diff = new Vector3(xShift, 0, zShift);
            Vector3 newDesiredPosition = Camera.main.transform.position - diff;
            StartCoroutine(LerpFromTo(Camera.main.transform.position, newDesiredPosition, 0.5f));

            if(other.tag == "Player1")
            {
                player2.transform.position = player1.transform.position;
            }
            else
            {
                player1.transform.position = player2.transform.position;
            }
            //Debug.Log("Camera shift");


        }
    }

    private void SpawnEnemies(Vector3 roomPosition)
    {
        Vector3 enemyPosition = new Vector3(roomPosition.x, 0, roomPosition.z);
        GameObject ground = Instantiate(Resources.Load("Prefabs/Enemies/cat", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
    {
        if (!buzyShifting)
        {
            CloseDoors(pos2);
            SpawnEnemies(pos2);

            buzyShifting = true;
            //Debug.Log("Camera is shifting");
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                Camera.main.transform.position = Vector3.Lerp(pos1, pos2, t / duration);
                yield return 0;
            }
            Camera.main.transform.position = pos2;
            buzyShifting = false;
            //Debug.Log("Camera is shifting from " + pos1 + " to " + pos2);
        }
    }

    private void CloseDoors(Vector3 roomPosition)
    {
        //Vector3 doorPosition = new Vector3(roomPosition.x, 0, roomPosition.z);
        //GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), doorPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject levelController = GameObject.FindGameObjectWithTag("LevelController");
        levelController.GetComponent<LevelController>().CloseDoors();

    }

    private bool ShouldShiftCamera(Collider other)
    {
        if (xShift == 0)
        {
            if (zShift > 0)
            {
                if (other.transform.position.z < Camera.main.transform.position.z)
                {
                    return true;
                }
            }
            else
            {
                if (other.transform.position.z > Camera.main.transform.position.z)
                {
                    return true;
                }
            }
        }
        if (zShift == 0)
        {
            if (xShift > 0)
            {
                if (other.transform.position.x < Camera.main.transform.position.x)
                {
                    return true;
                }
            }
            else
            {
                if (other.transform.position.x > Camera.main.transform.position.x)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


//Old code to change scene
//public string sceneName;
//public float xPosition;
//public float zPosition;
//if (other.tag == "Player1" || other.tag == "Player2")
//{
//    Debug.Log(other.tag);
//    other.transform.position = new Vector3(xPosition, 0, zPosition);
//    Debug.Log(sceneName);
//    SceneManager.LoadScene(sceneName);
//}