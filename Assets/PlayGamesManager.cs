using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayGamesManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
#if UNITY_ANDROID
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
#endif
        if (SceneManager.GetActiveScene().name == "splash")
        {
            PlayerPrefs.SetInt("askedForGPG", 0);
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (PlayerPrefs.GetInt("askedForGPG") == 0)
            {
                SignIn();
                PlayerPrefs.SetInt("askedForGPG", 1);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SignIn()
    {

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("SignIn Success");
            }
            else
            {
                Debug.Log("SignIn failed");
            }
        });

    }

    public void SignOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }


    #region Achievements
    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public static void LoadAchievements()
    {
        Social.LoadAchievements(achievements =>
        {
            if (achievements.Length > 0)
            {
                Debug.Log("Got " + achievements.Length + " achievement instances");
                string myAchievements = "My achievements:\n";
                foreach (IAchievement achievement in achievements)
                {
                    myAchievements += "\t" +
                        achievement.id + " " +
                        achievement.percentCompleted + " " +
                        achievement.completed + " " +
                        achievement.lastReportedDate + "\n";
                }
                Debug.Log(myAchievements);
                GameObject.Find("txtLoadAc").GetComponent<Text>().text = myAchievements;
            }
            else
                Debug.Log("No achievements returned");
        });
    }
    #endregion /Achievements

    #region Leaderboards
    public static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        if (Social.localUser.authenticated)
            Social.ReportScore(score, leaderboardId, success =>
            {

                if (success)
                {
                    print("Update Success");

                }
                else print("Update Score Fail");

            });
    }

    public static void ShowLeaderboards()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion Leaderboards



}
