using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour {

    public float FastSpeed;
    public float SlowSpeed;
    private LevelController levelController;
    private GameObject player1;

    // Use this for initialization
    void Start () {
        levelController = FindObjectOfType(typeof(LevelController)) as LevelController;
        player1 = GameObject.FindGameObjectWithTag("Player1");

        Collectibles.current = new Collectibles();
    }
	
	// Update is called once per frame
	void Update () {
        if (player1 == null)
        {
            return;
        }
        if (levelController == null)
        {
            return;
        }

        //Open doors
        //if (Input.GetKey(KeyCode.N))
        //{
        //    levelController.OpenDoors();
        //}

        //Shift level
        //if (Input.GetKey(KeyCode.X))
        //{
        //    player1.GetComponent<HeroController>().enabled = false;
        //    SceneManager.LoadScene("LoadLevelScene");
        //}

        int xRoomPos = (int)(player1.transform.position.x / levelController.scaleX);
        int zRoomPos = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)((player1.transform.position.z) / levelController.scaleZ) - 1;

        int xPlayerRoom = (int)((player1.transform.position.x / levelController.scaleX) + 0.5f) + 0;
        int zPlayerRoom = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)(((player1.transform.position.z) / levelController.scaleZ) + 1.5f);

        float xPlayerRelativePos = player1.transform.position.x / levelController.scaleX - xPlayerRoom + 0.5f;
        float zPlayerRelativePos = zPlayerRoom + 0.5f - (levelController.GetLevelRepresentation().RoomArray.GetLength(0) - player1.transform.position.z / levelController.scaleZ - 1);


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
        //float yPos = Camera.main.transform.position.y;
        //float zPos = player1.transform.position.z - 4.2f;
        float yPos = 20;
        float zPos = player1.transform.position.z - 18.19f;

        float xPosCentered = xPlayerRoom * levelController.scaleX;
        //float zPosCentered = (levelController.GetLevelRepresentation().RoomArray.GetLength(0) - 1.0f) * levelController.scaleZ - zPlayerRoom * levelController.scaleZ - 4.2f;
        float zPosCentered = (levelController.GetLevelRepresentation().RoomArray.GetLength(0) - 1.0f) * levelController.scaleZ - zPlayerRoom * levelController.scaleZ - 18.19f;

        if (blockNorth & zPlayerRelativePos > 0.5)
        {
            zPos = zPosCentered;
        }
        if (blockSouth & zPlayerRelativePos < 0.5)
        {
            zPos = zPosCentered;
        }
        if (blockWest & xPlayerRelativePos < 0.5)
        {
            xPos = xPosCentered;
        }
        if (blockEast & xPlayerRelativePos > 0.5)
        {
            xPos = xPosCentered;
        }

        newCameraPos = new Vector3(xPos, yPos, zPos);

        //if (Math.Abs(newCameraPos.x - Camera.main.transform.position.x) < 2 & Math.Abs(newCameraPos.z - Camera.main.transform.position.z) < 2)
        //{
        //    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCameraPos, 0.3f);
        //}
        //else
        //{
        //    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCameraPos, 0.3f);
        //}
        
        //Always using same speed seems ok
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCameraPos, 0.3f);
    }
}
