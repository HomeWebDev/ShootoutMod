using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour {

    public float Magic = 100;
    public float MagicRegenRate = 1;
    public UltimateStatusBar MagicStatusBar;

    public float maxMagic;
    private float nextUpdate = 0.1f;
    private GameObject player1;

    // Use this for initialization
    void Start () {
        maxMagic = Magic;

        MagicStatusBar.screenSpaceOptions.xRatio = maxMagic / 1000;
        MagicStatusBar.UpdatePositioning();

        MagicStatusBar.UpdateStatus(Magic, maxMagic);

        player1 = GameObject.FindGameObjectWithTag("Player1");
    }

    public void UpdateStatusBar()
    {
        MagicStatusBar.screenSpaceOptions.xRatio = maxMagic / 1000;
        MagicStatusBar.UpdatePositioning();

        Debug.Log("MagicXRatio: " + MagicStatusBar.screenSpaceOptions.xRatio);
        MagicStatusBar.UpdateStatus(Magic, maxMagic);
    }

    public void UpdateStatusBarWithoutPosition()
    {
        MagicStatusBar.UpdateStatus(Magic, maxMagic);
    }

    // Update is called once per frame
    void Update () {

        if(!player1.GetComponent<HeroController>().magicEnabled)
        {
            //Regenerate magic every interval if magic not enabled
            if (Time.time >= nextUpdate)
            {
                nextUpdate = Time.time + 0.1f;
                RegenMagic();
            }
        }
    }

    private void RegenMagic()
    {
        //Magic += maxMagic / 100 * MagicRegenRate;
        //if (Magic > maxMagic)
        //{
        //    Magic = maxMagic;
        //}
        //MagicStatusBar.UpdateStatus(Magic, maxMagic);
    }

    public bool DepleteMagic(float value)
    {

        float itemValue = player1.GetComponent<CostumeStats>().MagicMod - player1.GetComponent<BackStats>().MagicMod - player1.GetComponent<HeadStats>().MagicMod;
        value = value - itemValue;
        if (Magic >= value)
        {
            Magic -= value;
            MagicStatusBar.UpdateStatus(Magic, maxMagic);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMaxMagic()
    {
        Magic = maxMagic;
    }
}
