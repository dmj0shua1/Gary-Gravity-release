using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class SwipeMovement : MonoBehaviour {

    [Header("Powers")]
    public Slider healthBarSlider;
    public Slider shieldbarSlider;
    public Slider boostbarSlider;
    public Slider attackbarSlider;
    [Space]
    public float shieldCap;
    public float boostCap;
    public float attackCap;
    [Space]
    [Header("Swiping")]
    public int swipeCap;
	public string currentLane;
	public GameObject target1,target2,target3,protag,charMesh,smokeTrail,character;
	public Vector3 targetPosition,currentPosition;
    public float upSpeed, sideSpeed, angleLeft, angleRight;
    public static bool moveUp;
    public bool garySwiped = true;
    [Space]
    public bool isGameOver;
    SpriteRenderer tornadoSpriteLeft, tornadoSpriteRight;
    GameObject garySwipeLeftProp, garySwipeRightProp;
    Animation anim;
    AudioSource asSfxSwipe;
    bool gameStarted;
    Animator foamLeft, foamRight;
  
    boostProp PropScript;
  //  Text txtgarySwiped;
	// Use this for initialization
	void Start () {
        isGameOver = false;
        boostbarSlider.maxValue = boostCap;
        if (PlayerPrefs.HasKey("endTimeboost")) boostbarSlider.value = boostbarSlider.maxValue;
        shieldbarSlider.maxValue = shieldCap;
        if (PlayerPrefs.HasKey("endTimeshield")) shieldbarSlider.value = shieldbarSlider.maxValue;
        attackbarSlider.maxValue = attackCap;
        if (PlayerPrefs.HasKey("endTimeattack")) attackbarSlider.value = attackbarSlider.maxValue;
       // swipeCapManager();
        swipeCap = PlayerPrefs.GetInt("swipeCap");
        healthBarSlider.maxValue = swipeCap;
        healthBarSlider.value = swipeCap;

		currentLane = "Lane2";
		targetPosition = target2.transform.position;
        moveUp = true;

        Invoke("gameStartedOn",1.6f);

     //   txtgarySwiped = GameObject.Find("garySwiped").GetComponent<Text>(); 

	}

   /* void swipeCapManager()
    {
        swipeCap = PlayerPrefs.GetFloat("swipeCap");
        
    }*/
	
	// Update is called once per frame
   

    public void garyGSwiped()
    {
        garySwiped = true;
    }

    void Update()
    {
   //     txtgarySwiped.text = garySwiped.ToString();
    }

    void playSwipeRightAnimations()
    {
        if (character.name == "Ruth2")
        {
            anim.Play("shopper_moveright_anim");
            tornadoSpriteRight.enabled = true;
            asSfxSwipe = GameObject.Find("sfxSwipeShopper_girl").GetComponent<AudioSource>();
            garySwiped = false;
        }
        else if (character.name == "Char")
        {
            anim.Play("gary_swipe_right_anim");
            asSfxSwipe = GameObject.Find("sfxSwipeFireman").GetComponent<AudioSource>();
        /*   garySwipeRightProp.SetActive(false);
            
                garySwipeRightProp.SetActive(true);*/
            PropScript.redisableGaryPropRight();
                garySwiped = false;
           
            
        }

        if (PlayerPrefs.GetInt("Sfx") == 1)  asSfxSwipe.Play();

    }

    void playSwipeLeftAnimations()
    {
        if (character.name == "Ruth2")
        {
            anim.Play("shopper_moveleft_anim");
            tornadoSpriteLeft.enabled = true;
            asSfxSwipe = GameObject.Find("sfxSwipeShopper_girl").GetComponent<AudioSource>();
            garySwiped = false;
        }
        else if (character.name == "Char")
        {
            anim.Play("gary_swipe_left_anim");
            asSfxSwipe = GameObject.Find("sfxSwipeFireman").GetComponent<AudioSource>();
          /* garySwipeLeftProp.SetActive(false);
                garySwipeLeftProp.SetActive(true);*/
            PropScript.redisableGaryPropLeft();
                garySwiped = false;
           
        }
        if (PlayerPrefs.GetInt("Sfx") == 1) asSfxSwipe.Play();
    }

  

	void FixedUpdate() {
       
        character = GameObject.FindGameObjectWithTag("character");
        anim = character.GetComponent<Animation>();
        protag = GameObject.FindGameObjectWithTag("Player");
       

        if (character.name == "Ruth2")
        {
            GameObject tornadoSide = GameObject.Find("bag tornado side moveLeft");
            GameObject tornadoSide2 = GameObject.Find("bag tornado side moveRight");
            tornadoSpriteLeft = tornadoSide.GetComponent<SpriteRenderer>();
            tornadoSpriteRight = tornadoSide2.GetComponent<SpriteRenderer>();
        }
        else if (character.name == "Char")
        {
            PropScript = protag.GetComponent<boostProp>();
            garySwipeLeftProp = PropScript.swipeLeftProp;
            garySwipeRightProp = PropScript.swipeRightProp;
        }
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			// Get movement of the finger since last frame
            
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			// Move object across XY plane

            if (touchDeltaPosition.x > 1 && swipeCap >= 1 && Time.timeScale == 1f && protag.GetComponent<Rigidbody>().velocity == Vector3.zero)
			{


               if (character.name=="Ruth2") anim["shopper_moveright_anim"].speed = 2.0f;
				//SwipeRight
               if (currentLane == "Lane2" && garySwiped == false)
                {
                   targetPosition = target3.transform.position;
                   playSwipeRightAnimations();
				}
               else if (currentLane == "Lane1" && garySwiped == false)
                {
			       targetPosition = target2.transform.position;
                   playSwipeRightAnimations();
				}

            }
            else if (touchDeltaPosition.x < -1 && swipeCap >= 1 && Time.timeScale == 1f && protag.GetComponent<Rigidbody>().velocity == Vector3.zero)
			{
                if (character.name == "Ruth2") anim["shopper_moveleft_anim"].speed = 2.0f;
				//SwipeLeft
                if (currentLane == "Lane2" && garySwiped == false)
                    {
				    	targetPosition = target1.transform.position;
                        playSwipeLeftAnimations();
				    }
                else if (currentLane == "Lane3" && garySwiped == false)
                    {
					targetPosition = target2.transform.position;
                    playSwipeLeftAnimations();
               
				    }

            }
            else
            {
                AudioSource insufficientSwipe = GameObject.Find("sfxInsufficientSwipe").GetComponent<AudioSource>();
                if (PlayerPrefs.GetInt("Sfx") == 1)  insufficientSwipe.Play();
            }
            if (character.name == "Ruth2")  Invoke("disableTornadoSprite", 0.5f);
           
		}

        if (isGameOver == false) MovePlayer();

	}

    void disableTornadoSprite(){
        tornadoSpriteLeft.enabled = false;
        tornadoSpriteRight.enabled = false;
    }
	private void MoveTowardsTarget() {
		//the speed, in units per second, we want to move towards the target

		//move towards the center of the world (or where ever you like)
		//targetPosition = maintarget.transform.position;

		currentPosition = protag.transform.position;
        
		//first, check to see if we're close enough to the target
		if(Vector3.Distance(currentPosition, targetPosition) > .1f) { 
			Vector3 directionOfTravel = targetPosition - currentPosition;
			//now normalize the direction, since we only want the direction information
			directionOfTravel.Normalize();
			//scale the movement on each axis by the directionOfTravel vector components

			
			protag.transform.Translate( 
				(directionOfTravel.x * sideSpeed * Time.deltaTime),
                (directionOfTravel.y * sideSpeed * Time.deltaTime),
                (directionOfTravel.z * sideSpeed * Time.deltaTime),
				Space.World);
		}
	}

    private void MovePlayer()
    {
        try
        {
            protag.transform.position = Vector3.Lerp(protag.transform.position, targetPosition, Time.deltaTime * upSpeed);
        }
        catch (Exception e)
        {
            //do nothing
        }
    }

    public void setSwipeCap()
    {
        if (gameStarted)
        {
            --swipeCap;
            healthBarSlider.value -= 1f;
        }
    }

    void gameStartedOn()
    {
        gameStarted = true;
    }
 

  
}
