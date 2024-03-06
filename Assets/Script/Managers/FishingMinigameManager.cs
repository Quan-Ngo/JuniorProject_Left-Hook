using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class FishingMinigameManager : MonoBehaviour
{
	private enum ButtonToMash {LEFT, RIGHT, DOWN}
	
	[SerializeField] private float buttonMashChangeRate;
	[SerializeField] private ButtonToMash buttonToMash;
	[SerializeField] private float defaultReelDistance;
	[SerializeField] private float fishResist;
	[SerializeField] private float reelStrength;
	[SerializeField] private float greenBonus;
	[SerializeField] private float redPenalty;
	[SerializeField] private float fishingBarRate;
	[SerializeField] private GameObject whiteBall;

	[SerializeField] private GameObject fishingBar;
	[SerializeField] private Text mashButtonDisplay;
	[SerializeField] private TextMeshProUGUI distanceDisplay;
	[SerializeField] private GameObject fishDistanceUiObject; 
	[SerializeField] private float lineStrength;
	[SerializeField] private GameObject WinPanel;
	[SerializeField] private GameObject angler;
	[SerializeField] AudioSource barPopupWarningSound;

	[SerializeField] private PlayerStats stat;

	
	public bool inFishingGame;
	private float reelDistance;
	public AudioSource reelSound;
	
	public static FishingMinigameManager instance;
    public GameObject splashEffect;
	public PunchingGameManager punchGameScript;

	//Button Mash Game Object for Instructions
	public GameObject mashButtonDown;
	public GameObject mashButtonLeft;
	public GameObject mashButtonRight;
	//public GameObject mouseBarInstruction;

	//Intro text game objects
	public GameObject introTestREADY;
	public GameObject introTestSET;
	public GameObject introTestFISH;

	public GameObject lineBreakIndicator; 
	
	
    // Start is called before the first frame update
    void Start()
    {
		// When Game starts, pause for three seconds before beginning to show intro text
		StartCoroutine(ShowText()); 
		
		if (instance != null)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}

		reelDistance = defaultReelDistance;
		lineStrength = stat.getLineStrength();
		StartCoroutine(waitForNextBite());
		inFishingGame = false;

		lineBreakIndicator.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
		if (inFishingGame && (((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && buttonToMash == ButtonToMash.LEFT) || ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && buttonToMash == ButtonToMash.RIGHT) || (((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && buttonToMash == ButtonToMash.DOWN))))
        {
			reelDistance -= reelStrength;
			checkReelDistance();
        }
		distanceDisplay.text = reelDistance.ToString() + "m";

		float newY = Mathf.Lerp(-600, -100, reelDistance / 150.0f);
		whiteBall.transform.localPosition = new Vector3(whiteBall.transform.localPosition.x, newY, whiteBall.transform.localPosition.z);


	}

	private void checkReelDistance()
	{

        fishDistanceUiObject.SetActive(true);

        if (reelDistance <= 0)
		{
			fishGameSuccess();
            
        }
		reelSound.pitch = 2f - (reelDistance / 100); //Modulates the reeling sound so it becomes higher-pitched as the fish is closer.
	}


	
	private void fishGameSuccess()
	{
		StopAllCoroutines();
		
		if (inFishingGame){
			reelSound.enabled = false;
			mashButtonDisplay.enabled = false;
			distanceDisplay.enabled = false;
			inFishingGame = false;
            fishingBar.SetActive(false);
            splashEffect.SetActive(false);
            angler.GetComponent<AnglerController>().endFish();
			fishDistanceUiObject.SetActive(false);

			//button display for mashing
			mashButtonDown.SetActive(false);
			mashButtonLeft.SetActive(false);
			mashButtonRight.SetActive(false);

			/*boxer.SetActive(true);
			fish.SetActive(true);
			HPBar.SetActive(true);*/

			//gameObject.SetActive(false);

			PunchingGameManager.instance.startPunchingGame();
		}
	}
	
	public void timingResult(string color)
	{
		fishingBar.SetActive(false);
		switch (color)
		{
			case "Green":
				reelDistance -= greenBonus;
				checkReelDistance();
				break;
			case "Red":
				reelDistance += redPenalty;
				checkReelDistance();
				break;
		}
	}
	
	private void fishEscape()
	{
		StopAllCoroutines();
        lineBreakIndicator.SetActive(true);
        reelSound.enabled = false;
        splashEffect.SetActive(false);
		mashButtonDisplay.enabled = false;
		mashButtonDown.SetActive(false);
		mashButtonLeft.SetActive(false);
		mashButtonRight.SetActive(false);
		distanceDisplay.enabled = false;
        fishDistanceUiObject.SetActive(false);
        StartCoroutine(waitForNextBite());
	}
	
	IEnumerator waitForNextBite()
	{
		yield return new WaitForSeconds(2);
		
		lineBreakIndicator.SetActive(false);

        angler.GetComponent<AnglerController>().waitForBite();
		
		yield return new WaitForSeconds(Random.Range(5, 8));
		
		angler.GetComponent<AnglerController>().startReeling();
		reelSound.enabled = true;
        splashEffect.SetActive(true);
        reelDistance = defaultReelDistance;
		mashButtonDisplay.enabled = true;
		distanceDisplay.enabled = true;
		inFishingGame = true;
        StartCoroutine(buttonMashChangeTimer());
		StartCoroutine(fishResistCalc());
		StartCoroutine(fishingBarPopUp());
		StartCoroutine(fishingTimeLimit());
	}
		
	
	IEnumerator buttonMashChangeTimer()
	{
		switch (Random.Range(0,3))
		{
			case 0:
				buttonToMash = ButtonToMash.LEFT;
				mashButtonDown.SetActive(false);
				mashButtonRight.SetActive(false);
				mashButtonLeft.SetActive(true);
				mashButtonDisplay.text = "";
				break;
			case 1:
				buttonToMash = ButtonToMash.RIGHT;
				mashButtonDown.SetActive(false);
				mashButtonLeft.SetActive(false);
				mashButtonRight.SetActive(true);
				mashButtonDisplay.text = "";
				break;
			case 2:
				buttonToMash = ButtonToMash.DOWN;
				mashButtonLeft.SetActive(false);
				mashButtonRight.SetActive(false);
				mashButtonDown.SetActive(true);
				mashButtonDisplay.text = "";
				break;
		}
			
		while (true)
		{
			yield return new WaitForSeconds(buttonMashChangeRate);
			
			switch (Random.Range(0,3))
			{
				case 0:
					buttonToMash = ButtonToMash.LEFT;
					mashButtonDown.SetActive(false);
					mashButtonRight.SetActive(false);
					mashButtonLeft.SetActive(true);
					mashButtonDisplay.text = "";
					break;
				case 1:
					buttonToMash = ButtonToMash.RIGHT;
					mashButtonDown.SetActive(false);
					mashButtonLeft.SetActive(false);
					mashButtonRight.SetActive(true);
					mashButtonDisplay.text = "";
					break;
				case 2:
					buttonToMash = ButtonToMash.DOWN;
					mashButtonLeft.SetActive(false);
					mashButtonRight.SetActive(false);
					mashButtonDown.SetActive(true);
					mashButtonDisplay.text = "";
					break;
			}
		}
	}
	
	IEnumerator fishResistCalc()
	{
		while (true)
		{
			yield return new WaitForSeconds((1/60));
			reelDistance += fishResist;
		}
	}
	
	IEnumerator fishingBarPopUp()
	{
		while (true)
		{
			yield return new WaitForSeconds(fishingBarRate - 1f);
			barPopupWarningSound.Play();
			yield return new WaitForSeconds(1f);
			/*//turns instructions on at start of fight
			mouseBarInstruction.SetActive(true);*/
			//yield return new WaitForSecondsRealtime(.5f);
			//Turns instructions off
			fishingBar.SetActive(true);
			/*yield return new WaitForSecondsRealtime(1.5f);
			mouseBarInstruction.SetActive(false);*/
		}
	}

	IEnumerator fishingTimeLimit()
	{
		yield return new WaitForSeconds(lineStrength);
		fishEscape();
	}
	
	public void startFishing()
	{
        splashEffect.SetActive(false);
		WinPanel.SetActive(false);
		angler.SetActive(true);
		punchGameScript.timerOn = false;
        fishDistanceUiObject.SetActive(false);
        StartCoroutine(waitForNextBite());
	}

    public int getButtonToMashAsInt()
    {
        if (buttonToMash == ButtonToMash.LEFT)
            return -1;
        else if (buttonToMash == ButtonToMash.RIGHT)
            return 1;
        else
            return 0;
    }


	IEnumerator ShowText()
	{
		Time.timeScale = 0f; 
		
		
		introTestREADY.SetActive(true); 
		yield return new WaitForSecondsRealtime(1f);
        //Turn off first text
        introTestREADY.SetActive(false);
        // Show second text
		introTestSET.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
		//turn off second text
		introTestSET.SetActive(false);
		// Show third text
		introTestFISH.SetActive(true);
		yield return new WaitForSecondsRealtime(1f);
		//turn of third text
		introTestFISH.SetActive(false);
		Time.timeScale = 1f; 
	}

}
