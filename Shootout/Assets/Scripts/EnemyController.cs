using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyController : MonoBehaviour {

    Animation anim;
    public float speed;

    CharacterController controller;
    public float rotationDamping = 20f;
    public string horizontalAxis;
    public string verticalAxis;
    public int health;
    public float meleeDamage;
    public bool GeneratePathToPlayer = false;
    public bool MovePathToPlayer = false;

    public List<GameObject> FoundPath = new List<GameObject>();
    public List<Vector3> FoundPathVector = new List<Vector3>();
    public Vector3 currentvector = Vector3.zero;
    public float smoothTime = 2.0f;


    private GameObject player1;
    private GameObject player2;

    // Use this for initialization
    void Start()
    {
        controller = (CharacterController)(GetComponent(typeof(CharacterController)));
        //anim = GetComponent<Animation>();
        //anim["Walk"].speed = 3.0f

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, 0.001f, transform.position.z);

        //PlayerHealth playerHealth = this.GetComponent<PlayerHealth>();

        //Handle movements based on axises
        //float moveV = Input.GetAxis(verticalAxis);
        //float moveH = Input.GetAxis(horizontalAxis);

        //GameObject player1 = GameObject.Find("Player1");
        //GameObject player2 = GameObject.Find("Player2");

        float moveH = player1.transform.position.x - gameObject.transform.position.x;
        float moveV = player1.transform.position.z - gameObject.transform.position.z;
        //Vector3 movement = new Vector3(moveH, 0.0f, moveV);

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


        #region using vectors
        if (GeneratePathToPlayer && FoundPath.Count == 0)
        {
            GeneratePathToPlayer = false;
            GetComponent<Star>().GetPathToPlayer(out FoundPathVector);
            FoundPathVector.Reverse();
        }

        Vector3 movement = new Vector3(0, 0, 0);

        if (FoundPathVector.Count == 0)
        {
            MovePathToPlayer = false;
        }
        if (MovePathToPlayer)
        {

            //MovePathToPlayer = false;
            movement = new Vector3(FoundPathVector.First().x - gameObject.transform.position.x,
                                   0.0f,
                                   FoundPathVector.First().z - gameObject.transform.position.z);

            Decimal x1 = System.Math.Round((Decimal)transform.position.x, 0, MidpointRounding.AwayFromZero);
            Decimal z1 = System.Math.Round((Decimal)transform.position.z, 0, MidpointRounding.AwayFromZero);
            Decimal x2 = System.Math.Round((Decimal)FoundPathVector.First().x, 0, MidpointRounding.AwayFromZero);
            Decimal z2 = System.Math.Round((Decimal)FoundPathVector.First().z, 0, MidpointRounding.AwayFromZero);
            if ((x1 == x2) && (z1 == z2))
            {
                FoundPathVector.RemoveAt(0);
                if (FoundPathVector.Count != 0)
                {
                    movement = new Vector3(FoundPathVector.First().x - gameObject.transform.position.x,
                                           0.0f,
                                           FoundPathVector.First().z - gameObject.transform.position.z);

                }
            }
        }
        #endregion 

        movement *= speed;

        //stand still

        //Play animation if player is moving
        //if (moveV != 0 || moveH != 0)
        //{
        //    anim.Play();
        //}
        //else
        //    anim.Stop();

        //controller.Move(movement * Time.deltaTime);
        transform.position= Vector3.SmoothDamp(transform.position, transform.position + movement, ref currentvector, smoothTime);
        //Debug.Log("Cat moving " + movement + " moveH = " + moveH + " moveV = " + moveV);

        //look forwards
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                    Quaternion.LookRotation(movement),
                                    Time.deltaTime * rotationDamping);

        }
    }

    void Walk()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("This entered cat: " + other);

        //if (other.tag == "Player1" || other.tag == "Player2")
        //{
        //    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        //    playerHealth.TakeDamage(meleeDamage);
        //}
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
