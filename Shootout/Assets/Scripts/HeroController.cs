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

    private void GetWeaponType()
    {
        //GameObject rightHand = GameObject.Find("Dummy Prop Right");

        GameObject rightHand = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).gameObject;
        Transform leftHand = gameObject.transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).transform;

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
            //animator.ResetTrigger(relaxHash);
        }
        else if (moveH > 0)
        {
            animator.SetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
            //animator.ResetTrigger(relaxHash);
        }
        else if (moveV != 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
            //animator.ResetTrigger(relaxHash);
        }
        else
        {
            //animator.SetTrigger(relaxHash);
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
            //animator.ResetTrigger(relaxHash);
        }
        else if (moveH < 0)
        {
            animator.SetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
            //animator.ResetTrigger(relaxHash);
        }
        else if (moveV != 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
            //animator.ResetTrigger(relaxHash);
        }
        else
        {
            //animator.SetTrigger(relaxHash);
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
            //animator.ResetTrigger(relaxHash);
        }
        else if (moveV < 0)
        {
            animator.SetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
            //animator.ResetTrigger(relaxHash);
        }
        else if (moveH != 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
            //animator.ResetTrigger(relaxHash);
        }
        else
        {
            //animator.SetTrigger(relaxHash);
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
            //animator.ResetTrigger(relaxHash);
        }
        else if (moveV > 0)
        {
            animator.SetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
            //animator.ResetTrigger(relaxHash);
        }
        else if (moveH != 0)
        {
            animator.SetTrigger(walkHash);
            animator.ResetTrigger(walkBackwardHash);
            //animator.ResetTrigger(relaxHash);
        }
        else
        {
            //animator.SetTrigger(relaxHash);
            animator.ResetTrigger(walkBackwardHash);
            animator.ResetTrigger(walkHash);
        }
    }
}
