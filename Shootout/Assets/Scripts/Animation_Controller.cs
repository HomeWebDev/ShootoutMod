using UnityEngine;
using System.Collections;

/// <summary>
/// Class used to hande player animations and movements
/// </summary>
public class Animation_Controller : MonoBehaviour
{
    Animation anim;
    public float speed;

    CharacterController controller;
    public float rotationDamping = 20f;
    public string horizontalAxis;
    public string verticalAxis;
    public string leftFireButton;
    public string rightFireButton;
    public string upFireButton;
    public string downFireButton;

    public float fireRate;
    private float DefaultFirerate = 0.25f;
    private float nextFire;

    public Transform shotSpawn;
    public GameObject shot;
    public GameObject akm;
    public GameObject pistol;

    private bool firstTime = true;
    private int initLoops = 4;


    // Use this for initialization
    void Start ()
    {
        controller = (CharacterController)(GetComponent(typeof(CharacterController)));
        anim = GetComponent<Animation>();
        anim["Take 001"].speed = 3.0f;
        akm.SetActive(false);
        pistol.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //Set fire rate based on current weapon
        //if (akm.activeSelf)
        //{
        //    fireRate = DefaultFirerate / 2;
        //}
        //else if(pistol.activeSelf)
        //{
        //    fireRate = DefaultFirerate;
        //}

        //Fix y position at 0
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        PlayerHealth playerHealth = this.GetComponent<PlayerHealth>();

        //Handle movements based on axises
        float moveV = Input.GetAxis(verticalAxis);
        float moveH = Input.GetAxis(horizontalAxis);
        //Debug.Log("h: " + moveH);

        Vector3 movement = new Vector3(moveH, 0.0f, moveV);
        movement *= speed;


        if (Input.GetButton(leftFireButton) & Time.time > nextFire & playerHealth.health > 0)
        {
            nextFire = Time.time + fireRate;
            shot.tag = "shot";
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 270 + movement.z, 0));
        }
        if (Input.GetButton(rightFireButton) & Time.time > nextFire & playerHealth.health > 0)
        {
            nextFire = Time.time + fireRate;
            shot.tag = "shot";
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 90 - movement.z, 0));
        }
        if (Input.GetButton(upFireButton) & Time.time > nextFire & playerHealth.health > 0)
        {
            nextFire = Time.time + fireRate;
            shot.tag = "shot";
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 0 + movement.x, 0));
        }
        if (Input.GetButton(downFireButton) & Time.time > nextFire & playerHealth.health > 0)
        {
            nextFire = Time.time + fireRate;
            shot.tag = "shot";
            Instantiate(shot, shotSpawn.position, Quaternion.Euler(0, 180 - movement.x, 0));
        }

        //Play animation if player is moving
        if (moveV != 0 || moveH != 0)
        {
            anim.Play();
        }
        else
            anim.Stop();

        controller.Move(movement * Time.deltaTime);

        //Handle rotation, look in shooting direction
        if (Input.GetButton(leftFireButton))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(new Vector3(-1, 0, 0)),
                                 Time.deltaTime * rotationDamping);
        }
        else if (Input.GetButton(rightFireButton))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(new Vector3(1, 0, 0)),
                                 Time.deltaTime * rotationDamping);
        }
        else if (Input.GetButton(upFireButton))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(new Vector3(0, 0, 1)),
                                 Time.deltaTime * rotationDamping);
        }
        else if (Input.GetButton(downFireButton))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(new Vector3(0, 0, -1)),
                                 Time.deltaTime * rotationDamping);
        }
        //If not shooting look forwards
        else
        {
            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                     Quaternion.LookRotation(movement),
                                     Time.deltaTime * rotationDamping);

            }
        }
    }
}
