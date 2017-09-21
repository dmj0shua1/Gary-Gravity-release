using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cooldownAttack : MonoBehaviour {


     SwipeMovement swipeScript;
     GameObject character, player, goPanel, btnPause, ui, maincam;
     public Slider AttackbarSlider;
     RdmObjGen rdmobj;
      bool invokeCollectiblesOnce = false;
      Button btnBoost, btnAttack, btnShield;
    //  AttackMusic AttackMusicScript;
      Animation anim;
      Sprite sprAttack;
      public float regenAttackSpeed = 0.005f, regenAttackMultiplier = 0.00025f;
      int attackSkillLvl = 0;
      bool isRunning = false;
      public bool isBoosting = false;
      private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
        maincam = Camera.main.gameObject;
        btnShield = GameObject.Find("btnShield").GetComponent<Button>();
        btnAttack = GameObject.Find("btnAttack").GetComponent<Button>();
        btnBoost = GameObject.Find("btnBoost").GetComponent<Button>();
        btnPause = GameObject.Find("Pause");
        swipeScript = maincam.GetComponent<SwipeMovement>();

        if (PlayerPrefs.HasKey("attackSkillLvl")) attackSkillLvl = PlayerPrefs.GetInt("attackSkillLvl");
        else attackSkillLvl = 0;


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
            if (AttackbarSlider.value == AttackbarSlider.maxValue)
            {

                //change button color
                sprAttack = Resources.Load<Sprite>("Sprites/UI/btnAttack");
                btnAttack.GetComponent<Image>().sprite = sprAttack;
            }
            else
            {
                //change button color
                sprAttack = Resources.Load<Sprite>("Sprites/UI/btnAttackGrayscale");
                btnAttack.GetComponent<Image>().sprite = sprAttack;
            }

            if (AttackbarSlider.value <= 0f && attackSkillLvl >= 1)
            {

                if (!isRunning) regenAttack();
            }
 
        }
    }

	
	// Update is called once per frame
	void Update () {


	}

    void regenAttack()
    {
        isRunning = true;
        InvokeRepeating("regenAttackInvoked", 4, 4);
        
    }

    void regenAttackInvoked()
    {

        if (AttackbarSlider.value >= AttackbarSlider.maxValue)
        {
            isRunning = false;
            rdmobj.stopAttackNow = false;
            CancelInvoke("regenAttackInvoked");
            
        }
        else
        {
            if (!isBoosting)
            {
                AttackbarSlider.value += regenAttackSpeed + (regenAttackMultiplier * attackSkillLvl);
                swipeScript.attackCap += regenAttackSpeed + (regenAttackMultiplier * attackSkillLvl);
            }
          //invokeCollectiblesOnce = !invokeCollectiblesOnce;
         
        }
    }

    public void decAttack()
    {
        AttackbarSlider.value = 0f;
        swipeScript.attackCap = 0f;
        regenAttack();

     
    }
      

  

  
}
