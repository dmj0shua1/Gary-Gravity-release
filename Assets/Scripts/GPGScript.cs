using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPGScript : MonoBehaviour {

    public static GPGScript Instance { get; private set; }
    public string incrementalAchId,standardAchId;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetPoint()
    {
        ManagerScript.Instance.IncrementCounter();
    }

    public void Restart()
    {
        ManagerScript.Instance.RestartGame();
    }

    public void Increment()
    {
        PlayGamesManager.IncrementAchievement(incrementalAchId, 5);
    }

    public void Unlock()
    {
        PlayGamesManager.UnlockAchievement(standardAchId);
    }

    public void ShowAchievements()
    {
        PlayGamesManager.ShowAchievements();
    }

    public void ShowLeaderboards()
    {
        PlayGamesManager.ShowLeaderboards();
    }

    public void LoadAc()
    {
        PlayGamesManager.LoadAchievements();
    }


}
