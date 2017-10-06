using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("Music")) PlayerPrefs.SetInt("Music", 1);
        if (!PlayerPrefs.HasKey("Sfx")) PlayerPrefs.SetInt("Sfx", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void sfxtoggle()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
        {
            PlayerPrefs.SetInt("Sfx", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sfx", 1);
        }
    }

    public void musictoggle()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
        }
    }
}
