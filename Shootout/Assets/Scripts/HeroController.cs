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

    public bool meleeAttackActive;

    public float movementSpeed;
    public float attackSpeed;
    public float rotationDamping = 20f;
    private float longbowAnimationDelay = 0.50f;
    public float longbowForce = 30;
    private float crossbowAnimationDelay = 0.32f;
    private float crossbowForce = 20;
    private float wandAnimationDelay = 0.45f;
    private float wandForce = 5;
    private float rangedAttackRate;
    private float rangedAnimationDelay;
    private float rangedForce;
    private string longbowArrowString = "Arrow 01 Black";
    private string crossbowArrowString = "Arrow 01 Purple";
    private string wandArrowString = "Arrow 02 Fire";
    private string arrowString;

    private int walkHash = Animator.StringToHash("Walk");
    private int walkBackwardHash = Animator.StringToHash("Walk Backward");
    private int twoHandedIdleHash = Animator.StringToHash("TH Sword Idle");
    private int spearIdleHash = Animator.StringToHash("Spear Idle");
    private int punchHash = Animator.StringToHash("Punch");
    private int melee1Hash = Animator.StringToHash("Melee1"); //Strike forward like dagger or light sword
    private int melee2Hash = Animator.StringToHash("Melee2"); //Strike in arch like axe or hammer
    private int melee3Hash = Animator.StringToHash("Melee3"); //Strike in low arc/towards right side like scythe
    private int thMelee1Hash = Animator.StringToHash("THMelee1"); //Twohanded strike in up to down arch (axe)
    private int thMelee2Hash = Animator.StringToHash("THMelee2"); //Twohanded strike in left to right arch (sword)
    private int spearMelee1Hash = Animator.StringToHash("SpearMelee1"); //Normal spear attack
    private int spearMelee2Hash = Animator.StringToHash("SpearMelee2"); //Long range spear attack
    private int longbowShootHash = Animator.StringToHash("LongbowShoot");
    private int crossbowShootHash = Animator.StringToHash("CrossbowShoot");
    private int wandShootHash = Animator.StringToHash("WandShoot");

    private WeaponType weaponType;
    private GameObject rightHand;
    private GameObject leftHand;

    private float moveH;
    private float moveV;
    private Vector3 movement;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent(typeof(CharacterController)) as CharacterController;
        animator = GetComponent<Animator>();

        weaponType = WeaponType.BareHands;
        GetWeaponType();


    }

    public enum WeaponType
    {
        BareHands,
        Axe,
        Claws,
        Club,
        Crossbow,
        Dagger,
        Hammer,
        HandAuras,
        Knuckles,
        Longbow,
        Mace,
        Scepter,
        Scythe,
        Spear,
        LongSpear,
        Sword,
        TwoHandedAxe,
        TwoHandedSword,
        Wand,
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
        animator.SetFloat("MoveSpeedMultiplier", movementSpeed * 0.25f);
        animator.SetFloat("AttackSpeedMultiplier", attackSpeed * 0.1f);
        //Debug.Log("movement: " + Mathf.Min(Mathf.Abs(moveV), Mathf.Abs(moveH)));
    }

    void FixedUpdate()
    {
        //Fix y position at 0
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        rightHand = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;
        leftHand = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;

        //Handle movements based on axises
        moveV = Input.GetAxis(verticalAxis);
        moveH = Input.GetAxis(horizontalAxis);
        //Debug.Log("h: " + moveH);
        movement = new Vector3(moveH, 0.0f, moveV);
        movement *= movementSpeed / 2;
        controller.Move(movement * Time.deltaTime);

        GetWeaponType();

        //Debug.Log("Weapontype: " + weaponType.ToString());

        if (Input.GetButton(leftAttackButton) | Input.GetButton(rightAttackButton) | Input.GetButton(upAttackButton) | Input.GetButton(downAttackButton))
        {
            if (weaponType == WeaponType.BareHands |
                weaponType == WeaponType.Claws |
                weaponType == WeaponType.HandAuras |
                weaponType == WeaponType.Knuckles)
            {
                animator.SetTrigger(punchHash);
            }
            if (weaponType == WeaponType.Sword |
                weaponType == WeaponType.Dagger)
            {
                animator.SetTrigger(melee1Hash);
            }
            if (weaponType == WeaponType.Axe |
                weaponType == WeaponType.Hammer |
                weaponType == WeaponType.Mace |
                weaponType == WeaponType.Scepter |
                weaponType == WeaponType.Club)
            {
                animator.SetTrigger(melee2Hash);
            }
            if (weaponType == WeaponType.Scythe)
            {
                animator.SetTrigger(melee3Hash);
            }
            if (weaponType == WeaponType.TwoHandedAxe)
            {
                animator.SetTrigger(thMelee1Hash);
            }
            if (weaponType == WeaponType.TwoHandedSword)
            {
                animator.SetTrigger(thMelee2Hash);
            }
            if (weaponType == WeaponType.Spear)
            {
                animator.SetTrigger(spearMelee1Hash);
            }
            if (weaponType == WeaponType.LongSpear)
            {
                animator.SetTrigger(spearMelee2Hash);
            }
            if (weaponType == WeaponType.Longbow)
            {
                animator.SetTrigger(longbowShootHash);
            }
            if (weaponType == WeaponType.Crossbow)
            {
                animator.SetTrigger(crossbowShootHash);
            }
            if (weaponType == WeaponType.Wand)
            {
                animator.SetTrigger(wandShootHash);
            }

            animator.ResetTrigger(twoHandedIdleHash);
            animator.ResetTrigger(spearIdleHash);
        }
        else
        {
            animator.ResetTrigger(melee1Hash);
            animator.ResetTrigger(melee2Hash);
            animator.ResetTrigger(melee3Hash);
            animator.ResetTrigger(thMelee1Hash);
            animator.ResetTrigger(thMelee2Hash);
            animator.ResetTrigger(spearMelee1Hash);
            animator.ResetTrigger(spearMelee2Hash);
            animator.ResetTrigger(longbowShootHash);
            animator.ResetTrigger(crossbowShootHash);
            animator.ResetTrigger(wandShootHash);
            animator.ResetTrigger(punchHash);

            if (weaponType == WeaponType.TwoHandedAxe | weaponType == WeaponType.TwoHandedSword)
            {
                animator.SetTrigger(twoHandedIdleHash);
                animator.ResetTrigger(spearIdleHash);
            }
            else if(weaponType == WeaponType.Spear | weaponType == WeaponType.LongSpear)
            {
                animator.SetTrigger(spearIdleHash);
                animator.ResetTrigger(twoHandedIdleHash);
            }
            else
            {
                animator.ResetTrigger(twoHandedIdleHash);
                animator.ResetTrigger(spearIdleHash);
            }
        }

        //Handle rotation, look in attack direction
        if (Input.GetButton(leftAttackButton))
        {
            if (weaponType == WeaponType.Longbow)
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
            if (weaponType == WeaponType.Longbow)
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
            if (weaponType == WeaponType.Longbow)
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
            if (weaponType == WeaponType.Longbow)
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

    public void Melee1StartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void Melee1EndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void Melee2StartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void Melee2EndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void Melee3StartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void Melee3EndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void ThMelee1StartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void ThMelee1EndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void ThMelee2StartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void ThMelee2EndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void Spear1StartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void Spear1EndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void Spear2StartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void LeftPunchStartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void LeftPunchEndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void RightPunchStartedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void RightPunchEndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void Spear2EndedEvent()
    {
        GameObject weapon = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        weapon.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void LongbowShootEvent()
    {
        rangedAnimationDelay = longbowAnimationDelay / (attackSpeed * 0.1f);
        rangedForce = longbowForce;
        arrowString = longbowArrowString;
        RangedAttack();
    }

    public void CrossbowShootEvent()
    {
        rangedAnimationDelay = crossbowAnimationDelay / (attackSpeed * 0.1f);
        rangedForce = crossbowForce;
        arrowString = crossbowArrowString;
        RangedAttack();
    }

    public void WandShootEvent()
    {
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
        rightHand = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;

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

        GameObject arrow = Instantiate(Resources.Load("Prefabs/Weapons/Arrows/" + arrowString, typeof(GameObject)), rightHand.transform.position, Quaternion.Euler(0, y, 0)) as GameObject;
        arrow.tag = "Arrow";
        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * rangedForce;
    }

    private void GetWeaponType()
    {
        //Longbow only possible left hand weapon
        if (leftHand.transform.childCount > 0)
        {
            string weaponName = leftHand.transform.GetChild(0).name;

            if (weaponName.Contains("Longbow"))
                weaponType = WeaponType.Longbow;
        }

        if (rightHand.transform.childCount > 0)
        {
            string weaponName = rightHand.transform.GetChild(0).name;

            //Debug.Log("weaponName: " + weaponName);

            if (weaponName.Contains("Axe"))
                weaponType = WeaponType.Axe;
            if (weaponName.Contains("Claw"))
                weaponType = WeaponType.Claws;
            if (weaponName.Contains("Club"))
                weaponType = WeaponType.Club;
            if (weaponName.Contains("Crossbow"))
                weaponType = WeaponType.Crossbow;
            if (weaponName.Contains("Dagger"))
                weaponType = WeaponType.Dagger;
            if (weaponName.Contains("Hammer"))
                weaponType = WeaponType.Hammer;
            if (weaponName.Contains("Claw"))
                weaponType = WeaponType.Claws;
            if (weaponName.Contains("Hand Aura"))
                weaponType = WeaponType.HandAuras;
            if (weaponName.Contains("Knuckles"))
                weaponType = WeaponType.Knuckles;
            if (weaponName.Contains("Mace"))
                weaponType = WeaponType.Mace;
            if (weaponName.Contains("Scepter"))
                weaponType = WeaponType.Scepter;
            if (weaponName.Contains("Scythe"))
                weaponType = WeaponType.Scythe;
            if (weaponName.Contains("Spear"))
                weaponType = WeaponType.Spear;
            if (weaponName.Contains("Spear 03")) //Alternate longer attack movement for spear type 3
                weaponType = WeaponType.LongSpear;
            if (weaponName.Contains("Sword"))
                weaponType = WeaponType.Sword;
            if (weaponName.Contains("TH Axe"))
                weaponType = WeaponType.TwoHandedAxe;
            if (weaponName.Contains("TH Sword"))
                weaponType = WeaponType.TwoHandedSword;
            if (weaponName.Contains("Wand"))
                weaponType = WeaponType.Wand;
        }
    }

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
