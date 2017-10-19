using UnityEngine;
using System.Collections;

public class FgGen : MonoBehaviour {


	public Transform SpawnPoints;
	public float spawnTime = 1.5f;
    public ObjectPooler bdForeground;
//	public GameObject bdForeground;
 	//public GameObject[] Obstacles;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnForeground", 2.8f,spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnForeground() {
		//Instantiate(bdForeground, SpawnPoints.position, SpawnPoints.rotation);
        GameObject objectPooled = bdForeground.GetPooledObject();
        objectPooled.transform.position = SpawnPoints.transform.position;
        objectPooled.transform.rotation = SpawnPoints.transform.rotation;
        objectPooled.SetActive(true);
	}
}
