using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    private List<GameObject> fenceList = new List<GameObject>();
    private LevelRepresentation levelRepresentation = new LevelRepresentation();
    public int scaleX = 17;
    public int scaleZ = 12;

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
        int iPosition = levelRepresentation.RoomArray.GetLength(0) / 2;
        int jPosition = levelRepresentation.RoomArray.GetLength(1) / 2;
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
        for (int i = 0; i < levelRepresentation.RoomArray.GetLength(0); i++)
        {
            for (int j = 0; j < levelRepresentation.RoomArray.GetLength(1); j++)
            {
                GenerateRoom(levelRepresentation.RoomArray[levelRepresentation.RoomArray.GetLength(1) - i - 1, j], i, j);
            }
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
        GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Ground", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        //GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        //GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        //fenceList.Add(fences);

        if (room.NorthDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/Walls/NorthWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
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
			GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/Walls/NorthWallClosed", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
		}
        if (room.SouthDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/Walls/SouthWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
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
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/Walls/SouthWallClosed", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (room.WestDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/Walls/WestWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
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
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/Walls/WestWallClosed", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        if (room.EastDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/Walls/EastWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
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
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/Walls/EastWallClosed", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
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
