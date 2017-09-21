using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {

    public static ManagerScript Instance { get; private set; }
    public static int Counter {get; private set;}

	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementCounter()
    {
        Counter++;
    }

    public void RestartGame()
    {
        PlayGamesManager.AddScoreToLeaderboard(GaryGravityResources.leaderboard_swifest_leaderboard, Counter);
        Counter = 0;
    }
}
