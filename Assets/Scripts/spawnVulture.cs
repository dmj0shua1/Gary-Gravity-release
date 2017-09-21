using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnVulture : MonoBehaviour {

    public GameObject objVulture;
    GameObject spawnPoint, wayPoint,wp1,wp2,sp1,sp2;
    public AudioSource sfxVulture;
    private IEnumerator coroutine;
    public float spawnRateMin, spawnRateMax;
    goToWaypoint gotoWaypointScript;
    SwipeMovement swipemovementScript;
    public GameObject[] spawnPoints;
    public GameObject[] wayPoints;
	// Use this for initialization
	void Start () {
        coroutine = WaitAndUpdate(1.0f);
        StartCoroutine(coroutine);
        gotoWaypointScript = objVulture.GetComponent<goToWaypoint>();
        swipemovementScript = Camera.main.gameObject.GetComponent<SwipeMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator WaitAndUpdate(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));
            
            string currentLane = swipemovementScript.currentLane;

            int spawnIndex = Random.Range(0, spawnPoints.Length);
            int waypointIndex = Random.Range(0, wayPoints.Length);

            //see where player is
            if (currentLane == "Lane1")
            {
                spawnPoint = spawnPoints[1];
                wayPoint = wayPoints[1];
                
            }
            else if (currentLane == "Lane3")
            {
                spawnPoint = spawnPoints[0];
                wayPoint = wayPoints[1];
            }
            else
            {
               
                spawnPoint = spawnPoints[spawnIndex].gameObject;
                wayPoint = wayPoints[waypointIndex].gameObject;
            }

            sfxVulture.Play();
            //blink visual here
            Invoke("instantiateVulture", 1.8f);
        }
            
    }

    void instantiateVulture(){
        gotoWaypointScript.target = wayPoint;
        Instantiate(objVulture, spawnPoint.transform.position, spawnPoint.transform.rotation);
        
    }


}
