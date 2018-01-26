using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    private List<GameObject> fenceList = new List<GameObject>();
    private LevelRepresentation levelRepresentation;// = new LevelRepresentation();
    public int scaleX = 17;
    public int scaleZ = 12;
    public List<BoundsInt> RoomData;
    private Color wallColor;
    private int obstacleIndex;
    private int obstacleDensity = 10;
    private System.Random rand = new System.Random();
    private int obstacleType;
    private GameObject Walls;
    private GameObject Doors;
    private GameObject Grounds;
    private GameObject Fences;
    private GameObject Obstacles;
    private string groundPrefab;
    private int groundId;
    public GameObject WayPoint;
    public GameObject HiddenWayPoint;
    private int groupId = 0;

    List<GameObject> obstacleList = new List<GameObject>();

    void Awake()
    {
        levelRepresentation = GetComponent<LevelRepresentation>();
        levelRepresentation.CreateRoomArray();

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
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        //GameObject player2 = GameObject.Find("Player2");

        //Always start in center room
        int iPosition = levelRepresentation.RoomArray.GetLength(1) / 2;
        int jPosition = levelRepresentation.RoomArray.GetLength(0) / 2;
        int xPosition = iPosition * scaleX;
        int zPosition = jPosition * scaleZ;

        Vector3 player1Position = new Vector3(xPosition, 0, zPosition);

        player1.transform.position = player1Position;
        //player2.transform.position = player1Position;

        Vector3 cameraPosition = new Vector3(xPosition, Camera.main.transform.position.y, zPosition - 4.2f);
        Camera.main.transform.position = cameraPosition;
    }

    // Use this for initialization
    void Start() {

        StartCoroutine(DelayedAllowMovement());
    }

    IEnumerator DelayedAllowMovement()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        player1.GetComponent<HeroController>().enabled = true;

        //Debug.Log("Loading");
        //SaveLoad.Save();
        SaveLoad.Load();
        
    }

    // Update is called once per frame
    void Update() {

    }

    private void GenerateLevel()
    {
        //int terrainId = Random.Range(0, 31);
        ////Debug.Log("TerrainId: " + terrainId);
        //GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Terrain", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        //ground.GetComponent<TerrainHandler>().ShiftTerrain(terrainId);

        groundId = Random.Range(1, 31);

        GameObject progressController = GameObject.FindGameObjectWithTag("ProgressController");
        groundId = progressController.GetComponent<ProgressController>().NextLevel - 1;

        groundPrefab = "Prefabs/Environment/Grounds/Ground" + groundId;

        wallColor = Random.ColorHSV(0.0f, 1.0f, 0.8f, 0.8f, 1.0f, 1.0f);

        obstacleType = rand.Next(8);

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
            obstacleDensity = 2;
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Big Rock 01") as GameObject);
            obstacleList.Add(Resources.Load("Prefabs/Environment/Obstacles/Big Rock 02") as GameObject);
        }


        obstacleIndex = Random.Range(0, obstacleList.Count);

        Walls = new GameObject();
        Walls.name = "Walls";
        Doors = new GameObject();
        Doors.name = "Doors";
        Fences = new GameObject();
        Fences.name = "Fences";
        Obstacles = new GameObject();
        Obstacles.name = "Obstacles";
        Grounds = new GameObject();
        Grounds.name = "Grounds";

        for (int test = 0; test < 1; test++)
        {

            for (int i = 0; i < levelRepresentation.RoomArray.GetLength(0); i++)
            {
                for (int j = 0; j < levelRepresentation.RoomArray.GetLength(1); j++)
                {
                    obstacleIndex = Random.Range(0, obstacleList.Count);

                    //Debug.Log("i: " + i + " , j: " + j + " , value: " + (levelRepresentation.RoomArray.GetLength(0) - i - 1));
                    GenerateRoom(levelRepresentation.RoomArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j], i, j);

                    if (levelRepresentation.ContentArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j] == LevelRepresentation.ContentType.BossLevel1)
                    {
                        //Debug.Log("Bossroom");
                        AddBossEntrance(levelRepresentation.RoomArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j], i, j);
                        AddWaypoint(i, j);
                    }
                    if (levelRepresentation.ContentArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j] == LevelRepresentation.ContentType.ItemLevel1 ||
                        levelRepresentation.ContentArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j] == LevelRepresentation.ContentType.HiddenRoom)
                    {
                        //Debug.Log("Itemroom");
                        AddItems(levelRepresentation.RoomArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j], i, j);
                    }
                    if (levelRepresentation.ContentArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j] == LevelRepresentation.ContentType.HiddenRoom)
                    {
                        //Debug.Log("Hidden room");
                        if (ShouldAddHiddenDoor())
                        {
                            Debug.Log("Add hidden door");
                            AddHiddenDoor(levelRepresentation.RoomArray[levelRepresentation.RoomArray.GetLength(0) - i - 1, j], i, j);
                        }
                        else
                        {
                            Debug.Log("Add hidden door");
                            AddHiddenWaypoint(i, j);
                        }
                    }
                }
            }

        }
        RoomData.AddRange(levelRepresentation.RoomArray.Cast<Room>().Select(r => r.GetRoomArea()));


        //var comby =  GetComponent<Combine>();
        //foreach (Transform go in Walls.transform)
        //{
        //    comby.CombineStuff(go.gameObject);
        //}
        //Destroy(ground);

        //StaticBatchingUtility.Combine(Walls);
        //StaticBatchingUtility.Combine(Obstacles);
        //StaticBatchingUtility.Combine(Grounds);

        //MergeMeshes(Walls);
        //MergeMeshes(Obstacles);
        //MergeMeshes(Grounds);

        //StaticBatchingUtility.Combine(walls);
    }

    private bool ShouldAddHiddenDoor()
    {
        Debug.Log("ForestCleared: " + Collectibles.current.collectibleItemsList.Where(item => item.name == "ForestCleared").FirstOrDefault().collected);
        Debug.Log("groundId: " + groundId);
        if (Collectibles.current.collectibleItemsList.Where(item => item.name == "ForestCleared").FirstOrDefault().collected && groundId == 3)
        {
            //If Forest finished open path in Forest level 3 to Desert
            return false;
        }
        if (Collectibles.current.collectibleItemsList.Where(item => item.name == "DesertCleared").FirstOrDefault().collected && groundId == 4)
        {
            //If Desert finished open path in Forest level 4 to Castle Grounds
            return false;
        }
        if (Collectibles.current.collectibleItemsList.Where(item => item.name == "CastleCleared").FirstOrDefault().collected && groundId == 9)
        {
            //If Castle finished open path in Desert level 3 to Mountains
            return false;
        }
        else
        {
            return true;
        }
    }

    private void MergeMeshes(GameObject mergeObject)
    {
        Vector3 position = mergeObject.transform.position;
        mergeObject.transform.position = Vector3.zero;

        MeshRenderer meshRenderer = mergeObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = mergeObject.AddComponent<MeshFilter>();
        MeshFilter[] meshFilters = mergeObject.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //meshFilters[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            //meshFilters[i].gameObject.GetComponent<MeshFilter>();

            meshFilters[i].gameObject.SetActive(false);
            Debug.Log(meshFilters[i].gameObject.name);
            i++;
        }
        Debug.Log("Length: " + meshFilters.Length);
        mergeObject.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        mergeObject.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        mergeObject.transform.gameObject.SetActive(true);
        mergeObject.GetComponent<MeshRenderer>().enabled = true;

        Mesh mesh = mergeObject.GetComponent<MeshFilter>().mesh;
        mesh.RecalculateBounds();

        //Reset position
        mergeObject.transform.position = position;

        while (i < meshFilters.Length)
        {
            Destroy(meshFilters[i].gameObject.GetComponent<MeshRenderer>());
            Destroy(meshFilters[i].gameObject.GetComponent<MeshFilter>());
            Destroy(meshFilters[i].gameObject.GetComponentInChildren<MeshRenderer>());
            Destroy(meshFilters[i].gameObject.GetComponentInChildren<MeshFilter>());
            Destroy(meshFilters[i].gameObject.transform);
            i++;
        }

        var children = new List<GameObject>();
        foreach (Transform child in mergeObject.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

    private void AddItems(Room room, int i, int j)
    {
        int x = j * scaleX;
        int z = i * scaleZ;

        List<int> usedIndexes = new List<int>();
        List<int> availableIndexes = Enumerable.Range(1, 110).ToList();

        for (int k = 0; k < 4; k++)
        {
            int l = availableIndexes[Random.Range(0, availableIndexes.Count)];
            availableIndexes.Remove(l);
            usedIndexes.Add(l);
        }

        //Debug.Log("usedIndexes: " + usedIndexes[0] + ":" + usedIndexes[1] + ":" + usedIndexes[2] + ":" + usedIndexes[3]);

        Quaternion euler0 = Quaternion.Euler(-90, 0, 0);
        Quaternion euler1 = Quaternion.Euler(-90, 0, 0);
        Quaternion euler2 = Quaternion.Euler(-90, 0, 0);
        Quaternion euler3 = Quaternion.Euler(-90, 0, 0);

        if(usedIndexes[0] >= 34 && usedIndexes[0] <= 59)
        {
            euler0 = Quaternion.Euler(0, 180, 0);
        }
        if (usedIndexes[1] >= 34 && usedIndexes[1] <= 59)
        {
            euler1 = Quaternion.Euler(0, 180, 0);
        }
        if (usedIndexes[2] >= 34 && usedIndexes[2] <= 59)
        {
            euler2 = Quaternion.Euler(0, 180, 0);
        }
        if (usedIndexes[3] >= 34 && usedIndexes[3] <= 59)
        {
            euler3 = Quaternion.Euler(0, 180, 0);
        }


        GameObject item1 = Instantiate(Resources.Load("Prefabs/PickupsLevel1/" + usedIndexes[0], typeof(GameObject)), new Vector3(x+5, 0.5f, z+3), euler0) as GameObject;
        GameObject item2 = Instantiate(Resources.Load("Prefabs/PickupsLevel1/" + usedIndexes[1], typeof(GameObject)), new Vector3(x-5, 0.5f, z+3), euler1) as GameObject;
        GameObject item3 = Instantiate(Resources.Load("Prefabs/PickupsLevel1/" + usedIndexes[2], typeof(GameObject)), new Vector3(x+5, 0.5f, z-3), euler2) as GameObject;
        GameObject item4 = Instantiate(Resources.Load("Prefabs/PickupsLevel1/" + usedIndexes[3], typeof(GameObject)), new Vector3(x-5, 0.5f, z-3), euler3) as GameObject;

        item1.GetComponent<ItemName>().Group = "Group" + groupId;
        item2.GetComponent<ItemName>().Group = "Group" + groupId;
        item3.GetComponent<ItemName>().Group = "Group" + groupId;
        item4.GetComponent<ItemName>().Group = "Group" + groupId;

        groupId++;


        //item1.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));
        //item2.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));
        //item3.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));
        //item4.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));
    }

    private void AddHiddenDoor(Room room, int i, int j)
    {
        int x = j * scaleX;
        int z = i * scaleZ;

        if (room.NorthDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/Obstacles/Wall Rock 03a", typeof(GameObject)), new Vector3(x, 0, z + 6.0f), Quaternion.Euler(-90, 0, 0)) as GameObject;
        }
        if (room.SouthDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/Obstacles/Wall Rock 03a", typeof(GameObject)), new Vector3(x, 0, z - 6.0f), Quaternion.Euler(-90, 0, 0)) as GameObject;
        }
        if (room.WestDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/Obstacles/Wall Rock 03b", typeof(GameObject)), new Vector3(x - 8.5f, 0, z), Quaternion.Euler(-90, 270, 0)) as GameObject;
        }
        if (room.EastDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/Obstacles/Wall Rock 03b", typeof(GameObject)), new Vector3(x + 8.5f, 0, z), Quaternion.Euler(-90, 90, 0)) as GameObject;
        }
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
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/BossEntrance", typeof(GameObject)), new Vector3(x - 9.3f, 0, z), Quaternion.Euler(-90, 270, 0)) as GameObject;
        }
        if (room.EastDoorOpen)
        {
            GameObject bossEntrance = Instantiate(Resources.Load("Prefabs/Environment/BossEntrance", typeof(GameObject)), new Vector3(x + 9.3f, 0, z), Quaternion.Euler(-90, 90, 0)) as GameObject;
        }
    }

    private void AddWaypoint(int i, int j)
    {
        int x = j * scaleX;
        int z = i * scaleZ;

        WayPoint = Instantiate(Resources.Load("Prefabs/Environment/Waypoint", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(-90, 0, 0)) as GameObject;
        WayPoint.SetActive(false);
    }

    private void AddHiddenWaypoint(int i, int j)
    {
        int x = j * scaleX;
        int z = i * scaleZ;

        HiddenWayPoint = Instantiate(Resources.Load("Prefabs/Environment/HiddenWaypoint", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(-90, 0, 0)) as GameObject;
        //HiddenWayPoint.SetActive(false);
    }

    private void GenerateRoom(Room room, int i, int j)
    {
        int x = j * scaleX;
        int z = i * scaleZ;

        if (room.NoRoom == true)
        {
            return;
        }

        //Create the bounds of the room
        room.SetRoomArea(new Vector3Int(x - scaleX / 2, 0, z - scaleZ / 2), new Vector3Int(x + scaleX / 2, 0, z + scaleZ / 2));

        //Add common objects
        //GameObject ground = Instantiate(Resources.Load("Prefabs/Environment/Terrain", typeof(GameObject)), new Vector3(x - scaleX/2 - 0.2f, 0, z - scaleZ/2), Quaternion.Euler(0, 0, 0)) as GameObject;
        GameObject ground = Instantiate(Resources.Load(groundPrefab, typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        ground.transform.parent = Grounds.transform;
        //ground.GetComponent<TerrainHandler>().ShiftTerrain(terrainId);
        //ground.GetComponent<TerrainHandler>().ShiftTerrain(terrainId);

        //GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        //GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        //fenceList.Add(fences);

        //Debug.Log("Obstacle: " + obstacleList[obstacleIndex].ToString());

        //Instantiate(obstacleList[obstacleIndex], new Vector3(x, 0, z), Quaternion.Euler(-90, 0, 0));

        if (rand.Next(100) > 50)
        {
            for (int l = 0; l < obstacleDensity; l++)
            {
                if (rand.Next(100) > 50)
                {
                    int xSize = 1;
                    int zSize = 1;
                    int zRotation = 0;

                    if (obstacleType == 7) //Hard code rock size to 4
                    {
                        xSize = 4;
                        zSize = 4;
                    }
                    else
                    {
                        xSize = (int)System.Math.Round((obstacleList[obstacleIndex].GetComponent<BoxCollider>().size.x * obstacleList[obstacleIndex].transform.localScale.x), 0);
                        zSize = (int)System.Math.Round((obstacleList[obstacleIndex].GetComponent<BoxCollider>().size.y * obstacleList[obstacleIndex].transform.localScale.y), 0);
                    }

                    if (obstacleType == 5) //Hard code rock size to 4
                    {
                        zRotation = -90;
                    }

                    GameObject obstacle = Instantiate(obstacleList[obstacleIndex], new Vector3(x + rand.Next(-(scaleX - 4 - xSize) / 2, (scaleX - 2 - xSize) / 2), 0, z + rand.Next(-(scaleZ - 3 - zSize) / 2, (scaleZ - 1 - zSize) / 2)), Quaternion.Euler(-90, 0, zRotation));

                    //GameObject obstacle = Instantiate(obstacleList[obstacleIndex], new Vector3(x + rand.Next(-(scaleX - 4) / 2, (scaleX - 3) / 2), 0, z + rand.Next(-(scaleZ - 3) / 2, (scaleZ - 2) / 2)), Quaternion.Euler(-90, 0, 0));

                    room.ObstacleList.Add(obstacle);
                    obstacle.transform.parent = Obstacles.transform;
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
            wall.transform.parent = Walls.transform;
            doors.transform.parent = Doors.transform;
            fences.transform.parent = Fences.transform;
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
            wall1.transform.parent = Walls.transform;
            wall2.transform.parent = Walls.transform;
        }
        if (room.SouthDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/SouthWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall);
            GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors/SouthDoorBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences/SouthFenceBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            fenceList.Add(fences);
            wall.transform.parent = Walls.transform;
            doors.transform.parent = Doors.transform;
            fences.transform.parent = Fences.transform;
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
            wall1.transform.parent = Walls.transform;
            wall2.transform.parent = Walls.transform;
        }
        if (room.WestDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/WestWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall);
            GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors/WestDoorBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences/WestFenceBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            fenceList.Add(fences);
            wall.transform.parent = Walls.transform;
            doors.transform.parent = Doors.transform;
            fences.transform.parent = Fences.transform;
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
            wall1.transform.parent = Walls.transform;
            wall2.transform.parent = Walls.transform;
        }
        if (room.EastDoorOpen)
        {
            GameObject wall = Instantiate(Resources.Load("Prefabs/Environment/ToonWalls/EastWallOpen", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            SetColorOfChildren(wall);
            GameObject doors = Instantiate(Resources.Load("Prefabs/Environment/Doors/EastDoorBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject fences = Instantiate(Resources.Load("Prefabs/Environment/Fences/EastFenceBase", typeof(GameObject)), new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
            fenceList.Add(fences);
            wall.transform.parent = Walls.transform;
            doors.transform.parent = Doors.transform;
            fences.transform.parent = Fences.transform;
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
            wall1.transform.parent = Walls.transform;
            wall2.transform.parent = Walls.transform;
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
