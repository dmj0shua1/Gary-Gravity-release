using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cooldownShield : MonoBehaviour {


     SwipeMovement swipeScript;
     GameObject character, player, goPanel, btnPause, ui, maincam, ShieldLane;
     public Slider ShieldbarSlider;
     RdmObjGen rdmobj;
      bool invokeCollectiblesOnce = false;
      Button btnBoost, btnAttack, btnShield;
    //  ShieldMusic ShieldMusicScript;
      Animation anim;
      Sprite sprShield;
      public float regenShieldSpeed = 0.005f, regenShieldMultiplier = 0.0004f;
      int shieldSkillLvl = 0;
      bool isRunning = false;
      public bool isBoosting = false;
      private IEnumerator coroutine;
      public bool shieldIsOn = false;

	// Use this for initialization
	void Start () {
        maincam = Camera.main.gameObject;
        ShieldLane = GameObject.Find("ShieldLane");
        btnBoost = GameObject.Find("btnBoost").GetComponent<Button>();
        btnAttack = GameObject.Find("btnAttack").GetComponent<Button>();
        btnShield = GameObject.Find("btnShield").GetComponent<Button>();
        btnPause = GameObject.Find("Pause");
        swipeScript = maincam.GetComponent<SwipeMovement>();

        if (PlayerPrefs.HasKey("shieldSkillLvl")) shieldSkillLvl = PlayerPrefs.GetInt("shieldSkillLvl");
        else shieldSkillLvl = 0;

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
            maincam = GameObject.Find("Main Camera");
            rdmobj = GameObject.Find("RdmGen").GetComponent<RdmObjGen>();
            anim = character.GetComponent<Animation>();

            if (ShieldbarSlider.value == ShieldbarSlider.maxValue)
            {

                //change button color
                sprShield = Resources.Load<Sprite>("Sprites/UI/btnShield");
                btnShield.GetComponent<Image>().sprite = sprShield;
            }
            else
            {
                //change button color
                sprShield = Resources.Load<Sprite>("Sprites/UI/btnShieldGrayscale");
                btnShield.GetComponent<Image>().sprite = sprShield;
            }

            if (ShieldbarSlider.value <= 0f && shieldSkillLvl >= 1)
            {

                if (!isRunning && !shieldIsOn) regenShield();
            }
        }
    }

	// Update is called once per frame
	void Update () {


	}

    public void regenShield()
    {
        isRunning = true;
        InvokeRepeating("regenShieldInvoked", 4, 4);
        
    }

   

    void regenShieldInvoked()
    {

        if (ShieldbarSlider.value >= ShieldbarSlider.maxValue)
        {
            isRunning = false;
            rdmobj.stopShieldNow = false;
            CancelInvoke("regenShieldInvoked");
        }
        else
        {
            if (!isBoosting)
            {
                ShieldbarSlider.value += regenShieldSpeed + (regenShieldMultiplier * shieldSkillLvl);
                swipeScript.shieldCap += regenShieldSpeed + (regenShieldMultiplier * shieldSkillLvl);
            }
          //invokeCollectiblesOnce = !invokeCollectiblesOnce;
        }
    }

    public void decShield()
    {
        ShieldbarSlider.value = 0f;
        swipeScript.shieldCap = 0f;
        shieldIsOn=true;
        
    }
      

  

  
}
