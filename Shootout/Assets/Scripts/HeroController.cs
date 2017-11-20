using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    Rigidbody rb;
    CharacterController controller;
    Animator animator;
    public string horizontalAxis;
    public string verticalAxis;
    public string leftAttackButton;
    public string rightAttackButton;
    public string upAttackButton;
    public string downAttackButton;

    public float speed;
    public float rotationDamping = 20f;

    int jumpHash = Animator.StringToHash("Jump");
    int idleStateHash = Animator.StringToHash("Idle");
    int walkStateHash = Animator.StringToHash("Walk");

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent(typeof(CharacterController)) as CharacterController;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void LateUpdate()
    {
        float moveV = Input.GetAxis(verticalAxis);
        float moveH = Input.GetAxis(horizontalAxis);
        //moveV *= Time.deltaTime;
        //moveH *= Time.deltaTime;
        animator.SetFloat("animationSpeedMultiplier", speed * 0.5f);
        //Debug.Log("movement: " + Mathf.Min(Mathf.Abs(moveV), Mathf.Abs(moveH)));
    }

    void FixedUpdate()
    {
        //Fix y position at 0
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //Handle movements based on axises
        float moveV = Input.GetAxis(verticalAxis);
        float moveH = Input.GetAxis(horizontalAxis);
        //Debug.Log("h: " + moveH);
        Vector3 movement = new Vector3(moveH, 0.0f, moveV);
        movement *= speed;
        controller.Move(movement * Time.deltaTime);


        //animator.SetFloat("Speed", move);

        //animator.speed = 1.0f;

        //Animation
        if (moveV != 0 || moveH != 0)
        {
            //animator.SetTrigger(walkStateHash);
            animator.SetTrigger("Walk");
            //animator.SetTrigger("Strafe Left");
            animator.ResetTrigger("Relax");
        }
        else
        {
            animator.SetTrigger("Relax");
            //animator.ResetTrigger("Strafe Left");
            animator.ResetTrigger("Walk");
        }

        if (Input.GetButton(leftAttackButton) | Input.GetButton(rightAttackButton) | Input.GetButton(upAttackButton) | Input.GetButton(downAttackButton))
        {
            //animator.SetTrigger("Punch Attack");
            animator.SetTrigger("Punch");
            //animator.ResetTrigger("Relax");
        }
        else
        {
            animator.ResetTrigger("Punch");
        }


        //Handle rotation, look in attack direction
        if (Input.GetButton(leftAttackButton))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(new Vector3(-1, 0, 0)),
                                 Time.deltaTime * rotationDamping);

            if (moveH < 0)
            {
                animator.SetTrigger("Walk");
                animator.ResetTrigger("Walk Backward");
                animator.ResetTrigger("Relax");
            }
            else if (moveH > 0)
            {
                animator.SetTrigger("Walk Backward");
                animator.ResetTrigger("Walk");
                animator.ResetTrigger("Relax");
            }
            else if(moveV != 0)
            {
                animator.SetTrigger("Walk");
                animator.ResetTrigger("Walk Backward");
                animator.ResetTrigger("Relax");
            }
            else
            {
                animator.SetTrigger("Relax");
                animator.ResetTrigger("Walk Backward");
                animator.ResetTrigger("Walk");
            }
        }
        else if (Input.GetButton(rightAttackButton))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(new Vector3(1, 0, 0)),
                                 Time.deltaTime * rotationDamping);
        }
        else if (Input.GetButton(upAttackButton))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(new Vector3(0, 0, 1)),
                                 Time.deltaTime * rotationDamping);
        }
        else if (Input.GetButton(downAttackButton))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(new Vector3(0, 0, -1)),
                                 Time.deltaTime * rotationDamping);
        }
        //If not attacking look forwards
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
