using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatController : MonoBehaviour {

    private CharacterController characterController;
    private Vector3 lastPosition;
    private Vector3 position;

    private Animator animator;
    private int flyForwardHash = Animator.StringToHash("FlyForward");
    private int biteAttackHash = Animator.StringToHash("Bite Attack");
    private int soundwaveAttackHash = Animator.StringToHash("Soundwave Attack");
    private GameObject player1;
    private System.Random rand = new System.Random();

    public int AttackPattern = 0;
    public float AttackDistance = 2;
    public GameObject Soundwave;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        player1 = GameObject.FindGameObjectWithTag("Player1");
        position = GetComponent<Transform>().position;
        lastPosition = position;
    }
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update () {

        if ((GetComponent<Transform>().position - player1.transform.position).magnitude < AttackDistance)
        {
            if (AttackPattern == 0)
            {
                animator.SetTrigger(biteAttackHash);
                animator.ResetTrigger(soundwaveAttackHash);
            }
            else if (AttackPattern == 1)
            {
                animator.SetTrigger(soundwaveAttackHash);
                animator.ResetTrigger(biteAttackHash);
            }
            else
            {
                if (rand.Next(0, 100) > 50)
                {
                    animator.SetTrigger(biteAttackHash);
                    animator.ResetTrigger(soundwaveAttackHash);
                }
                else
                {
                    animator.SetTrigger(soundwaveAttackHash);
                    animator.ResetTrigger(biteAttackHash);
                }
            }
        }
        else
        {
            animator.ResetTrigger(biteAttackHash);
            animator.ResetTrigger(soundwaveAttackHash);
        }


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
        //    animator.SetTrigger(soundwaveAttackHash);
        //}
        //else
        //{
        //    animator.ResetTrigger(soundwaveAttackHash);
        //}
    }

    public void SoundwaveAttackStartedEvent()
    {
        Soundwave.SetActive(true);

        StartCoroutine(DelayedEndSoundwaveAttack());
    }

    IEnumerator DelayedEndSoundwaveAttack()
    {
        yield return new WaitForSeconds(1.5f);

        Soundwave.SetActive(false);
    }

    //public void SoundwaveAttackEndedEvent()
    //{
    //    Soundwave.SetActive(false);
    //}
}
