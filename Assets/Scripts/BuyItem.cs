using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
public class BuyItem : MonoBehaviour {
    public string itemName;
    public int itemPrice,playerGold;
    public double itemTime;
    private double tempItemTime = 0;
    public Text curGold;
  //  public string itmTime;
     PrintTimeBoost printTimeBoost;
     PrintTimeAttack printTimeAttack;
     PrintTimeShield printTimeShield;
    public GameObject printTime,buyNotif;
    PlayerGold pGold;
    Text timerBoost, timerAttack, timerShield;




	// Use this for initialization
	void Start () {
        printTime = GameObject.Find("timerPrinter"+itemName);
        timerBoost = GameObject.Find("timerBoost").GetComponent<Text>();
        timerAttack = GameObject.Find("timerAttack").GetComponent<Text>();
        timerShield = GameObject.Find("timerShield").GetComponent<Text>();
      //  gameObject.SendMessage("itemTime", 69);
        
	}
	
	// Update is called once per frame
	void Update () {
        //achievement
        if (PlayerPrefs.HasKey("endTimeboost") && PlayerPrefs.HasKey("endTimeattack") && PlayerPrefs.HasKey("endTimeshield"))
        {
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQFA");
            GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQFA", 100f, true);
            print("born ready!");
        }
	}

    public void buyItem()
    {
        playerGold = PlayerPrefs.GetInt("PlayerGold");
                //check player gold
            if (playerGold >= itemPrice && PlayerPrefs.HasKey(itemName+"SkillLvl"))
            {
   

                //play audio
                GameObject objSfxPurchasing = GameObject.Find("sfxPurchasing");
                AudioSource asSfxPuchasing = objSfxPurchasing.GetComponent<AudioSource>();
                asSfxPuchasing.Play();

                buyNotif.SetActive(true);

                //check if player has this item already

                if (itemTime <= 480)
                {
                    if ((PlayerPrefs.HasKey("endTime" + itemName)))
                    {
                        print("BEFORE: " + itemTime.ToString());
                        if (tempItemTime == 0) tempItemTime = itemTime;
                        itemTime = (Convert.ToDateTime(PlayerPrefs.GetString("endTime" + itemName)) - System.DateTime.Now).TotalMinutes + tempItemTime;
                        print("AFTER: " + itemTime.ToString());
                    }


                    if (itemName == "boost") {
                        printTimeBoost = printTime.GetComponent<PrintTimeBoost>();
                        printTimeBoost.itmTime = itemTime;
                        printTimeBoost.itemName = itemName;
                        printTimeBoost.iprintna();
                    }
                    else if (itemName == "attack")
                    {
                        printTimeAttack = printTime.GetComponent<PrintTimeAttack>();
                        printTimeAttack.itmTime = itemTime;
                        printTimeAttack.itemName = itemName;
                        printTimeAttack.iprintna();
                    }
                    else if (itemName == "shield")
                    {
                        printTimeShield = printTime.GetComponent<PrintTimeShield>();
                        printTimeShield.itmTime = itemTime;
                        printTimeShield.itemName = itemName;
                        printTimeShield.iprintna();
                    }
       
                //decrease price from gold
                PlayerPrefs.SetInt("PlayerGold", PlayerPrefs.GetInt("PlayerGold") - itemPrice);
                //curGold.text = PlayerPrefs.GetInt("PlayerGold").ToString();
                pGold = GameObject.Find("PlayerGoldMenu").GetComponent<PlayerGold>();
                pGold.playerGold = PlayerPrefs.GetInt("PlayerGold");
                PlayerPrefs.Save();
          
                }


           

            }
            else
            {
                //insufficient
                print("Insufficient Gold");

                //play audio
                GameObject objSfxInsufficientGold = GameObject.Find("sfxInsufficientGold");
                AudioSource asSfxInsufficientGold = objSfxInsufficientGold.GetComponent<AudioSource>();
                asSfxInsufficientGold.Play();
            }
        
       
    }

   
}

