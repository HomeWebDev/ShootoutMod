using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used to handle player health
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float shield = 0;

    public Slider healthSlider;
    public Slider shieldSlider;

    public float maxHealth;
    public UltimateStatusBar healthStatusBar;

    private Animator animator;
    private int takeDamageHash = Animator.StringToHash("Take Damage");
    private int dieHash = Animator.StringToHash("Die");
    private GameObject player1;

    // Use this for initialization
    void Start()
    {
        //shieldSlider.value = shield;

        maxHealth = health;

        healthStatusBar.screenSpaceOptions.xRatio = maxHealth / 1000;
        healthStatusBar.UpdatePositioning();

        healthStatusBar.UpdateStatus(health, maxHealth);

        animator = GetComponent<Animator>();

        player1 = GameObject.FindGameObjectWithTag("Player1");
    }

    public void UpdateStatusBar()
    {
        healthStatusBar.screenSpaceOptions.xRatio = maxHealth / 1000;
        healthStatusBar.UpdatePositioning();

        healthStatusBar.UpdateStatus(health, maxHealth);
    }

    /// <summary>
    /// Reset health
    /// </summary>
    public void ResetHealth()
    {
        //Reset health
        health = 100;

        healthSlider.value = health;
    }

    /// <summary>
    /// Add health by amount
    /// </summary>
    /// <param name="amount"></param>
    public void AddHealth(float amount)
    {
        float tmp = health + amount;
        if (health < 100)
        {
            health = Mathf.Min(tmp, 100);
            healthSlider.value = health;
        }
    }

    /// <summary>
    /// Add shield by amount
    /// </summary>
    /// <param name="amount"></param>
    public void AddShield(float amount)
    {
        float tmp = shield + amount;
        if (shield < 50)
        {
            shield = Mathf.Min(tmp, 50);
            shieldSlider.value = shield;
        }
    }

    public void TakeDamage(float amount)
    {
        //// Decrement the player's health or shield by amount.
        //if (shield > 0)
        //    shield -= amount;
        //else
        //    health -= amount;
        float headAmout = 0;
        for (int i = 0; i < player1.GetComponent<PlayerHeadStats>().NumberOfGroups; i++)
        {
            headAmout = player1.GetComponent<PlayerHeadStats>().HeadStats[i].DefenceMod;
        }
        float itemDefenceMod = amount * player1.GetComponent<CostumeStats>().DefenceMod / 100 - amount * player1.GetComponent<BackStats>().DefenceMod / 100 - amount * headAmout / 100;

        amount = amount - itemDefenceMod;

        health -= amount;

        healthStatusBar.UpdateStatus(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger(takeDamageHash);
        }

        //healthSlider.value = health;
        //shieldSlider.value = shield;

        //if (health <= 0)
        //{
        //    //SkinnedMeshRenderer render = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        //    //render.enabled = false;
        //    PlayerKilled();
        //}
    }

    private void Die()
    {
        animator.SetTrigger(dieHash);

        GameObject.FindGameObjectWithTag("Player1").GetComponent<HeroController>().enabled = false;

        StartCoroutine(DelayedGameOverMenu());
    }

    IEnumerator DelayedGameOverMenu()
    {
        yield return new WaitForSeconds(1.5f);

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<PauseController>().GameOver();
    }

            /// <summary>
            /// Let player take damage by amount. If player is killed respawn after 5 seconds
            /// Also handle update of kill score for player who fired killing bullet
            /// </summary>
            /// <param name="other"></param>
            /// <param name="amount"></param>
            /// <param name="bullet"></param>
    public void TakeDamage(Collider other, int amount, GameObject bullet)
    {
        // Decrement the player's health or shield by amount.
        if (shield > 0)
            shield -= amount;
        else
            health -= amount;

        healthSlider.value = health;
        shieldSlider.value = shield;

        if (health <= 0)
        {
            PlayerKilled();

            if (bullet.tag == "Player1")
            {
                GameControllerOld.Player1Kills++;
            }
            if (bullet.tag == "Player2")
            {
                GameControllerOld.Player2Kills++;
            }
            if (bullet.tag == "Player3")
            {
                GameControllerOld.Player3Kills++;
            }
            if (bullet.tag == "Player4")
            {
                GameControllerOld.Player4Kills++;
            }

            //Game ends when first player has 5 kills
            if (GameControllerOld.Player1Kills > 4 | GameControllerOld.Player2Kills > 4 | GameControllerOld.Player3Kills > 4 | GameControllerOld.Player4Kills > 4)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
            }
        }
    }

    /// <summary>
    /// Start reoutine for respawn
    /// </summary>
    private void PlayerKilled()
    {
        StartCoroutine(Respawn());
    }

    /// <summary>
    /// Respawn
    /// </summary>
    /// <returns></returns>
    IEnumerator Respawn()
    {
        SkinnedMeshRenderer render = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

        render.enabled = false;
        gameObject.transform.position = new Vector3(99, 0, -99);

        yield return new WaitForSeconds(5);

        ResetHealth();

        if (gameObject.tag == "Player1")
        {
            gameObject.transform.position = new Vector3(13, 0, -13);
        }
        if (gameObject.tag == "Player2")
        {
            gameObject.transform.position = new Vector3(-13, 0, 13);
        }
        if (gameObject.tag == "Player3")
        {
            gameObject.transform.position = new Vector3(-13, 0, -13);
        }
        if (gameObject.tag == "Player4")
        {
            gameObject.transform.position = new Vector3(13, 0, 13);
        }

        gameObject.GetComponent<Animation_Controller>().pistol.SetActive(true);
        gameObject.GetComponent<Animation_Controller>().akm.SetActive(false);
        render.enabled = true;
    }
}
