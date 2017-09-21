using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pauseScale : MonoBehaviour {
    public bool isBoosting = false,isGameOver = false;
    public Text resumeTimer;
	Camera cam;
	//private Blur Blurscript;
	// Use this for initialization
    RdmObjGen rdmobj;
    public GameObject pauseMenu, UI;
    private IEnumerator coroutine;
	void Start () {
		cam = Camera.main;
        rdmobj = cam.gameObject.GetComponent<RdmObjGen>();
      //  Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !isBoosting && !isGameOver && Time.timeScale == 1)
        {
            Pause();
        }
	}
	public void Pause(){

		cam.SendMessage ("TurnBlurOn");
         
        Destroy(GameObject.Find("Logo"));
        Destroy(GameObject.Find("FaderOut"));
        pauseMenu.SetActive(true);
        UI.SetActive(false);
        Time.timeScale = 0f;

	}
 
        
    void OnApplicationPause()
    {
        if (!isBoosting && !isGameOver) Pause();
    }

    public void startResume()
    {
        StartCoroutine(WaitToGetReady());
        cam.SendMessage("TurnBlurOff");
        pauseMenu.SetActive(false);
       
    }

    IEnumerator WaitToGetReady()
    {
        resumeTimer.gameObject.SetActive(true);
        resumeTimer.text = "" + 3;
        yield return WaitToResumeGame();

        resumeTimer.text = "" + 2;
        yield return WaitToResumeGame();

        resumeTimer.text = "" + 1;
        yield return WaitToResumeGame();

        Time.timeScale = 1f;
        UI.SetActive(true);
        resumeTimer.gameObject.SetActive(false);
    }

    IEnumerator WaitToResumeGame()
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + 1f)
        {
            yield return 0;
        }
    }
    private IEnumerator wait()
    {
       

        float pauseEndTime = Time.realtimeSinceStartup+3f;

        while (Time.realtimeSinceStartup < pauseEndTime)
        {
          
            yield return 0;
        }
       
        Time.timeScale = 1;
      
        UI.SetActive(true);
       
    }

    

    


}
