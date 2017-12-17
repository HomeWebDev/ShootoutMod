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
    public bool GeneratePathToPlayer = false;
    public bool MovePathToPlayer = false;
    public float moveGap = 1;
    public Vector3 LookAtposition;

    //public List<GameObject> FoundPath = new List<GameObject>();
    public List<Vector3> FoundPathVector = new List<Vector3>();
    public List<GameObject> BlockList = new List<GameObject>();
    public Vector3 currentvector = Vector3.zero;
    public float smoothTime = 0.2f;
    public float maxSpeed = 7f;


    private GameObject player1;
    private GameObject player2;

    // Use this for initialization
    void Start()
    {
        controller = (CharacterController)(GetComponent(typeof(CharacterController)));


        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
    }

    // Update is called once per frame
    void Update()
    {

        if (!UseNewMoveWithAstar)
            OldMove();
        else
            Move();

        #region using Gameobjects
        //if (GeneratePathToPlayer && FoundPath.Count == 0)
        //{
        //    GeneratePathToPlayer = false;
        //    GetComponent<Star>().GetPathToPlayer(out FoundPath);
        //    FoundPath.Reverse();
        //}

        //Vector3 movement = new Vector3(0, 0, 0);

        //if (MovePathToPlayer)
        //{
        //    //MovePathToPlayer = false;
        //    movement = new Vector3(FoundPath.First().transform.position.x - gameObject.transform.position.x, 
        //                           0.0f,
        //                           FoundPath.First().transform.position.z - gameObject.transform.position.z);

        //    Decimal x1 = System.Math.Round((Decimal)transform.position.x, 0, MidpointRounding.AwayFromZero);
        //    Decimal z1 = System.Math.Round((Decimal)transform.position.z, 0, MidpointRounding.AwayFromZero);
        //    Decimal x2 = System.Math.Round((Decimal)FoundPath.First().transform.position.x, 0, MidpointRounding.AwayFromZero);
        //    Decimal z2 = System.Math.Round((Decimal)FoundPath.First().transform.position.z, 0, MidpointRounding.AwayFromZero);
        //    if ((x1 == x2) && (z1 == z2))
        //    {
        //        Destroy(FoundPath.First());
        //        FoundPath.RemoveAt(0);
        //    }
        //}
        #endregion

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

            controller.Move(new Vector3(xsmooth - transform.position.x, 0f, zsmooth - transform.position.z));
            //transform.position = new Vector3(xsmooth, 0f, zsmooth);

            //transform.position = Vector3.SmoothDamp(transform.position, movement, ref currentvector, smoothTime, maxSpeed, Time.deltaTime);
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
        if (movement != Vector3.zero && MovePathToPlayer)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                    Quaternion.LookRotation(LookAtposition),
                                    Time.deltaTime * rotationDamping);

        }
        else
        {
            transform.LookAt(player1.transform);
        }
    }

    void Walk()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Crate" || collision.gameObject.tag == "Boundary")
        {
            if (!BlockList.Exists(v => v == collision.gameObject))
                BlockList.Add(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player1")
            FoundPathVector.Clear();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Crate" || collision.gameObject.tag == "Boundary")
        {
            if (!BlockList.Exists(v => v == collision.gameObject))
                BlockList.Add(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player1")
            FoundPathVector.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("This entered cat: " + other);

        //if (other.tag == "Player1" || other.tag == "Player2")
        //{
        //    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        //    playerHealth.TakeDamage(meleeDamage);
        //}

        if (other.gameObject.tag == "Crate" || other.gameObject.tag == "Boundary")
        {
            if (!BlockList.Exists(v => v == other.gameObject))
                BlockList.Add(other.gameObject);
        }

        if (other.gameObject.tag == "Player1")
            FoundPathVector.Clear();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("This entered cat: " + hit.gameObject);
        if (hit.gameObject.tag == "Player1" || hit.gameObject.tag == "Player2")
        {
            FoundPathVector.Clear();
            PlayerHealth playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(meleeDamage);
        }
        if (hit.gameObject.tag == "Crate" || hit.gameObject.tag == "Boundary")
        {
            if (!BlockList.Exists(v => v == hit.gameObject))
            BlockList.Add(hit.gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("This stays in cat: " + other);
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            FoundPathVector.Clear();
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(meleeDamage);
        }
        if (other.gameObject.tag == "Crate" || other.gameObject.tag == "Boundary")
        {
            if (!BlockList.Exists(v => v == other.gameObject))
                BlockList.Add(other.gameObject);
        }

    }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;

        if (health <= 0)
        {
            Destroy(gameObject);

            CheckDoorsOpen();
        } 
    }

    private void CheckDoorsOpen()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length <= 1)
        {
            GameObject levelController = GameObject.FindGameObjectWithTag("LevelController");
            levelController.GetComponent<LevelController>().OpenDoors();
        }
    }
}
