using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    private CharacterController characterController;
    private Vector3 lastPosition;
    private Vector3 position;

    private Animator animator;
    private int flyForwardHash = Animator.StringToHash("FlyForward");
    private int biteAttackHash = Animator.StringToHash("Bite Attack");
    private int surroundAttackHash = Animator.StringToHash("Surround Attack");
    private GameObject player1;
    private System.Random rand = new System.Random();

    public int AttackPattern = 0;
    public float AttackDistance = 2;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        player1 = GameObject.FindGameObjectWithTag("Player1");

        position = GetComponent<Transform>().position;
        lastPosition = position;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 moved = GetComponent<Transform>().position - lastPosition;
        lastPosition = GetComponent<Transform>().position;

        float velocity = moved.magnitude / Time.deltaTime;

        //if (velocity > 0.1f)
        //{
        //    animator.SetTrigger(flyForwardHash);
        //}
        //else
        //{
        //    animator.ResetTrigger(flyForwardHash);
        //}




        if ((GetComponent<Transform>().position - player1.transform.position).magnitude < AttackDistance)
        {
            if(AttackPattern == 0)
            {
                animator.SetTrigger(biteAttackHash);
                animator.ResetTrigger(surroundAttackHash);
            }
            else if (AttackPattern == 1)
            {
                animator.SetTrigger(surroundAttackHash);
                animator.ResetTrigger(biteAttackHash);
            }
            else
            {
                if(rand.Next(0, 100) > 50)
                {
                    animator.SetTrigger(biteAttackHash);
                    animator.ResetTrigger(surroundAttackHash);
                }
                else
                {
                    animator.SetTrigger(surroundAttackHash);
                    animator.ResetTrigger(biteAttackHash);
                }
            }
        }
        else
        {
            animator.ResetTrigger(biteAttackHash);
            animator.ResetTrigger(surroundAttackHash);
        }

        //if ((lastPosition - player1.transform.position).magnitude < 2)
        //{
        //    animator.SetTrigger(surroundAttackHash);
        //}
        //else
        //{
        //    animator.ResetTrigger(surroundAttackHash);
        //}

        //if (Input.GetKey(KeyCode.B))
        //{
        //    animator.SetTrigger(biteAttackHash);
        //}
        //else
        //{
        //    animator.ResetTrigger(biteAttackHash);
        //}

        //if (Input.GetKey(KeyCode.V))
        //{
        //    animator.SetTrigger(surroundAttackHash);
        //}
        //else
        //{
        //    animator.ResetTrigger(surroundAttackHash);
        //}
    }
}
