using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherController : MonoBehaviour {

    private CharacterController characterController;
    private Vector3 lastPosition;
    private Vector3 position;

    private Animator animator;
    private int walkHash = Animator.StringToHash("Walk");
    private int runHash = Animator.StringToHash("Run");
    private int arrowAttackHash = Animator.StringToHash("Arrow Attack");
    private int multipleShotsAttackHash = Animator.StringToHash("Multiple Shots Attack");
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


        if (Input.GetKey(KeyCode.C))
        {
            animator.SetTrigger(walkHash);
        }
        else
        {
            animator.ResetTrigger(walkHash);
        }

        if (Input.GetKey(KeyCode.X))
        {
            animator.SetTrigger(runHash);
        }
        else
        {
            animator.ResetTrigger(runHash);
        }


        if (Input.GetKey(KeyCode.B))
        {
            animator.SetTrigger(arrowAttackHash);
        }
        else
        {
            animator.ResetTrigger(arrowAttackHash);
        }

        if (Input.GetKey(KeyCode.V))
        {
            animator.SetTrigger(multipleShotsAttackHash);
        }
        else
        {
            animator.ResetTrigger(multipleShotsAttackHash);
        }
    }
}
