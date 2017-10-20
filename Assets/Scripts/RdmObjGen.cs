using UnityEngine;
using System.Collections;

public class RdmObjGen : MonoBehaviour
{


    scoreCounter scoreCounterScript;
    GameObject objScoreCounter;
    public Transform[] SpawnPoints;
    public float spawnTimeObs, spawnTimeCol, spawnTimeCoins;
    //public GameObject Obstacles;
    public GameObject[] Collectibles;
    public ObjectPooler[] Obstacles;
    public ObjectPooler objCoinPool;
    public bool stopBoostNow, stopShieldNow, stopAttackNow;
    // Use this for initialization
    private IEnumerator coroutine;
    GameObject playerObj;
    int prevCol, curCol, prevObs, curObs, prevIndex, curIndex, prevIndex1, curIndex1, prevIndex2, curIndex2;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        if (PlayerPrefs.GetString("chosenChar") == "Shopper_girl")
        {
            Collectibles[0] = (GameObject)Resources.Load("Collectibles/Collectcard", typeof(GameObject));
            Collectibles[1] = (GameObject)Resources.Load("Collectibles/Collectbags", typeof(GameObject));
            Collectibles[3] = (GameObject)Resources.Load("Collectibles/Collectshoe", typeof(GameObject));
        }
        else if (PlayerPrefs.GetString("chosenChar") == "Fireman")
        {
            Collectibles[0] = (GameObject)Resources.Load("Collectibles/Collecthose", typeof(GameObject));
            Collectibles[1] = (GameObject)Resources.Load("Collectibles/Collectext", typeof(GameObject));
            Collectibles[3] = (GameObject)Resources.Load("Collectibles/Collectaxe", typeof(GameObject));
        }
        objScoreCounter = GameObject.Find("subScoreCounter");
        scoreCounterScript = objScoreCounter.GetComponent<scoreCounter>();

        coroutine = WaitAndUpdate(0.1f);
        StartCoroutine(coroutine);

        prevCol = curCol = Random.Range(0, Collectibles.Length);
        prevObs = curObs = Random.Range(0, Obstacles.Length);
        prevIndex = curIndex = Random.Range(0, SpawnPoints.Length);
        prevIndex1 = curIndex1 = Random.Range(0, SpawnPoints.Length);
        prevIndex2 = curIndex2 = Random.Range(0, SpawnPoints.Length);
    }

    private IEnumerator WaitAndUpdate(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            //  checkNewSpeed();

            InvokeRepeating("SpawnCollectibles", spawnTimeCol, spawnTimeCol);
            InvokeRepeating("SpawnObstacles", spawnTimeObs, spawnTimeObs);
            InvokeRepeating("SpawnCoins", spawnTimeCoins, spawnTimeCoins);

            if (stopBoostNow) Destroy(GameObject.FindGameObjectWithTag("CollectiblesBoost"));
            if (stopShieldNow) Destroy(GameObject.FindGameObjectWithTag("CollectiblesShield"));
            if (stopAttackNow) Destroy(GameObject.FindGameObjectWithTag("CollectiblesAttack"));
        }
    }

    /*  void checkNewSpeed()
      {
          if (gameObject.tag != "Foreground")
          {
              spawnTimeObs = scoreCounterScript.MoveSpeed ;
              spawnTimeCol = scoreCounterScript.MoveSpeed + 2.5f;
          }
      }*/
    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacles()
    {
        // Random.InitState(System.DateTime.Now.Millisecond);


        while (prevObs == curObs)
        {
            curObs = Random.Range(0, Obstacles.Length);
        }
        prevObs = curObs;

        while (prevIndex == curIndex)
        {
            curIndex = Random.Range(0, SpawnPoints.Length);
        }
        prevIndex = curIndex;

      //  Instantiate(Obstacles[curObs], SpawnPoints[curIndex].position, SpawnPoints[curIndex].rotation);
        GameObject objPooled = Obstacles[curObs].GetPooledObject();
        objPooled.transform.position = SpawnPoints[curIndex].position;
        objPooled.transform.rotation = SpawnPoints[curIndex].rotation;
        objPooled.SetActive(true);
        CancelInvoke("SpawnObstacles");
        //    spawnTimeObs = Random.Range(minSpawnTimeObs, maxSpawnTimeObs);

    }

    void SpawnCollectibles()
    {
        //Random.InitState(System.DateTime.Now.Millisecond);

        while (prevCol == curCol)
        {
            curCol = Random.Range(0, Collectibles.Length);
        }

        prevCol = curCol;

        while (prevIndex1 == curIndex1)
        {
            curIndex1 = Random.Range(0, SpawnPoints.Length);
        }
        prevIndex1 = curIndex1;

        Instantiate(Collectibles[curCol], SpawnPoints[curIndex1].position, SpawnPoints[curIndex1].rotation);
      /*  Collectibles[curCol].transform.position = SpawnPoints[curIndex1].position;
        Collectibles[curCol].transform.rotation = SpawnPoints[curIndex1].rotation;
        Collectibles[curCol].SetActive(true);*/
        CancelInvoke("SpawnCollectibles");

        //      spawnTimeCol = Random.Range(minSpawnTimeCol, maxSpawnTimeCol);

    }

    void SpawnCoins()
    {
        while (prevIndex2 == curIndex2)
        {
            curIndex2 = Random.Range(0, SpawnPoints.Length);
        }
        prevIndex2 = curIndex2;
       // Instantiate(objCoin, SpawnPoints[curIndex2].position, SpawnPoints[curIndex2].rotation);
        GameObject objPooled = objCoinPool.GetPooledObject();
        objPooled.transform.position = SpawnPoints[curIndex2].position;
        objPooled.transform.rotation = SpawnPoints[curIndex2].rotation;
        objPooled.SetActive(true);
        CancelInvoke("SpawnCoins");
    }


}
