using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class spawnProjectile : MonoBehaviour
{

    // Use this for initialization

    public GameObject spawnPointProjectile, character;
    public GameObject[] projectile;
    Button btnAttack;
    RdmObjGen rdmobj;
    cooldownAttack cooldownAttackScript;
    Animation playerAnim;
    int projIndex;
    scoreCounter scoreCounter;
    public int achBlindsight = 0;
    int achPowerTripperAttack = 0;
    achPowerTripper achPowerTripScript;
    coinReward coinRewardScript;
    void Start()
    {
        rdmobj = GameObject.Find("RdmGen").GetComponent<RdmObjGen>();
        spawnPointProjectile = GameObject.Find("spawnPointProjectile");
        btnAttack = gameObject.GetComponent<Button>();
        cooldownAttackScript = GameObject.Find("cooldownAttack").GetComponent<cooldownAttack>();
        scoreCounter = GameObject.Find("subScoreCounter").GetComponent<scoreCounter>();
        achPowerTripScript = GameObject.Find("btnBoost").GetComponent<achPowerTripper>();

        coinRewardScript = GameObject.Find("pnlCoinReward").GetComponent<coinReward>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void fireProjectile()
    {
        character = GameObject.FindGameObjectWithTag("character");
        playerAnim = character.GetComponent<Animation>();

        if (PlayerPrefs.GetString("chosenChar") == "Shopper_girl")
        {
            playerAnim.Play("shopper_girl_attack");
            Invoke("replayFloat", 0.3f);
            projIndex = 0;
        }
        else if (PlayerPrefs.GetString("chosenChar") == "Fireman")
        {
            playerAnim.Play("gary_attack_anim");
            Invoke("replayFloat", 0.3f);
            projIndex = 1;
        }

        Instantiate(projectile[projIndex], spawnPointProjectile.transform.position, spawnPointProjectile.transform.rotation);
        btnAttack.interactable = false;
        cooldownAttackScript.decAttack();

        //achievement
        scoreCounter.LavidaLoca = false;
        achBlindsight++;

        if (achBlindsight == 3 && !PlayerPrefs.HasKey("CgkI1OXD-eYaEAIQDQ"))
        {
            PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQDQ");
            GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQDQ", 100f, true);
            coinRewardScript.giveMediumReward();
            PlayerPrefs.SetInt("CgkI1OXD-eYaEAIQDQ", 1);
        }
           
        if (achPowerTripperAttack < 1)
        {
            achPowerTripperAttack++;
        }
        else if (achPowerTripperAttack == 1)
        {
            achPowerTripScript.achPowerTrip++;
            achPowerTripScript.powerTrip();
            achPowerTripperAttack++;
        }
    }

    void replayFloat()
    {
        if (PlayerPrefs.GetString("chosenChar") == "Shopper_girl") playerAnim.CrossFade("shopper_idle_anim_root");
        else if (PlayerPrefs.GetString("chosenChar") == "Fireman") playerAnim.CrossFade("gary_idle_anim");

    }


}
