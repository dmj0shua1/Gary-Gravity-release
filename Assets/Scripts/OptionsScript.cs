using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public GameObject intro, loop;
    private AudioSource introMusic, loopMusic;
      [SerializeField]
    private Sprite toggledOff, toggledOn;
    [SerializeField]
      private GameObject btnMusic, btnSfx;
      
    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("Music")) PlayerPrefs.SetInt("Music", 1);
        if (!PlayerPrefs.HasKey("Sfx")) PlayerPrefs.SetInt("Sfx", 1);
        intro = GameObject.Find("Intro");
        loop = GameObject.Find("Loop");
        introMusic = intro.GetComponent<AudioSource>();
        loopMusic = loop.GetComponent<AudioSource>();
        checkSprite();
        
    }

    void checkSprite()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
        {
            btnSfx.GetComponent<Image>().sprite = toggledOn;
        }
        else
        {
            btnSfx.GetComponent<Image>().sprite = toggledOff;
        }

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            btnMusic.GetComponent<Image>().sprite = toggledOn;
        }
        else
        {
            btnMusic.GetComponent<Image>().sprite = toggledOff;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void sfxtoggle()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
        {
            btnSfx.GetComponent<Image>().sprite = toggledOff;
            PlayerPrefs.SetInt("Sfx", 0);

        
        }
        else
        {
            btnSfx.GetComponent<Image>().sprite = toggledOn;
            PlayerPrefs.SetInt("Sfx", 1);
        }
    }

    public void musictoggle()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            introMusic.Stop();
            loopMusic.Stop();
            btnMusic.GetComponent<Image>().sprite = toggledOff;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            if (!loopMusic.isPlaying)
            {
                introMusic.Play();
                loopMusic.PlayDelayed(12);

            }
            btnMusic.GetComponent<Image>().sprite = toggledOn;
            PlayerPrefs.SetInt("Music", 1);
        }
    }
}
