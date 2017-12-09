﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    private List<GameObject> fenceList = new List<GameObject>();
    private LevelRepresentation levelRepresentation = new LevelRepresentation();
    public int scaleX = 17;
    public int scaleZ = 12;
    private Color wallColor;
    private int obstacleIndex;
    private int obstacleDensity = 10;
    private System.Random rand = new System.Random();

    List<GameObject> obstacleList = new List<GameObject>();

    void Awake()
    {


        GenerateLevel();

        OpenDoors();

        PositionPlayersAndCamera();



        //Debug.Log("This was run");
    }

    public void CloseDoors()
    {
        foreach (var fence in fenceList)
        {
            fence.SetActive(true);
        }
    }

    public LevelRepresentation GetLevelRepresentation()
    {
        return levelRepresentation;
    }

    public void OpenDoors()
    {
        foreach (var fence in fenceList)
        {
            //Debug.Log("Fence: " + fence);
            fence.SetActive(false);
        }
    }

    private void PositionPlayersAndCamera()
    {
        GameObject player1 = GameObject.Find("Player1");
        GameObject player2 = GameObject.Find("Player2");

        //Always start in center room
        int iPosition = levelRepresentation.RoomArray.GetLength(1) / 2;
        int jPosition = levelRepresentation.RoomArray.GetLength(0) / 2;
        int xPosition = iPosition * scaleX;
        int zPosition = jPosition * scaleZ;

        Vector3 player1Position = new Vector3(xPosition, 0, zPosition);

        player1.transform.position = player1Position;
        player2.transform.position = player1Position;

        Vector3 cameraPosition = new Vector3(xPosition, Camera.main.transform.position.y, zPosition - 4.2f);
        Camera.main.transform.position = cameraPosition;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void GenerateLevel()
    {
        int terrainId = Random.Range(0, 31);
        //Debug.Log("TerrainId: " + terrainId);
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Terrain", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        ground.GetComponent<TerrainHandler>().ShiftTerrain(terrainId);

        wallColor = Random.ColorHSV(0.0f, 1.0f, 0.8f, 0.8f, 1.0f, 1.0f);

        int obstacleType = rand.Next(8);

        obstacleList.Clear();
        if (obstacleType == 0)
        {
            obstacleDensity = 10;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Cone Tree - Green") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Cone Tree - Grey") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Cone Tree - Lime") as GameObject);
        }
        if (obstacleType == 1)
        {
            obstacleDensity = 10;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Decrepit Tree 01 Brown") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Vine01") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Log") as GameObject);
        }
        if (obstacleType == 2)
        {
            obstacleDensity = 5;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Wall Rock 01") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Wall Rock 02") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Wall Rock 03") as GameObject);
        }
        if (obstacleType == 3)
        {
            obstacleDensity = 3;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Lamp") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Pillar 01 Brown") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Pillar 01 Grey") as GameObject);
        }
        if (obstacleType == 4)
        {
            obstacleDensity = 5;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Tombstone 01A") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Tombstone 01B") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Tombstone 02A") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Tombstone 02B") as GameObject);
        }
        if (obstacleType == 5)
        {
            obstacleDensity = 1;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Tombstone 03") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Graveyard Statue") as GameObject);
        }
        if (obstacleType == 6)
        {
            obstacleDensity = 3;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Pot 01 Blue") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Pot 01 Green") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Pot 01 Orange") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Pot 01 Purple") as GameObject);
        }
        if (obstacleType == 7)
        {
            obstacleDensity = 1;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Big Rock 01") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Big Rock 02") as GameObject);
        }


        obstacleIndex = Random.Range(0, obstacleList.Count);

        for (int i = 0; i < levelRepresentation.RoomArray.GetLength(0); i++)
        {
            for (int j = 0; j < levelRepresentation.RoomArray.GetLength(1); j++)
            {
                obstacleIndex = Random.Range(0, obstacleList.Count);

                //Debug.Log("i: " + i + " , j: " + j + " , value: " + (levelRepresentation.RoomArray.GetLength(0) - i - 1));
                GenerateRoom(levelRepresentation.RoomArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j], i, j);

                if(levelRepresentation.ContentArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j] == LevelRepresentation.ContentType.BossLevel1)
                {
                    //Debug.Log("Bossroom");
                    AddBossEntrance(levelRepresentation.RoomArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j], i, j);
                }
            }
        }

        Destroy(ground);
        //Destroy(wall);
    }

    private void AddBossEntrance(Room room, int i, int j)
    {
        int x = j * scaleX;
        int z = i * scaleZ;

        if(room.NorthDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/BossEntrance", typeof(GameObject)), new Vector3(x, 0, z + 6.9f), Quaternion.Euler(-90, 180, 0)) as GameObject;
        }
        if (room.SouthDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/BossEntrance", typeof(GameObject)), new Vector3(x, 0, z - 6.9f), Quaternion.Euler(-90, 180, 0)) as GameObject;
        }
        if (room.WestDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/BossEntrance", typeof(GameObject)), new Vector3(x - 9.3f, 0, z), Quaternion.Euler(-90, 90, 0)) as GameObject;
        }
        if (room.EastDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/BossEntrance", typeof(GameObject)), new Vector3(x + 9.3f, 0, z), Quaternion.Euler(-90, 270, 0)) as GameObject;
        }


    }

    private void GenerateRoom(Room room, int i, int j)
    {
        int x = j * scaleX;
        int z = i * scaleZ;

        if (room.NoRoom == true)
        {
            return;
        }

        //Add common objects
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Terrain", typeof(GameObject)), new Vector3(x - scaleX/2 - 0.2f, 0, z - scaleZ/2), Quaternion.Euler(0, 0, 0)) as GameObject;
        //ground.GetComponent<TerrainHandler>().ShiftTerrain(terrainId);
        //ground.GetComponent<TerrainHandler>().ShiftTerrain(terrainId);

        //GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        //GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        //fenceList.Add(fences);

        //Debug.Log("Obstacle: " + obstacleList[obstacleIndex].ToString());

        //Instantiate(obstacleList[obstacleIndex], new Vector3(x, 0, z), Quaternion.Euler(-90, 0, 0));

        
        if(rand.Next(100) > 50)
        {
            for (int l = 0; l < obstacleDensity; l++)
            {
                if (rand.Next(100) > 50)
                {
                    Instantiate(obstacleList[obstacleIndex], new Vector3(x + rand.Next(-(scaleX - 4) / 2, (scaleX - 3) / 2), 0, z + rand.Next(-(scaleZ - 3) / 2, (scaleZ - 2) / 2)), Quaternion.Euler(-90, 0, 0));
                }
            }
        }

        if (room.NorthDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/NorthWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall);
            GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors/NorthDoorBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences/NorthFenceBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            fenceList.Add(fences);
        }
        else if(room.NorthWallOpen)
        {
            //Open
        }
		else
		{
            GameObject wall1 = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/NorthWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject wall2 = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/NorthWallClosed", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall1);
            SetColorOfChildren(wall2);
        }
        if (room.SouthDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/SouthWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall);
            GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors/SouthDoorBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences/SouthFenceBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            fenceList.Add(fences);
        }
		else if(room.SouthWallOpen)
        {
            //Open
        }
        else
        {
            GameObject wall1 = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/SouthWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject wall2 = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/SouthWallClosed", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall1);
            SetColorOfChildren(wall2);
        }
        if (room.WestDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/WestWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall);
            GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors/WestDoorBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences/WestFenceBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            fenceList.Add(fences);
        }
		else if(room.WestWallOpen)
        {
            //Open
        }
        else
        {
            GameObject wall1 = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/WestWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject wall2 = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/WestWallClosed", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall1);
            SetColorOfChildren(wall2);
        }
        if (room.EastDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/EastWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall);
            GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors/EastDoorBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences/EastFenceBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            fenceList.Add(fences);
        }
		else if(room.EastWallOpen)
        {
            //Open
        }
        else
        {
            GameObject wall1 = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/EastWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject wall2 = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/EastWallClosed", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall1);
            SetColorOfChildren(wall2);
        }
    }

    private void SetColorOfChildren(GameObject wall)
    {
        for (int i = 0; i < wall.transform.childCount; i++)
        {
            wall.transform.GetChild(i).GetComponent<MeshRenderer>().material.SetColor("_Color", wallColor);
        }
    }


    private void GenerateLevelOld()
    {
        GenerateFirstRoom();
        GenerateNorthRoom();
        GenerateNorthEastRoom();
        GenerateSouthRoom();
        GenerateWestRoom();
        GenerateEastRoom();
    }

    private void GenerateFirstRoom()
    {
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Ground", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(0, 0.5f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

        GameObject northWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/NorthWallOpen", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject southWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/SouthWallOpen", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject westWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/WestWallOpen", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject eastWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/EastWallOpen", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

        //GameObject walls = Instantiate(Resources.Load("Prefabs/Environment/Walls", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        fenceList.Add(fences);
    }

    private void GenerateNorthRoom()
    {
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Ground", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(0, 0.5f, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate2 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(1, 0.5f, 10), Quaternion.Euler(0, 0, 0)) as GameObject;

        GameObject northWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/NorthWallClosed", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject southWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/SouthWallOpen", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject westWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/WestWallClosed", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject eastWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/EastWallClosed", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;

        //GameObject walls = Instantiate(Resources.Load("Prefabs/Environment/Walls", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        fenceList.Add(fences);
    }

    private void GenerateNorthEastRoom()
    {
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Ground", typeof(GameObject)), new Vector3(18, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(18, 0.5f, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate2 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(19, 0.5f, 10), Quaternion.Euler(0, 0, 0)) as GameObject;

        GameObject northWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/NorthWallClosed", typeof(GameObject)), new Vector3(18, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject southWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/SouthWallOpen", typeof(GameObject)), new Vector3(18, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject westWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/WestWallClosed", typeof(GameObject)), new Vector3(18, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject eastWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/EastWallClosed", typeof(GameObject)), new Vector3(18, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;

        //GameObject walls = Instantiate(Resources.Load("Prefabs/Environment/Walls", typeof(GameObject)), new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(18, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(18, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
        fenceList.Add(fences);

        //GameObject cat = Instantiate(Resources.Load("Prefabs/Enemies/Cat/cat_idle", typeof(GameObject)), new Vector3(18, 0, 10), Quaternion.Euler(0, 0, 0)) as GameObject;
    }

    private void GenerateSouthRoom()
    {
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Ground", typeof(GameObject)), new Vector3(0, 0, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(0, 0.5f, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate2 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(1, 0.5f, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate3 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(2, 0.5f, -10), Quaternion.Euler(0, 0, 0)) as GameObject;

        GameObject northWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/NorthWallOpen", typeof(GameObject)), new Vector3(0, 0, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject southWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/SouthWallClosed", typeof(GameObject)), new Vector3(0, 0, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject westWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/WestWallClosed", typeof(GameObject)), new Vector3(0, 0, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject eastWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/EastWallClosed", typeof(GameObject)), new Vector3(0, 0, -10), Quaternion.Euler(0, 0, 0)) as GameObject;

        //GameObject walls = Instantiate(Resources.Load("Prefabs/Environment/Walls", typeof(GameObject)), new Vector3(0, 0, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(0, 0, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(0, 0, -10), Quaternion.Euler(0, 0, 0)) as GameObject;
        fenceList.Add(fences);
    }

    private void GenerateWestRoom()
    {
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Ground", typeof(GameObject)), new Vector3(-18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(-18, 0.5f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate2 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(-18, 0.5f, 1), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate3 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(-18, 0.5f, 2), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate4 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(-18, 0.5f, 3), Quaternion.Euler(0, 0, 0)) as GameObject;

        GameObject northWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/NorthWallClosed", typeof(GameObject)), new Vector3(-18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject southWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/SouthWallClosed", typeof(GameObject)), new Vector3(-18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject westWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/WestWallClosed", typeof(GameObject)), new Vector3(-18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject eastWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/EastWallOpen", typeof(GameObject)), new Vector3(-18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

        //GameObject walls = Instantiate(Resources.Load("Prefabs/Environment/Walls", typeof(GameObject)), new Vector3(-18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(-18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(-18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        fenceList.Add(fences);
    }

    private void GenerateEastRoom()
    {
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Ground", typeof(GameObject)), new Vector3(18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(18, 0.5f, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate2 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(18, 0.5f, 1), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate3 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(18, 0.5f, 2), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate4 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(18, 0.5f, 3), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject crate5 = Instantiate(Resources.Load("Prefabs/Crate01", typeof(GameObject)), new Vector3(18, 0.5f, 4), Quaternion.Euler(0, 0, 0)) as GameObject;

        GameObject northWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/NorthWallOpen", typeof(GameObject)), new Vector3(18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject southWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/SouthWallClosed", typeof(GameObject)), new Vector3(18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject westWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/WestWallOpen", typeof(GameObject)), new Vector3(18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject eastWall = Instantiate(Resources.Load("Prefabs/Environment/Walls/EastWallClosed", typeof(GameObject)), new Vector3(18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;

        //GameObject walls = Instantiate(Resources.Load("Prefabs/Environment/Walls", typeof(GameObject)), new Vector3(18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(18, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        fenceList.Add(fences);
    }
}
