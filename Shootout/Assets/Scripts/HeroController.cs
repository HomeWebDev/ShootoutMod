using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    Rigidbody rb;
    CharacterController controller;
    Animator animator;
    public float MusicVolume = 0.15f;
    public float FXVolume = 0.15f;
    public float UnitVolume = 0.15f;
    public string horizontalAxis;
    public string verticalAxis;
    public string leftAttackButton;
    public string rightAttackButton;
    public string upAttackButton;
    public string downAttackButton;
    public string shiftMagicButton;
    private bool magicEnabled;
    private float nextMagicShift;
    private float magicShiftRate = 0.25f;
    public bool AuraUpdated;
    private float nextMagic;
    private float magicDelay = 0.50f;
    private bool nextThrownIsRightHand = false;


    public bool meleeAttackActive;

    public float movementSpeed;
    public float attackSpeed;
    public float rotationDamping = 20f;
    private float longbowAnimationDelay = 0.50f;
    public float longbowForce = 30;
    private float crossbowAnimationDelay = 0.32f;
    private float crossbowForce = 20;
    private float wandAnimationDelay = 0.45f;
    private float melee1AnimationDelay = 0.00f;
    public float ThrowForce = 20.0f;
    private float wandForce = 5;
    private float rangedAttackRate;
    private float rangedAnimationDelay;
    private float rangedForce;
    private string longbowArrowString = "Arrow 01 Black";
    private string crossbowArrowString = "Arrow 01 Purple";
    private string wandArrowString = "Arrow 02 Fire";
    private string arrowString;

    public int currentWeaponHash = 0;
    private int walkHash = Animator.StringToHash("Walk");
    private int walkBackwardHash = Animator.StringToHash("Walk Backward");
    //private int twoHandedIdleHash = Animator.StringToHash("TH Sword Idle");
    //private int spearIdleHash = Animator.StringToHash("Spear Idle");
    //private int punchHash = Animator.StringToHash("Punch");
    //private int melee1Hash = Animator.StringToHash("Melee1"); //Strike forward like dagger or light sword
    //private int melee2Hash = Animator.StringToHash("Melee2"); //Strike in arch like axe or hammer
    //private int melee3Hash = Animator.StringToHash("Melee3"); //Strike in low arc/towards right side like scythe
    //private int thMelee1Hash = Animator.StringToHash("THMelee1"); //Twohanded strike in up to down arch (axe)
    //private int thMelee2Hash = Animator.StringToHash("THMelee2"); //Twohanded strike in left to right arch (sword)
    //private int spearMelee1Hash = Animator.StringToHash("SpearMelee1"); //Normal spear attack
    //private int spearMelee2Hash = Animator.StringToHash("SpearMelee2"); //Long range spear attack
    //private int longbowShootHash = Animator.StringToHash("LongbowShoot");
    //private int crossbowShootHash = Animator.StringToHash("CrossbowShoot");
    //private int wandShootHash = Animator.StringToHash("WandShoot");
    //private int melee2ThrowHash = Animator.StringToHash("Melee2Throw");

    //private WeaponType weaponType;
    //private GameObject rightHand;
    //private GameObject leftHand;

    private float moveH;
    private float moveV;
    private Vector3 movement;
    private Equipment ActiveEq;

    private PlayerStamina playerStamina;
    private PlayerMagic playerMagic;
    private GameObject player1;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent(typeof(CharacterController)) as CharacterController;
        animator = GetComponent<Animator>();
        ActiveEq = GetComponent<Equipment>();

        playerStamina = GetComponent<PlayerStamina>();
        playerMagic = GetComponent<PlayerMagic>();
        player1 = GameObject.FindGameObjectWithTag("Player1");
    }
    void Awake()
    {
        //GetWeaponType();
    }

    public enum WeaponType
    {
        Melee,
        RangeShort,
        RangeLong,
        //BareHands,
        //Axe,
        //Claws,
        //Club,
        //Crossbow,
        //Dagger,
        //Hammer,
        //HandAuras,
        //Knuckles,
        //Longbow,
        //Mace,
        //Scepter,
        //Scythe,
        //Spear,
        //LongSpear,
        //Sword,
        //TwoHandedAxe,
        //TwoHandedSword,
        //Wand,
    };

    public enum WeponAttackType
    {
        Punch,
        PunchThrow,
        Melee1,
        Melee1Throw,
        Melee2,
        Melee2Throw,
        Melee3,
        THMelee1,
        THMelee2,
        SpearMelee1,
        SpearMelee2,
        LongbowShoot,
        CrossbowShoot,
        WandShoot,
        TwoHanded_Idle,
        Spear_Idle

    };

    // Update is called once per frame
    void Update()
    {
     }

    private void LateUpdate()
    {
        moveV = Input.GetAxis(verticalAxis);
        moveH = Input.GetAxis(horizontalAxis);
        //moveV *= Time.deltaTime;
        //moveH *= Time.deltaTime;
        animator.SetFloat("MoveSpeedMultiplier", movementSpeed * 0.125f);
        //float headAttackSpeedMod = 0;
        //for (int i = 0; i < GetComponent<PlayerHeadStats>().NumberOfGroups; i++)
        //{
        //    Debug.Log("i: " + i);
        //    PlayerHeadStats t = player1.GetComponent<PlayerHeadStats>();
        //    HeadStats h = player1.GetComponent<PlayerHeadStats>().HeadStats[i];
        //    float tr = h.AttackSpeedMod; //This row gets the error
        //    headAttackSpeedMod = player1.GetComponent<PlayerHeadStats>().HeadStats[i].AttackSpeedMod;
        //}
        float attackSpeedMultiplier = 0.1f;
        float itemAttackSpeedMod = GetComponent<CostumeStats>().AttackSpeedMod * attackSpeedMultiplier + GetComponent<BackStats>().AttackSpeedMod * attackSpeedMultiplier + GetComponent<HeadStats>().AttackSpeedMod * attackSpeedMultiplier;
        animator.SetFloat("AttackSpeedMultiplier", attackSpeed * attackSpeedMultiplier + itemAttackSpeedMod);
        //Debug.Log("movement: " + Mathf.Min(Mathf.Abs(moveV), Mathf.Abs(moveH)));
    }

    private void HandleMagic()
    {
        if (Input.GetButton(shiftMagicButton) && Time.time > nextMagicShift)
        {
            nextMagicShift = Time.time + magicShiftRate;
            magicEnabled = !magicEnabled;
        }

        GameObject aura = GameObject.FindWithTag("Aura");

        if (magicEnabled && !AuraUpdated)
        {
            if (player1.gameObject.GetComponent<MagicStats>().Aura != null)
            {
                //Destroy old auras
                GameObject[] oldAuras = GameObject.FindGameObjectsWithTag("Aura");
                for (var i = 0; i < oldAuras.Length; i++)
                {
                    Destroy(oldAuras[i]);
                }

                GameObject newAura = Instantiate(player1.gameObject.GetComponent<MagicStats>().Aura, player1.transform);
                newAura.tag = "Aura";
                AuraUpdated = true;
                //Debug.Log("Aura: " + newAura);
            }
        }

        if (!magicEnabled)
        {
            AuraUpdated = false;
            Destroy(aura);
        }
    }

    void FixedUpdate()
    {
        //Fix y position at 0
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        //Handle movements based on axises
        moveV = Input.GetAxis(verticalAxis);
        moveH = Input.GetAxis(horizontalAxis);

        //Normalize vector if length is more than 1 to get speed cap
        if(Math.Sqrt(moveV * moveV + moveH * moveH) > 1)
        {
            movement = new Vector3(moveH, 0.0f, moveV).normalized;
        }
        else
        {
            movement = new Vector3(moveH, 0.0f, moveV);
        }

        //float headSpeedMod = 0;
        //for (int i = 0; i < GetComponent<PlayerHeadStats>().NumberOfGroups; i++)
        //{
        //    headSpeedMod = GetComponent<PlayerHeadStats>().HeadStats[i].SpeedMod;
        //}
        float itemSpeedMod = GetComponent<CostumeStats>().SpeedMod / 2 + GetComponent<BackStats>().SpeedMod / 2 + GetComponent<HeadStats>().SpeedMod / 2;
        movement *= (movementSpeed / 2 + itemSpeedMod);
        controller.Move(movement * Time.deltaTime);

        // GetWeaponType();

        //Debug.Log("Weapontype: " + weaponType.ToString());

        HandleMagic();

        //CHANGE!!!!
        if (ActiveEq.weapon != null)
        {
            if (currentWeaponHash != 0)
                animator.ResetTrigger(currentWeaponHash);
            if (Input.GetButton(leftAttackButton) | Input.GetButton(rightAttackButton) | Input.GetButton(upAttackButton) | Input.GetButton(downAttackButton))
            {
                animator.SetTrigger(ActiveEq.weapon.GetComponent<WeaponConfig>().AttackAnimationHash);
                currentWeaponHash = ActiveEq.weapon.GetComponent<WeaponConfig>().AttackAnimationHash;
            }
            //else
            //{
            //    //animator.ResetTrigger(currentWeaponHash);
            //    //animator.ResetTrigger(melee2Hash);
            //    //animator.ResetTrigger(melee3Hash);
            //    //animator.ResetTrigger(thMelee1Hash);
            //    //animator.ResetTrigger(thMelee2Hash);
            //    //animator.ResetTrigger(spearMelee1Hash);
            //    //animator.ResetTrigger(spearMelee2Hash);
            //    //animator.ResetTrigger(longbowShootHash);
            //    //animator.ResetTrigger(crossbowShootHash);
            //    //animator.ResetTrigger(wandShootHash);
            //    //animator.ResetTrigger(punchHash);
            //    //animator.ResetTrigger(melee2ThrowHash);
            //    //animator.ResetTrigger(twoHandedIdleHash);
            //    //animator.ResetTrigger(spearIdleHash);
            //}
            #region old way of handleing animation triggers.

            //if (weaponType == WeaponType.BareHands |
            //    weaponType == WeaponType.Claws |
            //    weaponType == WeaponType.HandAuras |
            //    weaponType == WeaponType.Knuckles)
            //{
            //    animator.SetTrigger(Animator.StringToHash(ActiveEq.weapon.GetComponent<WeaponConfig>().AttackAnimation.ToString()));
            //}
            //if (weaponType == WeaponType.Sword |
            //    weaponType == WeaponType.Dagger)
            //{
            //    animator.SetTrigger(melee1Hash);
            //}
            //if (//weaponType == WeaponType.Axe |
            //    weaponType == WeaponType.Hammer |
            //    weaponType == WeaponType.Mace |
            //    weaponType == WeaponType.Scepter |
            //      weaponType == WeaponType.Club)
            //{
            //    animator.SetTrigger(melee2Hash);
            //}
            //if (weaponType == WeaponType.Axe)
            //{
            //    animator.SetTrigger(melee2ThrowHash);
            //}
            //if (weaponType == WeaponType.Scythe)
            //{
            //    animator.SetTrigger(melee3Hash);
            //}
            //if (weaponType == WeaponType.TwoHandedAxe)
            //{
            //    animator.SetTrigger(thMelee1Hash);
            //}
            //if (weaponType == WeaponType.TwoHandedSword)
            //{
            //    animator.SetTrigger(thMelee2Hash);
            //}
            //if (weaponType == WeaponType.Spear)
            //{
            //    animator.SetTrigger(spearMelee1Hash);
            //}
            //if (weaponType == WeaponType.LongSpear)
            //{
            //    animator.SetTrigger(spearMelee2Hash);
            //}
            //if (weaponType == WeaponType.Longbow)
            //{
            //    animator.SetTrigger(longbowShootHash);
            //}
            //if (weaponType == WeaponType.Crossbow)
            //{
            //    animator.SetTrigger(crossbowShootHash);
            //}
            //if (weaponType == WeaponType.Wand)
            //{
            //    animator.SetTrigger(wandShootHash);
            //}

            //animator.ResetTrigger(twoHandedIdleHash);
            //animator.ResetTrigger(spearIdleHash);
            //}
            //else
            //{
            //    animator.ResetTrigger(melee1Hash);
            //    animator.ResetTrigger(melee2Hash);
            //    animator.ResetTrigger(melee3Hash);
            //    animator.ResetTrigger(thMelee1Hash);
            //    animator.ResetTrigger(thMelee2Hash);
            //    animator.ResetTrigger(spearMelee1Hash);
            //    animator.ResetTrigger(spearMelee2Hash);
            //    animator.ResetTrigger(longbowShootHash);
            //    animator.ResetTrigger(crossbowShootHash);
            //    animator.ResetTrigger(wandShootHash);
            //    animator.ResetTrigger(punchHash);
            //    animator.ResetTrigger(melee2ThrowHash);

            //    if (weaponType == WeaponType.TwoHandedAxe | weaponType == WeaponType.TwoHandedSword)
            //    {
            //        animator.SetTrigger(twoHandedIdleHash);
            //        animator.ResetTrigger(spearIdleHash);
            //    }
            //    else if(weaponType == WeaponType.Spear | weaponType == WeaponType.LongSpear)
            //    {
            //        animator.SetTrigger(spearIdleHash);
            //        animator.ResetTrigger(twoHandedIdleHash);
            //    }
            //    else
            //    {
            //        animator.ResetTrigger(twoHandedIdleHash);
            //        animator.ResetTrigger(spearIdleHash);
            //    }
            //}

            //Handle rotation, look in attack direction
            #endregion

            if (Input.GetButton(leftAttackButton))
            {
                HandleMagicShot();

                //weaponType == WeaponType.Longbow
                if (ActiveEq.weapon.GetComponent<WeaponConfig>().WeaponType.Equals(WeaponType.RangeLong))
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(new Vector3(-1, 0, 1)),
                        Time.deltaTime * rotationDamping);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(new Vector3(-1, 0, 0)),
                        Time.deltaTime * rotationDamping);
                }
                SetLeftAttackingMovement(moveH, moveV);
            }
            else if (Input.GetButton(rightAttackButton))
            {
                HandleMagicShot();

                if (ActiveEq.weapon.GetComponent<WeaponConfig>().WeaponType.Equals(WeaponType.RangeLong))
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(new Vector3(1, 0, -1)),
                        Time.deltaTime * rotationDamping);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(new Vector3(1, 0, 0)),
                        Time.deltaTime * rotationDamping);
                }
                SetRightAttackingMovement(moveH, moveV);
            }
            else if (Input.GetButton(upAttackButton))
            {
                HandleMagicShot();

                if (ActiveEq.weapon.GetComponent<WeaponConfig>().WeaponType.Equals(WeaponType.RangeLong))
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(new Vector3(1, 0, 1)),
                        Time.deltaTime * rotationDamping);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(new Vector3(0, 0, 1)),
                        Time.deltaTime * rotationDamping);
                }
                SetUpAttackingMovement(moveH, moveV);
            }
            else if (Input.GetButton(downAttackButton))
            {
                HandleMagicShot();

                if (ActiveEq.weapon.GetComponent<WeaponConfig>().WeaponType.Equals(WeaponType.RangeLong))
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(new Vector3(-1, 0, -1)),
                        Time.deltaTime * rotationDamping);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(new Vector3(0, 0, -1)),
                        Time.deltaTime * rotationDamping);
                }
                SetDownAttackingMovement(moveH, moveV);
            }
            //If not attacking look forwards
            else
            {
                if (movement != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(movement),
                        Time.deltaTime * rotationDamping);
                    animator.SetTrigger(walkHash);
                    animator.ResetTrigger(walkBackwardHash);
                }
                else
                {
                    animator.ResetTrigger(walkBackwardHash);
                    animator.ResetTrigger(walkHash);
                }
            }
        }
    }

    private void HandleMagicShot()
    {
        if (magicEnabled && Time.time > nextMagic)
        {
            if (playerMagic.DepleteMagic(10))
            {
                nextMagic = Time.time + magicDelay;
                MagicAttack();
            }
        }
    }

    public void Melee1StartedEvent()
    {
        if(ActiveEq.weapon == null)
        {
            return;
        }
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;
        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void Melee1EndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;
        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void Melee2StartedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;
        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;

        ////Test: Throwing all one-handed axes
        //if (weaponType == WeaponType.Axe)
        //{
        //    rangedAnimationDelay = melee1AnimationDelay / (attackSpeed * 0.1f);
        //    rangedForce = melee1ThrowForce;
        //    ThrowWeapon();
        //}
    }

    public void LeftPunchThrowEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        rangedAnimationDelay = melee1AnimationDelay / (attackSpeed * 0.1f);
        rangedForce = ThrowForce;

        if (playerStamina.DepleteStamina(10))
        {
            ThrowWeapon();
        }
    }

    public void RightPunchThrowEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        rangedAnimationDelay = melee1AnimationDelay / (attackSpeed * 0.1f);
        rangedForce = ThrowForce;

        if (playerStamina.DepleteStamina(10))
        {
            nextThrownIsRightHand = true;
            ThrowWeapon();
        }
    }

    public void Melee1ThrowEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        rangedAnimationDelay = melee1AnimationDelay / (attackSpeed * 0.1f);
        rangedForce = ThrowForce;

        if (playerStamina.DepleteStamina(10))
        {
            ThrowWeapon();
        }
    }

    public void Melee2ThrowEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        rangedAnimationDelay = melee1AnimationDelay / (attackSpeed * 0.1f);
        rangedForce = ThrowForce;

        if (playerStamina.DepleteStamina(10))
        {
            ThrowWeapon();
        }
    }

    public void Melee2EndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void Melee3StartedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;

    }

    public void Melee3EndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;

    }

    public void ThMelee1StartedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;

    }

    public void ThMelee1EndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;

    }

    public void ThMelee2StartedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;

    }

    public void ThMelee2EndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;

    }

    public void Spear1StartedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;

    }

    public void Spear1EndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;

    }

    public void Spear2StartedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;

    }

    public void Spear2EndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;

    }
    public void LeftPunchStartedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;

    }

    public void LeftPunchEndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;

    }

    public void RightPunchStartedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = true;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = true;

    }

    public void RightPunchEndedEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //weapon.GetComponent<CapsuleCollider>().enabled = false;
        ActiveEq.weapon.GetComponent<CapsuleCollider>().enabled = false;

    }



    public void LongbowShootEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        rangedAnimationDelay = longbowAnimationDelay / (attackSpeed * 0.1f);
        rangedForce = longbowForce;
        arrowString = longbowArrowString;
        RangedAttack();
    }

    public void CrossbowShootEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        rangedAnimationDelay = crossbowAnimationDelay / (attackSpeed * 0.1f);
        rangedForce = crossbowForce;
        arrowString = crossbowArrowString;
        RangedAttack();
    }

    public void WandShootEvent()
    {
        if (ActiveEq.weapon == null)
        {
            return;
        }

        rangedAnimationDelay = wandAnimationDelay / (attackSpeed * 0.1f);
        rangedForce = wandForce;
        arrowString = wandArrowString;
        RangedAttack();
    }

    private void RangedAttack()
    {

        if (Input.GetButton(leftAttackButton))
        {
            StartCoroutine(SpawnArrow(270 + movement.z));
        }
        if (Input.GetButton(rightAttackButton))
        {
            StartCoroutine(SpawnArrow(90 - movement.z));
        }
        if (Input.GetButton(upAttackButton))
        {
            StartCoroutine(SpawnArrow(0 + movement.x));
        }
        if (Input.GetButton(downAttackButton))
        {
            StartCoroutine(SpawnArrow(180 - movement.x));
        }
    }

    IEnumerator SpawnArrow(float y)
    {
        yield return new WaitForSeconds(rangedAnimationDelay);
        //rightHand = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;

        if (Input.GetButton(leftAttackButton))
        {
            y = 270 + movement.z;
        }
        if (Input.GetButton(rightAttackButton))
        {
            y = 90 - movement.z;
        }
        if (Input.GetButton(upAttackButton))
        {
            y = 0 + movement.x;
        }
        if (Input.GetButton(downAttackButton))
        {
            y = 180 - movement.x;
        }

        GameObject arrow = Instantiate(Resources.Load("Prefabs/Weapons/Arrows/" + arrowString, typeof(GameObject)), ActiveEq.RightHand.transform.position, Quaternion.Euler(0, y, 0)) as GameObject;
        arrow.tag = "Arrow";
        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * rangedForce;
    }

    private void MagicAttack()
    {
        if (Input.GetButton(leftAttackButton))
        {
            StartCoroutine(SpawnMagicProjectile(270 + movement.z));
        }
        if (Input.GetButton(rightAttackButton))
        {
            StartCoroutine(SpawnMagicProjectile(90 - movement.z));
        }
        if (Input.GetButton(upAttackButton))
        {
            StartCoroutine(SpawnMagicProjectile(0 + movement.x));
        }
        if (Input.GetButton(downAttackButton))
        {
            StartCoroutine(SpawnMagicProjectile(180 - movement.x));
        }
    }

    IEnumerator SpawnMagicProjectile(float y)
    {
        yield return new WaitForSeconds(rangedAnimationDelay);
        //rightHand = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;

        if (Input.GetButton(leftAttackButton))
        {
            y = 270 + movement.z;
        }
        if (Input.GetButton(rightAttackButton))
        {
            y = 90 - movement.z;
        }
        if (Input.GetButton(upAttackButton))
        {
            y = 0 + movement.x;
        }
        if (Input.GetButton(downAttackButton))
        {
            y = 180 - movement.x;
        }


        try
        {
            //GetComponent<MagicProjectile>().Projectile = GetComponent<MagicStats>().Projectile;
            GameObject projectile = Instantiate(GetComponent<MagicStats>().Projectile, ActiveEq.RightHand.transform.position, Quaternion.Euler(0, y, 0)) as GameObject;
            projectile.tag = "Arrow";
            //projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * rangedForce;
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * (GetComponent<MagicStats>().Speed));
            //projectile.GetComponent<MagicProjectileScript>().impactNormal = hit.normal;
            //arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * rangedForce;
        }
        catch(Exception ex) { }
    }

    private void ThrowWeapon()
    {
        int doubleAngle = 20;
        int tripleAngle = 20;
        int quadAngle = 90;

        float angle = 0;
        int buttonPressed = 0;
        if (Input.GetButton(leftAttackButton))
        {
            angle = 270 + movement.z;
            buttonPressed = 1;
        }
        if (Input.GetButton(rightAttackButton))
        {
            angle = 90 - movement.z;
            buttonPressed = 2;
        }
        if (Input.GetButton(upAttackButton))
        {
            angle = 0 + movement.x;
            buttonPressed = 3;
        }
        if (Input.GetButton(downAttackButton))
        {
            angle = 180 - movement.x;
            buttonPressed = 4;
        }

        if (buttonPressed != 0)
        {
            if (GetComponent<PowerupStats>().DoubleShot && !GetComponent<PowerupStats>().TripleShot)
            {
                StartCoroutine(SpawnThrowWeapon(angle - doubleAngle, buttonPressed));
                StartCoroutine(SpawnThrowWeapon(angle + doubleAngle, buttonPressed));
            }
            else
            {
                StartCoroutine(SpawnThrowWeapon(angle, buttonPressed));
            }
            if (GetComponent<PowerupStats>().TripleShot)
            {
                StartCoroutine(SpawnThrowWeapon(angle - tripleAngle, buttonPressed));
                StartCoroutine(SpawnThrowWeapon(angle + tripleAngle, buttonPressed));
            }
            if (GetComponent<PowerupStats>().QuadShot)
            {
                StartCoroutine(SpawnThrowWeapon(angle + quadAngle, buttonPressed));
                StartCoroutine(SpawnThrowWeapon(angle + 2 * quadAngle, buttonPressed));
                StartCoroutine(SpawnThrowWeapon(angle + 3 * quadAngle, buttonPressed));
            }
        }
    }

    IEnumerator SpawnThrowWeapon(float y, int buttonPressed)
    {
        yield return new WaitForSeconds(rangedAnimationDelay);
        //rightHand = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;
        //GameObject rightShoulder = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject;
        //GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;

        //MagicAttack();

        if (buttonPressed == 1)
        {
            if (Input.GetButton(rightAttackButton))
            {
                y += 180;
            }
            if (Input.GetButton(upAttackButton))
            {
                y += 90;
            }
            if (Input.GetButton(downAttackButton))
            {
                y -= 90;
            }
        }
        if (buttonPressed == 2)
        {
            if (Input.GetButton(leftAttackButton))
            {
                y += 180;
            }
            if (Input.GetButton(upAttackButton))
            {
                y -= 90;
            }
            if (Input.GetButton(downAttackButton))
            {
                y += 90;
            }
        }
        if (buttonPressed == 3)
        {
            if (Input.GetButton(leftAttackButton))
            {
                y -= 90;
            }
            if (Input.GetButton(rightAttackButton))
            {
                y += 90;
            }
            if (Input.GetButton(downAttackButton))
            {
                y += 180;
            }
        }
        if (buttonPressed == 4)
        {
            if (Input.GetButton(leftAttackButton))
            {
                y += 90;
            }
            if (Input.GetButton(rightAttackButton))
            {
                y -= 90;
            }
            if (Input.GetButton(upAttackButton))
            {
                y += 180;
            }
        }

        GameObject throwWeapon;
        //Handle dual-handed weapons
        if (nextThrownIsRightHand)
        {
            nextThrownIsRightHand = false;
            throwWeapon = Instantiate(ActiveEq.weapon, ActiveEq.RightHand.transform.position, Quaternion.Euler(-00, y + 90, -90));
        }
        else
        {
            throwWeapon = Instantiate(ActiveEq.weapon, ActiveEq.weapon.transform.position, Quaternion.Euler(-00, y + 90, -90));
        }
        //GameObject throwWeapon = Instantiate(ActiveEq.weapon, ActiveEq.RightHand.transform.position, Quaternion.Euler(-00, y + 90, -90));
        throwWeapon = ResizeThrownItem(throwWeapon);

        throwWeapon.GetComponent<CapsuleCollider>().enabled = true;
        throwWeapon.AddComponent<Rigidbody>();

        if(GetComponent<PowerupStats>().CircularShot)
        {
            throwWeapon.AddComponent<ThrowRotator>();
            throwWeapon.GetComponent<ThrowRotator>().ThrowForce = ThrowForce;
            throwWeapon.GetComponent<ThrowRotator>().Behaviour = 0;
        }
        if (GetComponent<PowerupStats>().SpinningShot)
        {
            throwWeapon.AddComponent<ThrowRotator>();
            throwWeapon.GetComponent<ThrowRotator>().ThrowForce = ThrowForce;
            throwWeapon.GetComponent<ThrowRotator>().Behaviour = 1;
        }

        throwWeapon.tag = "Arrow";

        throwWeapon.GetComponent<Rigidbody>().AddRelativeForce(-150, rangedForce*-20, rangedForce* 0);
        throwWeapon.GetComponent<Rigidbody>().AddRelativeTorque(0, 1000, 0);
    }

    private GameObject ResizeThrownItem(GameObject item)
    {
        Vector3 adjustedScale = new Vector3(item.transform.localScale.x + GetComponent<PowerupStats>().SizeIncrease, item.transform.localScale.y + GetComponent<PowerupStats>().SizeIncrease, item.transform.localScale.z + GetComponent<PowerupStats>().SizeIncrease);
        item.transform.localScale = adjustedScale;

        return item;
    }


    //NOT USED ANYMORE
    //private void GetWeaponType()
    //{
    //    if(leftHand == null)
    //    {
    //        return;
    //    }
    //    //Longbow only possible left hand weapon
    //    if (leftHand.transform.childCount > 0)
    //    {
    //        string weaponName = leftHand.transform.GetChild(0).name;

    //        if (weaponName.Contains("Longbow"))
    //            weaponType = WeaponType.Longbow;
    //    }

    //    if (rightHand.transform.childCount > 0)
    //    {
    //        string weaponName = rightHand.transform.GetChild(0).name;

    //        //Debug.Log("weaponName: " + weaponName);

    //        if (weaponName.Contains("Axe"))
    //            weaponType = WeaponType.Axe;
    //        if (weaponName.Contains("Claw"))
    //            weaponType = WeaponType.Claws;
    //        if (weaponName.Contains("Club"))
    //            weaponType = WeaponType.Club;
    //        if (weaponName.Contains("Crossbow"))
    //            weaponType = WeaponType.Crossbow;
    //        if (weaponName.Contains("Dagger"))
    //            weaponType = WeaponType.Dagger;
    //        if (weaponName.Contains("Hammer"))
    //            weaponType = WeaponType.Hammer;
    //        if (weaponName.Contains("Claw"))
    //            weaponType = WeaponType.Claws;
    //        if (weaponName.Contains("Hand Aura"))
    //            weaponType = WeaponType.HandAuras;
    //        if (weaponName.Contains("Knuckles"))
    //            weaponType = WeaponType.Knuckles;
    //        if (weaponName.Contains("Mace"))
    //            weaponType = WeaponType.Mace;
    //        if (weaponName.Contains("Scepter"))
    //            weaponType = WeaponType.Scepter;
    //        if (weaponName.Contains("Scythe"))
    //            weaponType = WeaponType.Scythe;
    //        if (weaponName.Contains("Spear"))
    //            weaponType = WeaponType.Spear;
    //        if (weaponName.Contains("Spear 03")) //Alternate longer attack movement for spear type 3
    //            weaponType = WeaponType.LongSpear;
    //        if (weaponName.Contains("Sword"))
    //            weaponType = WeaponType.Sword;
    //        if (weaponName.Contains("TH Axe"))
    //            weaponType = WeaponType.TwoHandedAxe;
    //        if (weaponName.Contains("TH Sword"))
    //            weaponType = WeaponType.TwoHandedSword;
    //        if (weaponName.Contains("Wand"))
    //            weaponType = WeaponType.Wand;
    //    }
    //}

private void SetLeftAttackingMovement(float moveH, float moveV)
    {
        if (moveH < 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
        }
        else if (moveH > 0)
        {
            animator.SetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
        else if (moveV != 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
        }
        else
        {
            animator.ResetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
    }

    private void SetRightAttackingMovement(float moveH, float moveV)
    {
        if (moveH > 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
        }
        else if (moveH < 0)
        {
            animator.SetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
        else if (moveV != 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
        }
        else
        {
            animator.ResetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
    }

    private void SetUpAttackingMovement(float moveH, float moveV)
    {
        if (moveV > 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
        }
        else if (moveV < 0)
        {
            animator.SetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
        else if (moveH != 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
        }
        else
        {
            animator.ResetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
    }

    private void SetDownAttackingMovement(float moveH, float moveV)
    {
        if (moveV < 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
        }
        else if (moveV > 0)
        {
            animator.SetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
        else if (moveH != 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
        }
        else
        {
            animator.ResetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
    }
}
