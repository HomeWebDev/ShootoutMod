using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    private Animator animator;
    private int takeDamageHash = Animator.StringToHash("Take Damage");
    private int dieHash = Animator.StringToHash("Die");

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage()
    {
        animator.SetTrigger(takeDamageHash);
    }

    public void Die()
    {
        animator.SetTrigger(dieHash);
    }
}
