using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    private List<GameObject> fenceList = new List<GameObject>();

    void Awake()
    {
        GenerateLevel();

        OpenDoors();

        //Debug.Log("This was run");
    }

    public void CloseDoors()
    {
        foreach (var fence in fenceList)
        {
            fence.SetActive(true);
        }
    }

    public void OpenDoors()
    {
        foreach (var fence in fenceList)
        {
            Debug.Log("Fence: " + fence);
            fence.SetActive(false);
        }
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void GenerateLevel()
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
