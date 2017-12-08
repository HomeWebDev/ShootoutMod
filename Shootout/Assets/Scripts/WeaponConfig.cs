using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfig : MonoBehaviour {

    // Use this for initialization
    public bool DoubleHanded;
    public bool LeftHandCopy;
    public bool RightHandCopy;
    public Vector3 WeaponRotationLeftHand;
    public Vector3 WeaponRotationRightHand;
    public HeroController.WeaponType WeaponType;
    public HeroController.WeponAttackType AttackAnimation;
    public int AttackAnimationHash = 0;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimationHash = Animator.StringToHash(AttackAnimation.ToString().Replace('_',' '));
    }
}
