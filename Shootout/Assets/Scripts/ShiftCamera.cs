using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftCamera : MonoBehaviour {

    public int xShift;
    public int zShift;
    public float shiftSpeed;
    private bool buzyShifting = false;
    private LevelController levelController;
    GameObject player1;

    // Update is called once per frame
    private void Update()
    {
        int xRoomPos = (int)(player1.transform.position.x / levelController.scaleX);
        int zRoomPos = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)((player1.transform.position.z) / levelController.scaleZ) - 1;

        int xPlayerRoom = (int)((player1.transform.position.x / levelController.scaleX) + 0.5f) + 0;
        int zPlayerRoom = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)(((player1.transform.position.z) / levelController.scaleZ) + 1.5f);


        float xPlayerRelativePos = player1.transform.position.x / levelController.scaleX - xPlayerRoom + 0.5f;
        float zPlayerRelativePos = zPlayerRoom + 2.5f - (levelController.GetLevelRepresentation().RoomArray.GetLength(1) - player1.transform.position.z / levelController.scaleZ - 1);
        //Debug.Log("xPlayerRelativePos: " + xPlayerRelativePos + " , zPlayerRelativePos: " + zPlayerRelativePos);

        bool blockNorth = false, blockSouth = false, blockWest = false, blockEast = false;

        //Debug.Log("xPlayerRoom: " + xPlayerRoom + " , zPlayerRoom: " + zPlayerRoom);

        if (!levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].NorthWallOpen)
        {
            blockNorth = true;
            if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].WestWallOpen)
            {
                if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom - 1].NorthWallOpen)
                {
                    blockNorth = false;
                }
            }
            if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].EastWallOpen)
            {
                if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom + 1].NorthWallOpen)
                {
                    blockNorth = false;
                }
            }
        }
        if (!levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].SouthWallOpen)
        {
            blockSouth = true;
            if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].WestWallOpen)
            {
                if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom - 1].SouthWallOpen)
                {
                    blockSouth = false;
                }
            }
            if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].EastWallOpen)
            {
                if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom + 1].SouthWallOpen)
                {
                    blockSouth = false;
                }
            }
        }
        if (!levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].WestWallOpen)
        {
            blockWest = true;
            if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].NorthWallOpen)
            {
                if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom - 1, xPlayerRoom].WestWallOpen)
                {
                    blockWest = false;
                }
            }
            if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].SouthWallOpen)
            {
                if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom + 1, xPlayerRoom].WestWallOpen)
                {
                    blockWest = false;
                }
            }
        }
        if (!levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].EastWallOpen)
        {
            blockEast = true;
            if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].NorthWallOpen)
            {
                if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom - 1, xPlayerRoom].EastWallOpen)
                {
                    blockEast = false;
                }
            }
            if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].SouthWallOpen)
            {
                if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom + 1, xPlayerRoom].EastWallOpen)
                {
                    blockEast = false;
                }
            }
        }

        //Debug.Log("" + blockNorth + blockSouth + blockWest + blockEast);

        Vector3 newCameraPos;

        float xPos = player1.transform.position.x;
        float yPos = Camera.main.transform.position.y;
        float zPos = player1.transform.position.z - 4.2f;
		
		float xPosCentered = xPlayerRoom * levelController.scaleX;
		float zPosCentered = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (zPlayerRoom - 0.5f) * levelController.scaleZ - 4.2f + 15;

        if (blockNorth & zPlayerRelativePos > 0.5)
        {
            //zPos = Camera.main.transform.position.z;
			zPos = zPosCentered;
        }
        if (blockSouth & zPlayerRelativePos < 0.5)
        {
            //zPos = Camera.main.transform.position.z;
			zPos = zPosCentered;
        }
        if (blockWest & xPlayerRelativePos < 0.5)
        {
            //xPos = Camera.main.transform.position.x;
			xPos = xPosCentered;
        }
        if (blockEast & xPlayerRelativePos > 0.5)
        {
            //xPos = Camera.main.transform.position.x;
			xPos = xPosCentered;
        }

        newCameraPos = new Vector3(xPos, yPos, zPos);

        if (Math.Abs(newCameraPos.x - Camera.main.transform.position.x) < 2 & Math.Abs(newCameraPos.z - Camera.main.transform.position.z) < 2)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCameraPos, 1.0f);
            //Camera.main.transform.position = newCameraPos;
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCameraPos, 0.02f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnterTrigger");
        GameObject player1 = GameObject.Find("Player1");
        GameObject player2 = GameObject.Find("Player2");

        //var pos = player1.transform.position;
        //pos.z += 1;
        //player1.transform.position = pos;

        //Debug.Log("EnterTrigger, shouldMove = " + shouldShiftCamera(other) + " zShift = " + zShift);

        if ((other.tag == "Player1" || other.tag == "Player2") && ShouldShiftCamera(other) & false)
        {
            Vector3 diff = new Vector3(xShift, 0, zShift);
            Vector3 newDesiredPositionRaw = Camera.main.transform.position - diff;

            int xRoom = (int)((newDesiredPositionRaw.x / levelController.scaleX) + 0.5f) + 0;
            int zRoom = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)(((newDesiredPositionRaw.z) / levelController.scaleZ) + 1.5f);

            float xRoomCentered = xRoom * levelController.scaleX;
            float zRoomCentered = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (zRoom - 0.5f) * levelController.scaleZ - 4.2f + 15;

            Vector3 newDesiredPosition = new Vector3(xRoomCentered, newDesiredPositionRaw.y, zRoomCentered);


            StartCoroutine(LerpFromTo(Camera.main.transform.position, newDesiredPosition, shiftSpeed));

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
        //Get room position
        levelController = FindObjectOfType(typeof(LevelController)) as LevelController;
        int xRoomPos = (int)(roomPosition.x / levelController.scaleX);
        int zRoomPos = levelController.GetLevelRepresentation().RoomArray.GetLength(1) - (int)((roomPosition.z + 4.2f) / levelController.scaleZ) - 1;

        //Debug.Log("x: " + roomPosition.x + " z: " + roomPosition.z);
        //Debug.Log("xint: " + xRoomPos + " zint: " + zRoomPos);
        //Debug.Log("ContentType: " + levelController.GetLevelRepresentation().ContentArray[xRoomPos, zRoomPos]);

        //Add enemies and close doors
        //if (levelController.GetLevelRepresentation().ContentArray[zRoomPos, xRoomPos] == LevelRepresentation.ContentType.EnemyLevel1)
        //{
        //    CloseDoors(roomPosition);
        //    //TODO: Add random enemies of appropriate level
        //    Vector3 enemyPosition = new Vector3(roomPosition.x, 0, roomPosition.z + 4.2f);
        //    GameObject cat = Instantiate(Resources.Load("Prefabs/Enemies/cat", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        //    levelController.GetLevelRepresentation().ContentArray[zRoomPos, xRoomPos] = LevelRepresentation.ContentType.NoContent;
        //}
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
    {
        if (!buzyShifting)
        {
            //CloseDoors(pos2);
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
        levelController = FindObjectOfType(typeof(LevelController)) as LevelController;
        player1 = GameObject.Find("Player1");
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