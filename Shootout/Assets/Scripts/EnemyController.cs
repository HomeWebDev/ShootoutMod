using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyController : MonoBehaviour {

    public float speed;

    CharacterController controller;
    public float rotationDamping = 20f;
    public int health;
    public float meleeDamage;

    public bool UseNewMoveWithAstar = false;
    public bool UseNav = false;
    public bool GeneratePathToPlayer = false;
    public bool MovePathToPlayer = false;
    public bool UpdatePostionNew = false;
    public float moveGap = 1;
    public Vector3 LookAtposition;

    //public List<GameObject> FoundPath = new List<GameObject>();
    public List<Vector3> FoundPathVector = new List<Vector3>();
    public List<GameObject> BlockList = new List<GameObject>();
    public Vector3 currentvector = Vector3.zero;
    public float smoothTime = 0.2f;
    public float maxSpeed = 7f;
    public bool IsBoss;
    private int maxHealth;
    public UltimateStatusBar healthStatusBar;
    public DamageController damageController;
    private System.Random rand = new System.Random();


    public float timeBetweenAttacks = 0.2f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.

    public GameObject player1;                          // Reference to the player GameObject.
    private PlayerHealth playerHealth;                  // Reference to the player's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.                            // Timer for counting up to the next attack.

    //private GameObject player2;

    // Use this for initialization
    void Start()
    {
        maxHealth = health;

        healthStatusBar.UpdateStatus(health, maxHealth);


        //player2 = GameObject.Find("Player2");
    }

    private void Awake()
    {
        controller = (CharacterController)(GetComponent(typeof(CharacterController)));


        player1 = GameObject.FindGameObjectWithTag("Player1");
        playerHealth = player1.GetComponent<PlayerHealth>();
    }
    // Update is called once per frame
    void Update()
    {

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && health > 0)
        {
            // ... attack.
            Attack();
        }


        if (UseNewMoveWithAstar)
            Move();
        else if (UseNav)
            LookAtOnly();
        else
            OldMove();

    }

    private void LookAtOnly()
    {
        Vector3 neutralLookAtPlayerPosition = new Vector3(player1.transform.position.x, 1, player1.transform.position.z);
        transform.LookAt(player1.transform.position);
    }

    private void OldMove()
    {
        float moveH = player1.transform.position.x - transform.position.x;
        float moveV = player1.transform.position.z - transform.position.z;
        Vector3 movement = new Vector3(moveH, 0f, moveV);


        movement *= speed;

        controller.Move(movement * Time.deltaTime);


        if (movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(movement),
                        Time.deltaTime * rotationDamping);
    }

    private void Move()
    {
        Vector3 movement = new Vector3(0, 0, 0);
        float moveH = player1.transform.position.x - transform.position.x;
        float moveV = player1.transform.position.z - transform.position.z;

        #region using vectors
        if (GeneratePathToPlayer)
        {
            GeneratePathToPlayer = false;
            GetComponent<Star>().GetPathToPlayer(BlockList, transform.position, player1.transform.position, out FoundPathVector);
            GetComponent<Star>().CleanUp();
            if (FoundPathVector.Count != 0 && FoundPathVector.Count > 2)
            {
                FoundPathVector.Reverse();
                FoundPathVector.RemoveAt(0);
                //FoundPathVector.Last().Set(player1.transform.position.x, 0f, player1.transform.position.z);
                //FoundPathVector.Remove(FoundPathVector.Last());
                MovePathToPlayer = true;
            }
        }
        if (Vector3.Distance(transform.position, player1.transform.position) > moveGap)
        {
            GeneratePathToPlayer = true;
        }
        if (FoundPathVector.Count == 0)
        {
            MovePathToPlayer = false;
        }
        if (MovePathToPlayer)
        {

            //MovePathToPlayer = false;
            //movement = new Vector3(FoundPathVector.First().x - transform.position.x,
            //                       0.0f,
            //                       FoundPathVector.First().z - transform.position.z);
            movement = new Vector3(FoundPathVector.First().x,
                                   0.0f,
                                   FoundPathVector.First().z);

            Decimal x1 = System.Math.Round((Decimal)transform.position.x, 0, MidpointRounding.AwayFromZero);
            Decimal z1 = System.Math.Round((Decimal)transform.position.z, 0, MidpointRounding.AwayFromZero);
            Decimal x2 = System.Math.Round((Decimal)FoundPathVector.First().x, 0, MidpointRounding.AwayFromZero);
            Decimal z2 = System.Math.Round((Decimal)FoundPathVector.First().z, 0, MidpointRounding.AwayFromZero);
            if ((x1 == x2) && (z1 == z2))
            {
                if (FoundPathVector.Count != 0)
                {
                    //movement = new Vector3(FoundPathVector.First().x - transform.position.x,
                    //                       0.0f,
                    //                       FoundPathVector.First().z - transform.position.z);
                    movement = new Vector3(FoundPathVector.First().x,
                       0.0f,
                       FoundPathVector.First().z);

                }
                FoundPathVector.RemoveAt(0);
            }

            var xsmooth = Mathf.SmoothDamp(transform.position.x, movement.x, ref currentvector.x, smoothTime, maxSpeed, Time.deltaTime);
            var zsmooth = Mathf.SmoothDamp(transform.position.z, movement.z, ref currentvector.z, smoothTime, maxSpeed, Time.deltaTime);

            if (UpdatePostionNew)
            {
                transform.position = new Vector3(xsmooth, 0f, zsmooth);

            }
            else
            {
                controller.Move(new Vector3(xsmooth - transform.position.x, 0f, zsmooth - transform.position.z));
                //transform.position = new Vector3(xsmooth, 0f, zsmooth);

                //transform.position = Vector3.SmoothDamp(transform.position, movement, ref currentvector, smoothTime, maxSpeed, Time.deltaTime);
            }
        }
        #endregion

        //movement *= speed;
        if (!MovePathToPlayer)
        {
            //movement = new Vector3(player1.transform.position.x - transform.position.x, 0f, player1.transform.position.z - transform.position.z);
            //transform.position = Vector3.SmoothDamp(transform.position, player1.transform.position, ref currentvector, delaytime);
            //movement = new Vector3(player1.transform.position.x, 0f, player1.transform.position.z);
        }

        //controller.Move(movement * Time.deltaTime);
        //Debug.Log("Cat moving " + movement + " moveH = " + moveH + " moveV = " + moveV);


        LookAtposition = new Vector3(player1.transform.position.x - transform.position.x,
                                        0f,
                                        player1.transform.position.z - transform.position.z);

        //look forwards
        //if (movement != Vector3.zero && MovePathToPlayer)
        //{
        //    transform.LookAt(movement);
        //    //transform.rotation = Quaternion.Slerp(transform.rotation,
        //    //                        Quaternion.LookRotation(LookAtposition),
        //    //                        Time.deltaTime * rotationDamping);

        //}
        //else
        //{
        //    //Vector3 neutralLookAtPlayerPosition = new Vector3(player1.transform.position.x, 1, player1.transform.position.z);
        //    //transform.LookAt(neutralLookAtPlayerPosition);
        //    transform.LookAt(LookAtposition);
        //    //transform.LookAt(player1.transform);
        //}

        //Temporary fix, always look at player to avoid strange behaviour when switching between if-else above
        Vector3 neutralLookAtPlayerPosition = new Vector3(player1.transform.position.x, 1, player1.transform.position.z);
        transform.LookAt(neutralLookAtPlayerPosition);
    }

    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.health > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
    }

    #region Events maybe of use
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Crate" || collision.gameObject.tag == "Boundary" || collision.gameObject.tag == "Obstacle")
    //    {
    //        if (!BlockList.Exists(v => v == collision.gameObject))
    //            BlockList.Add(collision.gameObject);
    //    }

    //    if (collision.gameObject.tag == "Player1")
    //        FoundPathVector.Clear();
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Crate" || collision.gameObject.tag == "Boundary" || collision.gameObject.tag == "Obstacle")
    //    {
    //        if (!BlockList.Exists(v => v == collision.gameObject))
    //            BlockList.Add(collision.gameObject);
    //    }

    //    if (collision.gameObject.tag == "Player1")
    //        FoundPathVector.Clear();
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Debug.Log("This entered cat: " + other);

    //    //if (other.tag == "Player1" || other.tag == "Player2")
    //    //{
    //    //    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
    //    //    playerHealth.TakeDamage(meleeDamage);
    //    //}

    //    if (other.gameObject.tag == "Crate" || other.gameObject.tag == "Boundary" || other.gameObject.tag == "Obstacle")
    //    {
    //        if (!BlockList.Exists(v => v == other.gameObject))
    //            BlockList.Add(other.gameObject);
    //    }

    //    if (other.gameObject.tag == "Player1")
    //        FoundPathVector.Clear();
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    //Debug.Log("This entered cat: " + hit.gameObject);
    //    if (hit.gameObject.tag == "Player1" || hit.gameObject.tag == "Player2")
    //    {
    //        FoundPathVector.Clear();
    //        PlayerHealth playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
    //        playerHealth.TakeDamage(meleeDamage);
    //    }
    //    if (hit.gameObject.tag == "Crate" || hit.gameObject.tag == "Boundary" || hit.gameObject.tag == "Obstacle")
    //    {
    //        if (!BlockList.Exists(v => v == hit.gameObject))
    //            BlockList.Add(hit.gameObject);
    //    }

    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    //Debug.Log("This stays in cat: " + other);
    //    if (other.tag == "Player1" || other.tag == "Player2")
    //    {
    //        FoundPathVector.Clear();
    //        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
    //        playerHealth.TakeDamage(meleeDamage);
    //    }
    //    if (other.gameObject.tag == "Crate" || other.gameObject.tag == "Boundary" || other.gameObject.tag == "Obstacle")
    //    {
    //        if (!BlockList.Exists(v => v == other.gameObject))
    //            BlockList.Add(other.gameObject);
    //    }

    //}
    #endregion


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player1)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player1)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;

        healthStatusBar.UpdateStatus(health, maxHealth);
        damageController.TakeDamage();

        if (health <= 0)
        {
            if (IsBoss)
            {
                GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>().WayPoint.SetActive(true);
            }

            damageController.Die();

            maxSpeed = 0;
            speed = 0;

            //Destroy(gameObject);
            StartCoroutine(DelayedDelete());

            tag = "Untagged";
        }
    }

    IEnumerator DelayedDelete()
    {
        yield return new WaitForSeconds(1.5f);

        if(IsBoss)
        {
            SpawnBossItem();
        }
        else
        {
            SpawnItem();
        }

        Destroy(gameObject);

        CheckDoorsOpen();
    }

    private void SpawnItem()
    {
        if (rand.Next(100) > 50)
        {
            int heartId = rand.Next(8) + 1;
            //int heartId = rand.Next(4) + 5;

            Debug.Log("HeartId: " + heartId);

            GameObject item1 = Instantiate(Resources.Load("Prefabs/Pickups/Replenish/" + heartId, typeof(GameObject)), new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    private void SpawnBossItem()
    {
        int itemIndex = UnityEngine.Random.Range(17, 33);

        //Place item 2.8 meters below waypoint
        GameObject levelController = GameObject.FindGameObjectWithTag("LevelController");
        int xBossRoom = (int)((transform.position.x / levelController.GetComponent<LevelController>().scaleX) + 0.5f) + 0;
        int zBossRoom = levelController.GetComponent<LevelController>().GetLevelRepresentation().RoomArray.GetLength(0) - (int)(((transform.position.z) / levelController.GetComponent<LevelController>().scaleZ) + 1.5f);
        float posX = xBossRoom * levelController.GetComponent<LevelController>().scaleX;
        float posZ = (levelController.GetComponent<LevelController>().GetLevelRepresentation().RoomArray.GetLength(0) - 1 - zBossRoom) * levelController.GetComponent<LevelController>().scaleZ - 2.8f;
        GameObject item1 = Instantiate(Resources.Load("Prefabs/PickupsLevel1/" + itemIndex, typeof(GameObject)), new Vector3(posX, 0.5f, posZ), Quaternion.Euler(0, 0, 0)) as GameObject;
        //GameObject item1 = Instantiate(Resources.Load("Prefabs/PickupsLevel1/" + itemIndex, typeof(GameObject)), new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.Euler(0, 0, 0)) as GameObject;

        //Debug.Log("transform.position.x: " + transform.position.x + " transform.position.z: " + transform.position.z);
        //Debug.Log("posX: " + posX + " posZ: " + posZ);

        item1.GetComponent<ItemName>().Group = "GroupBoss";
    }

    private void CheckDoorsOpen()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length <= 0)
        {
            GameObject levelController = GameObject.FindGameObjectWithTag("LevelController");
            levelController.GetComponent<LevelController>().OpenDoors();
        }
    }
}
