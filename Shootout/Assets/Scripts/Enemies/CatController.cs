using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    Animation anim;
    public float speed;

    CharacterController controller;
    public float rotationDamping = 20f;
    public string horizontalAxis;
    public string verticalAxis;
    public int health;

    private bool firstTime = true;
    private int initLoops = 4;

    // Use this for initialization
    void Start()
    {
        controller = (CharacterController)(GetComponent(typeof(CharacterController)));
        anim = GetComponent<Animation>();
        anim["Walk"].speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerHealth playerHealth = this.GetComponent<PlayerHealth>();

        //Handle movements based on axises
        //float moveV = Input.GetAxis(verticalAxis);
        //float moveH = Input.GetAxis(horizontalAxis);

        GameObject player1 = GameObject.Find("Player1");
        GameObject player2 = GameObject.Find("Player2");

        float moveH = player1.transform.position.x - gameObject.transform.position.x;
        float moveV = player1.transform.position.z - gameObject.transform.position.z;

        Vector3 movement = new Vector3(moveH, 0.0f, moveV);
        movement *= speed;


        //Play animation if player is moving
        if (moveV != 0 || moveH != 0)
        {
            anim.Play();
        }
        else
            anim.Stop();

        controller.Move(movement * Time.deltaTime);

        //Debug.Log("Cat moving " + movement + " moveH = " + moveH + " moveV = " + moveV);

        //look forwards
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                    Quaternion.LookRotation(movement),
                                    Time.deltaTime * rotationDamping);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("This entered cat: " + other);
        
    }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
    }
}
