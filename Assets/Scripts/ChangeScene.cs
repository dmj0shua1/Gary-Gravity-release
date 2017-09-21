using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour {


	public string GoTo;
	public float waitTime;
	public GameObject fader,buttons;
	Animator anim,slide; 
    
	void Start () {
        if (!PlayerPrefs.HasKey("chosenStage")) PlayerPrefs.SetString("chosenStage","Game");
	}
	void Update () {
	
	}
	public void changeScene(){
        anim = fader.GetComponent<Animator>(); 
		slide = buttons.GetComponent<Animator> ();
        anim.SetBool("FadeIn", true);
		slide.SetBool ("SlideOut", true);
        Time.timeScale = 1f;
		Invoke ("GoToScene", waitTime);
	}
	void GoToScene(){
        PlayerPrefs.GetString("chosenStage");
        SceneManager.LoadScene(PlayerPrefs.GetString("chosenStage"));
	}


}
