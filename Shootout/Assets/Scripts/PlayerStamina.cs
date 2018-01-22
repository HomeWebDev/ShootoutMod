using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour {

    public float Stamina = 100;
    public float StaminaRegenRate = 1;
    public UltimateStatusBar StaminaStatusBar;

    public float maxStamina;
    private float nextUpdate = 0.1f;
    private GameObject player1;

    // Use this for initialization
    void Start () {
        maxStamina = Stamina;

        StaminaStatusBar.screenSpaceOptions.xRatio = maxStamina / 1000;
        StaminaStatusBar.UpdatePositioning();

        StaminaStatusBar.UpdateStatus(Stamina, maxStamina);

        player1 = GameObject.FindGameObjectWithTag("Player1");
    }

    public void UpdateStatusBar()
    {
        StaminaStatusBar.screenSpaceOptions.xRatio = maxStamina / 1000;
        StaminaStatusBar.UpdatePositioning();

        StaminaStatusBar.UpdateStatus(Stamina, maxStamina);
    }

    // Update is called once per frame
    void Update () {

        //Regenerate stamina every interval
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Time.time + 0.1f;
            RegenStamina();
        }
    }

    private void RegenStamina()
    {
        Stamina += maxStamina / 100 * StaminaRegenRate;
        //Stamina += StaminaRegenRate;
        if(Stamina > maxStamina)
        {
            Stamina = maxStamina;
        }
        StaminaStatusBar.UpdateStatus(Stamina, maxStamina);
    }

    public bool DepleteStamina(float value)
    {
        //Add stamina modifier
        value = value - player1.GetComponent<CostumeStats>().StaminaMod;
        if (Stamina >= value)
        {
            Stamina -= value;
            StaminaStatusBar.UpdateStatus(Stamina, maxStamina);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMaxStamina()
    {
        Stamina = maxStamina;
    }
}
