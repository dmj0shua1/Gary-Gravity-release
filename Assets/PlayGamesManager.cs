using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.UI;

public class PlayGamesManager : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {

        if (SceneManager.GetActiveScene().name == "splash")
        {
            PlayerPrefs.SetInt("askedForGPG", 0);
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
#if UNITY_ANDROID
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();    
#elif UNITY_IPHONE
            GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
            Social.localUser.Authenticate(ProcessAuthentication);
            Debug.Log("Authentication Executed");
#endif
            if (PlayerPrefs.GetInt("askedForGPG") == 0)
            {
                SignIn();
                PlayerPrefs.SetInt("askedForGPG", 1);
            }
        }

    }

    void ProcessAuthentication (bool success){
        if (success)
        {
            Debug.Log("Authenticated, checking achievements");

            // Request loaded achievements, and register a callback for processing them
            Social.LoadAchievements(ProcessLoadedAchievements);
        }
        else
            Debug.Log("Failed to authenticate");
    }

    void ProcessLoadedAchievements(IAchievement[] achievements)
    {
         if (achievements.Length == 0)
        Debug.Log ("Error: no achievements found");
    else
        Debug.Log ("Got " + achievements.Length + " achievements");
 
    // You can also call into the functions like this
    Social.ReportProgress ("Achievement01", 100.0, (bool result) => {
        if (result)
            Debug.Log ("Successfully reported achievement progress");
        else
            Debug.Log ("Failed to report achievement");
    });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SignIn()
    {
#if UNITY_ANDROID
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

#endif

    }

    public void SignOut()
    {
#if UNITY_ANDROID
        ((PlayGamesPlatform)Social.Active).SignOut();
#endif
    }

    #region Achievements
    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
#endif
    }

    public static void ShowAchievements()
    {
        Social.ShowAchievementsUI();
        Debug.Log("ShowAchievements UI executed");
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
