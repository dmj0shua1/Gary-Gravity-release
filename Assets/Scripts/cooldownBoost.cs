using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cooldownBoost : MonoBehaviour
{

    SwipeMovement swipeScript;
    GameObject character, player, goPanel, btnPause, ui, maincam, boostLane, objBoostTornadoL, objBoostTornadoR, explosion;
    public Slider boostbarSlider;
    RdmObjGen rdmobj;
    bool invokeCollectiblesOnce = false;
    Button btnShield, btnAttack, btnBoost;
    boostMusic boostMusicScript;
    Animation anim;
    Sprite sprBoost, sprShield, sprAttack;
    Animator boostFlashAnim, hudAnim;
    AudioSource sfxExplosion;
    GameObject boostProps, sprPlayerShield;
    boostProp boostPropScript;
    public float regenBoostSpeed = 0.005f, regenBoostMultiplier = 0.0005f, boostDuration = 16f;
    BoxCollider charCollider, shieldCollider;
    bool isRunning = false;
    int boostSkillLvl = 0;
    cooldownAttack cDownAttack;
    cooldownShield cDownShield;
    private IEnumerator coroutine;
    pauseScale pauseScript;
    scoreCounter scoreCounter;
    int achPowerTripperBoost = 0;
    achPowerTripper achPowerTripScript;
    // Use this for initialization
    void Start()
    {
        scoreCounter = GameObject.Find("subScoreCounter").GetComponent<scoreCounter>();
        PlayerPrefs.SetInt("boost3timesAch", 0);
        boostLane = GameObject.Find("BoostLane");
        btnShield = GameObject.Find("btnShield").GetComponent<Button>();
        btnAttack = GameObject.Find("btnAttack").GetComponent<Button>();
        btnBoost = GameObject.Find("btnBoost").GetComponent<Button>();
        btnPause = GameObject.Find("Pause");
        maincam = GameObject.Find("Main Camera");
        swipeScript = maincam.gameObject.GetComponent<SwipeMovement>();
        boostPropScript = GameObject.FindGameObjectWithTag("Player").GetComponent<boostProp>();
        cDownAttack = GameObject.Find("cooldownAttack").GetComponent<cooldownAttack>();
        cDownShield = GameObject.Find("cooldownShield").GetComponent<cooldownShield>();
        pauseScript = GameObject.Find("onPause").GetComponent<pauseScale>();
        achPowerTripScript = GameObject.Find("btnBoost").GetComponent<achPowerTripper>();

        if (PlayerPrefs.HasKey("boostSkillLvl")) boostSkillLvl = PlayerPrefs.GetInt("boostSkillLvl");
        else boostSkillLvl = 0;

        if (boostbarSlider.value <= 0f && boostSkillLvl >= 1)
        {
            if (!isRunning) regenBoost();
        }

        coroutine = WaitAndUpdate(1.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndUpdate(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            //declarations
            character = GameObject.FindGameObjectWithTag("character");
            rdmobj = GameObject.Find("RdmGen").GetComponent<RdmObjGen>();
            anim = character.GetComponent<Animation>();

            if (boostbarSlider.value == boostbarSlider.maxValue)
            {


              /*  if (!invokeCollectiblesOnce)
                {
                    rdmobj.invokeCollectibles = true;
                    invokeCollectiblesOnce = true;
                }*/
                //change button color
                sprBoost = Resources.Load<Sprite>("Sprites/UI/btnBoost");
                btnBoost.GetComponent<Image>().sprite = sprBoost;
            }
            else
            {
                //change button color
                sprBoost = Resources.Load<Sprite>("Sprites/UI/btnBoostGrayscale");
                btnBoost.GetComponent<Image>().sprite = sprBoost;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void regenBoost()
    {
        isRunning = true;
        InvokeRepeating("regenBoostInvoked", 4, 4);

    }

    void regenBoostInvoked()
    {

        if (boostbarSlider.value >= boostbarSlider.maxValue)
        {
            isRunning = false;
            rdmobj.stopBoostNow = false;
            CancelInvoke("regenBoostInvoked");
        }
        else
        {
            print("regenBoostSpeed: " + regenBoostSpeed.ToString() + ",regenBoostMulti: " + regenBoostMultiplier.ToString() + ",Slider: " + boostbarSlider.value.ToString() + ", skillLvl: " + boostSkillLvl.ToString());
            boostbarSlider.value += regenBoostSpeed + (regenBoostMultiplier * boostSkillLvl);
            swipeScript.boostCap += regenBoostSpeed + (regenBoostMultiplier * boostSkillLvl);
            invokeCollectiblesOnce = !invokeCollectiblesOnce;
        }
    }
    public void startBoost()
    {

        swipeScript.targetPosition = boostLane.transform.position;
        Invoke("timeScaler", 1.6f);
        Invoke("decBoost", 4f);

        // disable buttons
        btnPause.GetComponent<Button>().interactable = false;
        btnBoost.GetComponent<Button>().enabled = false;
        btnShield.GetComponent<Button>().enabled = false;
        btnAttack.GetComponent<Button>().enabled = false;

        //pauseCooldowns
        cDownAttack.isBoosting = true;
        cDownShield.isBoosting = true;
        pauseScript.isBoosting = true;

        //achievement
        scoreCounter.LavidaLoca = false;
        if (PlayerPrefs.GetInt("boost3timesAch") == 2)
        {
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQAg");
        }
        else
        {
            PlayerPrefs.SetInt("boost3timesAch", PlayerPrefs.GetInt("boost3timesAch") + 1);
        }

        if (achPowerTripperBoost < 1)
        {
            achPowerTripperBoost++;
        }
        else if (achPowerTripperBoost == 1)
        {
            achPowerTripScript.achPowerTrip++;
            achPowerTripScript.powerTrip();
            achPowerTripperBoost++;
        }


    }



    void timeScaler()
    {
        Time.timeScale = 4f;

        //enable boost props
        if (character.name == "Char")
        {
            anim.CrossFade("gary_boost_animation");
            boostProps = boostPropScript.objBoostProp;
            boostProps.SetActive(true);
            boostPropScript.objIdleProp.SetActive(false);
            Animation hose_anim = boostProps.GetComponent<Animation>();

        }
        else if (PlayerPrefs.GetString("chosenChar") == "Shopper_girl")
        {
            //turnOnTornados
            objBoostTornadoL = GameObject.Find("boostTornadoL");
            objBoostTornadoR = GameObject.Find("boostTornadoR");
            objBoostTornadoL.GetComponent<SpriteRenderer>().enabled = true;
            objBoostTornadoR.GetComponent<SpriteRenderer>().enabled = true;

            if (character.name == "Ruth2") anim.CrossFade("shopper_girl_spin");
        }

    }

    void Unboost()
    {
        swipeScript = maincam.gameObject.GetComponent<SwipeMovement>();
        swipeScript.currentLane = "Lane2";
        swipeScript.targetPosition = swipeScript.target2.transform.position;

        charCollider = GameObject.FindGameObjectWithTag("character").GetComponent<BoxCollider>();
        charCollider.enabled = true;
        sprPlayerShield = GameObject.Find("sprite_playerShield");
        SpriteRenderer ren = sprPlayerShield.GetComponent<SpriteRenderer>();

        if (ren.enabled)
        {
            shieldCollider = GameObject.Find("playerShield").GetComponent<BoxCollider>();
            shieldCollider.enabled = true;
        }


        Time.timeScale = 1f;
        //destroy all obstacles
        GameObject[] obs = GameObject.FindGameObjectsWithTag("Obstacles");
        sfxExplosion = GameObject.Find("sfxDestroyedByAttack").GetComponent<AudioSource>();
        explosion = (GameObject)Resources.Load("Visuals/attackExplosion", typeof(GameObject));

        foreach (GameObject ob in obs)
        {
            Destroy(ob);
            if (PlayerPrefs.GetInt("Sfx") == 1) sfxExplosion.Play();
            Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        }

        //destroy collectiblecaps
        GameObject[] obscap = GameObject.FindGameObjectsWithTag("CollectiblesCap");
        foreach (GameObject obcap in obscap)
        {
            Destroy(obcap);
        }

        //destroy collectibleboost
        GameObject[] obsboost = GameObject.FindGameObjectsWithTag("CollectiblesBoost");
        foreach (GameObject obboost in obsboost)
        {
            Destroy(obboost);
        }

        //enable buttons
        btnPause.GetComponent<Button>().interactable = true;
        btnBoost.GetComponent<Button>().enabled = true;
        btnShield.GetComponent<Button>().enabled = true;
        btnAttack.GetComponent<Button>().enabled = true;
        hudAnim = GameObject.Find("hud").GetComponent<Animator>();
        hudAnim.SetBool("isBoosting", false);

        //change button color
        /*  sprBoost = Resources.Load<Sprite>("Sprites/UI/btnBoost");
          sprShield = Resources.Load<Sprite>("Sprites/UI/btnShield");
          sprAttack = Resources.Load<Sprite>("Sprites/UI/btnAttack");*/

        /*   btnBoost.GetComponent<Image>().sprite = sprBoost;
           btnShield.GetComponent<Image>().sprite = sprShield;
           btnAttack.GetComponent<Image>().sprite = sprAttack;*/

        if (PlayerPrefs.GetString("chosenChar") == "Shopper_girl")
        {
            //turnOffTornados
            objBoostTornadoL = GameObject.Find("boostTornadoL");
            objBoostTornadoR = GameObject.Find("boostTornadoR");
            objBoostTornadoL.GetComponent<SpriteRenderer>().enabled = false;
            objBoostTornadoR.GetComponent<SpriteRenderer>().enabled = false;

            anim.CrossFade("shopper_idle_anim_root");
        }
        else if (PlayerPrefs.GetString("chosenChar") == "Fireman")
        {
            boostProps.SetActive(false);
            boostPropScript.objIdleProp.SetActive(true);

            anim.CrossFade("gary_idle_anim");
        }

        //stop boost music
        GameObject objboostMusic = GameObject.Find("btnBoost");
        boostMusicScript = objboostMusic.GetComponent<boostMusic>();
        boostMusicScript.stopBoostMusic();

        if (boostbarSlider.value <= 0f && boostSkillLvl >= 1)
        {

            if (!isRunning) regenBoost();
        }

        //resume cooldowns
        cDownAttack.isBoosting = false;
        cDownShield.isBoosting = false;
        pauseScript.isBoosting = false;
    }

    void decBoost()
    {
        /*  if (swipeScript.boostCap > 0f)
          {
              swipeScript = maincam.gameObject.GetComponent<SwipeMovement>();
              swipeScript.boostCap -= 0.5f;
              boostbarSlider.value -= 0.5f;
          }else 
          {
              //regenBoostNow = true;
              CancelInvoke("decBoost");
              Unboost();
          }*/

        swipeScript.boostCap = 0f;
        boostbarSlider.value = 0f;
        Invoke("Unboost", boostDuration);
    }
}
