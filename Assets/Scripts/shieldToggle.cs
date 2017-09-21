using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shieldToggle : MonoBehaviour
{

    // Use this for initialization
    GameObject pShield, sprPlayerShield;
    Button btnShield;
    RdmObjGen rdmobj;
    cooldownShield cooldownShieldScript;
    scoreCounter scoreCounter;
    int achPowerTripperShield = 0;
    achPowerTripper achPowerTripScript;
    void Start()
    {
        rdmobj = GameObject.Find("RdmGen").GetComponent<RdmObjGen>();
        btnShield = gameObject.GetComponent<Button>();
        scoreCounter = GameObject.Find("subScoreCounter").GetComponent<scoreCounter>();
        achPowerTripScript = GameObject.Find("btnBoost").GetComponent<achPowerTripper>();
    }

    void Update()
    {



    }

    void activateShield()
    {
        pShield = GameObject.Find("playerShield");
        Collider col = pShield.GetComponent<Collider>();
        col.enabled = true;
        sprPlayerShield = GameObject.Find("sprite_playerShield");
        SpriteRenderer ren = sprPlayerShield.GetComponent<SpriteRenderer>();
        ren.enabled = true;

        cooldownShieldScript = GameObject.Find("cooldownShield").GetComponent<cooldownShield>();
        cooldownShieldScript.decShield();

        //achievement
        scoreCounter.LavidaLoca = false;
    }

    public void deactivateShield()
    {
        pShield = GameObject.Find("playerShield");
        Collider col = pShield.GetComponent<Collider>();
        col.enabled = false;
        sprPlayerShield = GameObject.Find("sprite_playerShield");
        SpriteRenderer ren = sprPlayerShield.GetComponent<SpriteRenderer>();
        ren.enabled = false;
    }

    // Update is called once per frame


    public void onShield()
    {
        activateShield();
        btnShield.interactable = false;

        //achievement
        if (achPowerTripperShield < 1)
        {
            achPowerTripperShield++;
        }
        else if (achPowerTripperShield == 1)
        {
            achPowerTripScript.achPowerTrip++;
            achPowerTripScript.powerTrip();
            achPowerTripperShield++;
        }
    }
}
