using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
public class objectdestroyer : MonoBehaviour
{

    // Use this for initialization
    public GameObject explosion, btnShieldObj;
    public Boolean isDestroyer = false;
    shieldToggle shieldToggleScript;
    cooldownShield cdShieldScript;
    int achDemolitionDerby = 0, achTv = 0;
    spawnProjectile spawnProjectileScript;
    void Start()
    {
        cdShieldScript = GameObject.Find("cooldownShield").GetComponent<cooldownShield>();
        spawnProjectileScript = GameObject.Find("btnAttack").GetComponent<spawnProjectile>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider obj)
    {


        if (isDestroyer && (obj.tag == "Foreground" || obj.tag == "Obstacles"))
        {
            Destroy(obj.gameObject);
        }
        else if (obj.tag == "Obstacles")
        {
            if (gameObject.name == "playerShield")
            {
                try
                { Instantiate(explosion, transform.position, transform.rotation); }
                catch (Exception e) { }
                btnShieldObj = GameObject.Find("btnShield");
                /*if (SceneManager.GetActiveScene().name == "Game") */
                shieldToggleScript = btnShieldObj.GetComponent<shieldToggle>();
                shieldToggleScript.deactivateShield();
                Destroy(obj.gameObject);
                GameObject objSfxDestByShield = GameObject.Find("sfxDestroyedByShield");
                AudioSource asSfxDestByShield = objSfxDestByShield.GetComponent<AudioSource>();
                asSfxDestByShield.Play();
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                BoxCollider colPlayer = player.GetComponent<BoxCollider>();
                cdShieldScript.regenShield();

                //achievement
                if (obj.gameObject.name == "Tv(Clone)")
                {
                    if (achTv < 4)
                    {
                        achTv++;
                    }
                    else if (achTv >= 4)
                    {
                        PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEA");
                    }
                }
            }
            else if (gameObject.tag == "PowerAttack")
            {
                GameObject objSfxDestByAttack = GameObject.Find("sfxDestroyedByAttack");
                AudioSource asSfxDestByAttack = objSfxDestByAttack.GetComponent<AudioSource>();
                asSfxDestByAttack.Play();

                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

                Invoke("destroyExp", 0.5f);
                Destroy(gameObject);
                Destroy(obj.gameObject);

                //achievement


                if (spawnProjectileScript.achBlindsight >= 1) spawnProjectileScript.achBlindsight--;

                if (obj.gameObject.name == "Tv(Clone)")
                {
                    if (achTv < 4)
                    {
                        achTv++;
                    }
                    else if (achTv >= 4)
                    {
                        PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQEA");
                        GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQEA", 100f, true);
                    }
                }

                if (achDemolitionDerby < 2)
                {
                    achDemolitionDerby++;
                }
                else if (achDemolitionDerby >= 2)
                {
                    PlayGamesManager.UnlockAchievement("CgkI1OXD-eYaEAIQCg");
                    GKAchievementReporter.ReportAchievement("CgkI1OXDeYaEAIQCg", 100f, true);
                }
            }


        }
    }

    void destroyExp()
    {
        Destroy(explosion);
    }


}
