using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    private Animator animator;
    private int takeDamageHash = Animator.StringToHash("Take Damage");
    private int dieHash = Animator.StringToHash("Die");
    private GameObject player1;
    private Vector3 knockbackImpact = Vector3.zero;
    CharacterController characterController;
    private int knockbackForce = 10;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        player1 = GameObject.FindGameObjectWithTag("Player1");

        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {

        //Handle knockback
        if (knockbackImpact.magnitude > 0.2)
        {
            characterController.Move(knockbackImpact * Time.deltaTime);
        }
        knockbackImpact = Vector3.Lerp(knockbackImpact, Vector3.zero, 5 * Time.deltaTime);
    }

    public void TakeDamage()
    {
        Knockback();

        animator.SetTrigger(takeDamageHash);
    }

    private void Knockback()
    {
        Vector3 dir = (transform.position - player1.transform.position).normalized;
        dir.y = 0;
        knockbackImpact = dir * knockbackForce;
    }

    public void Die()
    {
        animator.SetTrigger(dieHash);
    }
}
