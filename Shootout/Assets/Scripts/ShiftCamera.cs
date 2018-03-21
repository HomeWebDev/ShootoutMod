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
    private System.Random rand = new System.Random();
    private LevelController levelController;
    private GameObject player1;
    private int levelId = 1;

    // Update is called once per frame
    //  private void Update()
    //  {
    //      if(player1 == null)
    //      {
    //          return;
    //      }
    //      if (levelController == null)
    //      {
    //          return;
    //      }

    //      numberOfUpdates++;
    //      Debug.Log("numberOfUpdates: " + numberOfUpdates);

    //      int xRoomPos = (int)(player1.transform.position.x / levelController.scaleX);
    //      int zRoomPos = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)((player1.transform.position.z) / levelController.scaleZ) - 1;

    //      int xPlayerRoom = (int)((player1.transform.position.x / levelController.scaleX) + 0.5f) + 0;
    //      int zPlayerRoom = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)(((player1.transform.position.z) / levelController.scaleZ) + 1.5f);

    //      float xPlayerRelativePos = player1.transform.position.x / levelController.scaleX - xPlayerRoom + 0.5f;
    //      float zPlayerRelativePos =  zPlayerRoom + 0.5f - (levelController.GetLevelRepresentation().RoomArray.GetLength(0) - player1.transform.position.z / levelController.scaleZ - 1);


    //      //Debug.Log("xPlayerRelativePos: " + xPlayerRelativePos + " , zPlayerRelativePos: " + zPlayerRelativePos);

    //      bool blockNorth = false, blockSouth = false, blockWest = false, blockEast = false;

    //      //Debug.Log("xPlayerRoom: " + xPlayerRoom + " , zPlayerRoom: " + zPlayerRoom);

    //      if (!levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].NorthWallOpen)
    //      {
    //          blockNorth = true;
    //          if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].WestWallOpen)
    //          {
    //              if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom - 1].NorthWallOpen)
    //              {
    //                  blockNorth = false;
    //              }
    //          }
    //          if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].EastWallOpen)
    //          {
    //              if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom + 1].NorthWallOpen)
    //              {
    //                  blockNorth = false;
    //              }
    //          }
    //      }
    //      if (!levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].SouthWallOpen)
    //      {
    //          blockSouth = true;
    //          if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].WestWallOpen)
    //          {
    //              if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom - 1].SouthWallOpen)
    //              {
    //                  blockSouth = false;
    //              }
    //          }
    //          if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].EastWallOpen)
    //          {
    //              if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom + 1].SouthWallOpen)
    //              {
    //                  blockSouth = false;
    //              }
    //          }
    //      }
    //      if (!levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].WestWallOpen)
    //      {
    //          blockWest = true;
    //          if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].NorthWallOpen)
    //          {
    //              if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom - 1, xPlayerRoom].WestWallOpen)
    //              {
    //                  blockWest = false;
    //              }
    //          }
    //          if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].SouthWallOpen)
    //          {
    //              if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom + 1, xPlayerRoom].WestWallOpen)
    //              {
    //                  blockWest = false;
    //              }
    //          }
    //      }
    //      if (!levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].EastWallOpen)
    //      {
    //          blockEast = true;
    //          if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].NorthWallOpen)
    //          {
    //              if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom - 1, xPlayerRoom].EastWallOpen)
    //              {
    //                  blockEast = false;
    //              }
    //          }
    //          if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom, xPlayerRoom].SouthWallOpen)
    //          {
    //              if (levelController.GetLevelRepresentation().RoomArray[zPlayerRoom + 1, xPlayerRoom].EastWallOpen)
    //              {
    //                  blockEast = false;
    //              }
    //          }
    //      }

    //      //Debug.Log("" + blockNorth + blockSouth + blockWest + blockEast);

    //      Vector3 newCameraPos;

    //      float xPos = player1.transform.position.x;
    //      float yPos = Camera.main.transform.position.y;
    //      float zPos = player1.transform.position.z - 4.2f;

    //float xPosCentered = xPlayerRoom * levelController.scaleX;
    //      float zPosCentered = (levelController.GetLevelRepresentation().RoomArray.GetLength(0) - 1.0f) * levelController.scaleZ - zPlayerRoom * levelController.scaleZ - 4.2f;

    //      if (blockNorth & zPlayerRelativePos > 0.5)
    //      {
    //	zPos = zPosCentered;
    //      }
    //      if (blockSouth & zPlayerRelativePos < 0.5)
    //      {
    //	zPos = zPosCentered;
    //      }
    //      if (blockWest & xPlayerRelativePos < 0.5)
    //      {
    //	xPos = xPosCentered;
    //      }
    //      if (blockEast & xPlayerRelativePos > 0.5)
    //      {
    //	xPos = xPosCentered;
    //      }

    //      newCameraPos = new Vector3(xPos, yPos, zPos);

    //      if (Math.Abs(newCameraPos.x - Camera.main.transform.position.x) < 2 & Math.Abs(newCameraPos.z - Camera.main.transform.position.z) < 2)
    //      {
    //          Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCameraPos, 2.0f);
    //      }
    //      else
    //      {
    //          Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newCameraPos, 0.01f);
    //      }
    //  }

    IEnumerator LoadAsyncronously()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadLevelScene");

        

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }

    private bool ShouldCloseDoors()
    {
        int xThisRoom = (int)(((transform.position.x + xShift/2) / levelController.scaleX) + 0.5f) + 0;
        int zThisRoom = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)(((transform.position.z + zShift/2) / levelController.scaleZ) + 1.5f);

        Debug.Log("Content: " + levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom]);
        //Debug.Log("xThisRoom: " + xThisRoom + " , zThisRoom: " + zThisRoom);

        if (levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom] == LevelRepresentation.ContentType.EnemyLevel1)
        {
            SetAllConnectedRoomsEmpty(zThisRoom, xThisRoom);
            return true;
        }
        else if (levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom] == LevelRepresentation.ContentType.BossLevel1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetAllConnectedRoomsEmpty(int zThisRoom, int xThisRoom)
    {
        levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom] = LevelRepresentation.ContentType.NoContent;

        if (levelController.GetLevelRepresentation().RoomArray[zThisRoom, xThisRoom].NorthWallOpen)
        {
            if (levelController.GetLevelRepresentation().ContentArray[zThisRoom - 1, xThisRoom] != LevelRepresentation.ContentType.NoContent)
            {
                SetAllConnectedRoomsEmpty(zThisRoom - 1, xThisRoom);
            }
        }

        if (levelController.GetLevelRepresentation().RoomArray[zThisRoom, xThisRoom].SouthWallOpen)
        {
            if (levelController.GetLevelRepresentation().ContentArray[zThisRoom + 1, xThisRoom] != LevelRepresentation.ContentType.NoContent)
            {
                SetAllConnectedRoomsEmpty(zThisRoom + 1, xThisRoom);
            }
        }

        if (levelController.GetLevelRepresentation().RoomArray[zThisRoom, xThisRoom].WestWallOpen)
        {
            if (levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom - 1] != LevelRepresentation.ContentType.NoContent)
            {
                SetAllConnectedRoomsEmpty(zThisRoom, xThisRoom - 1);
            }
        }

        if (levelController.GetLevelRepresentation().RoomArray[zThisRoom, xThisRoom].EastWallOpen)
        {
            if (levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom + 1] != LevelRepresentation.ContentType.NoContent)
            {
                SetAllConnectedRoomsEmpty(zThisRoom, xThisRoom + 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnterTrigger");
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject progressController = GameObject.FindGameObjectWithTag("ProgressController");
        levelId = progressController.GetComponent<ProgressController>().NextLevel - 1;
        //GameObject player2 = GameObject.Find("Player2");

        //Debug.Log("Player1: " + player1);

        if (other.tag == "Player1")
        { 
            if (ShouldCloseDoors())
            {
                float xPlayerPos;
                float zPlayerPos;

                if (xShift > 0)
                {
                    xPlayerPos = player1.transform.position.x + 0.5f;
                }
                else if (xShift < 0)
                {
                    xPlayerPos = player1.transform.position.x - 0.5f;
                }
                else
                {
                    xPlayerPos = player1.transform.position.x;
                }
                if (zShift > 0)
                {
                    zPlayerPos = player1.transform.position.z + 0.5f;
                }
                else if (zShift < 0)
                {
                    zPlayerPos = player1.transform.position.z - 0.5f;
                }
                else
                {
                    zPlayerPos = player1.transform.position.z;
                }
                player1.transform.position = new Vector3(xPlayerPos, player1.transform.position.y, zPlayerPos);

                CloseDoors();

                int xThisRoom = (int)(((transform.position.x + xShift / 2) / levelController.scaleX) + 0.5f) + 0;
                int zThisRoom = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)(((transform.position.z + zShift / 2) / levelController.scaleZ) + 1.5f);

                //Debug.Log("" + levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom]);

                if (levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom] == LevelRepresentation.ContentType.BossLevel1)
                {
                    SpawnBoss(zThisRoom, xThisRoom);
                }
                else
                {
                    SpawnEnemies(zThisRoom, xThisRoom);
                }
            }
        }




        //StartCoroutine(LerpFromTo(Camera.main.transform.position, newDesiredPosition, shiftSpeed));
        //SpawnEnemies(newDesiredPosition);
            

        //if (other.tag == "Player1")
        //{
        //    player2.transform.position = player1.transform.position;
        //}
        //else
        //{
        //    player1.transform.position = player2.transform.position;
        //}
        //Debug.Log("Camera shift");
    }

    private void SpawnBoss(int zThisRoom, int xThisRoom)
    {
        float xPosCentered = xThisRoom * levelController.scaleX;
        float zPosCentered = (levelController.GetLevelRepresentation().RoomArray.GetLength(0) - 1) * levelController.scaleZ - zThisRoom * levelController.scaleZ;

        Vector3 bossPosition = new Vector3(xPosCentered, 0, zPosCentered);

        GameObject cat = Instantiate(Resources.Load("Prefabs/Enemies/cat", typeof(GameObject)), bossPosition, Quaternion.Euler(0, 0, 0)) as GameObject;

        Vector3 adjustedScale = new Vector3(cat.transform.localScale.x + levelId - 1, player1.transform.localScale.y + levelId - 1, player1.transform.localScale.z + levelId - 1);
        cat.transform.localScale = adjustedScale;

        levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom] = LevelRepresentation.ContentType.NoContent;
    }

    private void SpawnEnemies(int zThisRoom, int xThisRoom)
    {
        SpawnEnemiesInAllConnectedRooms(zThisRoom, xThisRoom);
    }

    private void SpawnEnemiesInAllConnectedRooms(int zThisRoom, int xThisRoom)
    {
        float xPosCentered = xThisRoom * levelController.scaleX;
        float zPosCentered = (levelController.GetLevelRepresentation().RoomArray.GetLength(0) - 1) * levelController.scaleZ - zThisRoom * levelController.scaleZ;

        //Debug.Log("RoomArray.GetLength(0): " + levelController.GetLevelRepresentation().RoomArray.GetLength(0));

        Vector3 enemyPosition = new Vector3(xPosCentered, 0, zPosCentered);

        InstantiateEnemies(enemyPosition);

        //GameObject cat = Instantiate(Resources.Load("Prefabs/Enemies/cat", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;

        levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom] = LevelRepresentation.ContentType.EnemiesSpawned;


        if (levelController.GetLevelRepresentation().RoomArray[zThisRoom, xThisRoom].NorthWallOpen)
        {
            if (levelController.GetLevelRepresentation().ContentArray[zThisRoom - 1, xThisRoom] != LevelRepresentation.ContentType.EnemiesSpawned)
            {
                SpawnEnemiesInAllConnectedRooms(zThisRoom - 1, xThisRoom);
            }
        }

        if (levelController.GetLevelRepresentation().RoomArray[zThisRoom, xThisRoom].SouthWallOpen)
        {
            if (levelController.GetLevelRepresentation().ContentArray[zThisRoom + 1, xThisRoom] != LevelRepresentation.ContentType.EnemiesSpawned)
            {
                SpawnEnemiesInAllConnectedRooms(zThisRoom + 1, xThisRoom);
            }
        }

        if (levelController.GetLevelRepresentation().RoomArray[zThisRoom, xThisRoom].WestWallOpen)
        {
            if (levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom - 1] != LevelRepresentation.ContentType.EnemiesSpawned)
            {
                SpawnEnemiesInAllConnectedRooms(zThisRoom, xThisRoom - 1);
            }
        }

        if (levelController.GetLevelRepresentation().RoomArray[zThisRoom, xThisRoom].EastWallOpen)
        {
            if (levelController.GetLevelRepresentation().ContentArray[zThisRoom, xThisRoom + 1] != LevelRepresentation.ContentType.EnemiesSpawned)
            {
                SpawnEnemiesInAllConnectedRooms(zThisRoom, xThisRoom + 1);
            }
        }
    }

    private void InstantiateEnemies(Vector3 enemyPosition)
    {
        if (levelId == 1)
        {
            InstantiateLevel1Enemies(enemyPosition);
        }
        else if (levelId == 2)
        {
            InstantiateLevel2Enemies(enemyPosition);
        }
        else if (levelId == 3)
        {
            InstantiateLevel3Enemies(enemyPosition);
        }
        else if (levelId == 4)
        {
            InstantiateLevel4Enemies(enemyPosition);
        }
        else if (levelId == 5)
        {
            InstantiateLevel5Enemies(enemyPosition);
        }
        else if (levelId == 6)
        {
            InstantiateLevel6Enemies(enemyPosition);
        }
        else
        {
            if (rand.NextDouble() > 0.50)
            {
                InstantiateBats(enemyPosition);
            }
            else
            {
                InstantiateGhosts(enemyPosition);
            }
        }
    }

    private void InstantiateLevel1Enemies(Vector3 enemyPosition)
    {
        if (rand.NextDouble() > 0.50)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Blue", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void InstantiateLevel2Enemies(Vector3 enemyPosition)
    {
        if (rand.NextDouble() > 0.50)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Blue", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Blue", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void InstantiateLevel3Enemies(Vector3 enemyPosition)
    {
        if (rand.NextDouble() > 0.75)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else if (rand.NextDouble() > 0.67)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Pink", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else if (rand.NextDouble() > 0.5)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Blue", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Blue", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void InstantiateLevel4Enemies(Vector3 enemyPosition)
    {
        if (rand.NextDouble() > 0.75)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else if (rand.NextDouble() > 0.67)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Pink", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Pink", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else if (rand.NextDouble() > 0.5)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Blue", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Blue", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void InstantiateLevel5Enemies(Vector3 enemyPosition)
    {
        if (rand.NextDouble() > 0.75)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Red", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else if (rand.NextDouble() > 0.67)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Pink", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else if (rand.NextDouble() > 0.5)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Orange", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void InstantiateLevel6Enemies(Vector3 enemyPosition)
    {
        if (rand.NextDouble() > 0.50)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Red", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy1 = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Red", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Red", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Orange", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy1 = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Orange", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            GameObject enemy2 = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Orange", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void InstantiateBats(Vector3 enemyPosition)
    {
        if (rand.NextDouble() > 0.67)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else if (rand.NextDouble() > 0.50)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Pink", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Bats/Toon Bat-Red", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void InstantiateGhosts(Vector3 enemyPosition)
    {
        if (rand.NextDouble() > 0.67)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Blue", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else if (rand.NextDouble() > 0.50)
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Green", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemies/Ghosts/Toon Ghost-Orange", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void SpawnEnemies(Vector3 roomPosition)
    {
        //Get room position
        levelController = FindObjectOfType(typeof(LevelController)) as LevelController;
        int xRoomPos = (int)(roomPosition.x / levelController.scaleX);
        int zRoomPos = levelController.GetLevelRepresentation().RoomArray.GetLength(0) - (int)((roomPosition.z + 4.2f) / levelController.scaleZ) - 1;

        Debug.Log("x: " + roomPosition.x + " z: " + roomPosition.z);
        Debug.Log("xint: " + xRoomPos + " zint: " + zRoomPos);
        Debug.Log("levelController.GetLevelRepresentation().ContentArray.GetLength(0): " + levelController.GetLevelRepresentation().ContentArray.GetLength(0) + " levelController.GetLevelRepresentation().ContentArray.GetLength(1): " + levelController.GetLevelRepresentation().ContentArray.GetLength(1));
        //Debug.Log("ContentType: " + levelController.GetLevelRepresentation().ContentArray[xRoomPos, zRoomPos]);

        //Add enemies and close doors
        if (levelController.GetLevelRepresentation().ContentArray[zRoomPos, xRoomPos] == LevelRepresentation.ContentType.EnemyLevel1)
        {
            CloseDoors(roomPosition);
            //TODO: Add random enemies of appropriate level
            Vector3 enemyPosition = new Vector3(roomPosition.x, 0, roomPosition.z + 4.2f);
            GameObject cat = Instantiate(Resources.Load("Prefabs/Enemies/cat", typeof(GameObject)), enemyPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
            levelController.GetLevelRepresentation().ContentArray[zRoomPos, xRoomPos] = LevelRepresentation.ContentType.NoContent;
        }
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

    private void CloseDoors()
    {
        GameObject levelController = GameObject.FindGameObjectWithTag("LevelController");
        levelController.GetComponent<LevelController>().CloseDoors();

    }

    private bool ShouldShiftCamera(Collider other)
    {
        if (xShift == 0)
        {
            if (zShift > 0)
            {
                if (other.transform.position.z > Camera.main.transform.position.z)
                {
                    return true;
                }
            }
            else
            {
                if (other.transform.position.z < Camera.main.transform.position.z)
                {
                    return true;
                }
            }
        }
        if (zShift == 0)
        {
            if (xShift > 0)
            {
                if (other.transform.position.x > Camera.main.transform.position.x)
                {
                    return true;
                }
            }
            else
            {
                if (other.transform.position.x < Camera.main.transform.position.x)
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
        player1 = GameObject.FindGameObjectWithTag("Player1");

        Collectibles.current = new Collectibles();
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