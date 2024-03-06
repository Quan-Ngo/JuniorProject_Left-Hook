using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PunchingGameManager : MonoBehaviour
{
    public bool timerOn;
	public float timeLeft;
	public float timerStartTime; 
    public TextMeshProUGUI countdownText;

    [SerializeField] private GameObject boxer;
	[SerializeField] private FishTable fishTable;
	[SerializeField] private GameObject fishHPObj;
	[SerializeField] private GameObject fishHPBar;
	[SerializeField] private GameObject WinPanel;
	[SerializeField] private updateMoneyDisplay moneyDisplay;
	[SerializeField] private Animator animator;
	[SerializeField] private TextMeshProUGUI defeatedFishText; 
	[SerializeField] private Image shieldIcon;

	private int defeatedFish;

	public PlayerController playerControllerScript;
	public FishingButton fishingButtonScript;

	public GameObject mouseClicks;
	public GameObject dodgeInstructions;
	public GameObject blockedText; 


	//[SerializeField] private updateMoneyDisplay moneyDisplay;
	
	public static PunchingGameManager instance;
	
	
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}

		defeatedFish = 0;
		timerOn = false;

		//blockedText.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn == true)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
				countdownText.text = timeLeft.ToString("#"); 
            }
			
            else if (timeLeft <= 0)
            {
                Debug.Log("Time is up");
                timerOn = false;
                timeLeft = timerStartTime;

                // Send player to boxing game is no response 
                fishingButtonScript.StartFishing();
            }
        }

		defeatedFishText.text = defeatedFish.ToString(); 
		// if fish blocked attack
		// then play cortourine for fish blocked text 
    }
	
	public void startPunchingGame()
	{
		GameObject opponentFish;

		animator.SetTrigger("ActivateBoat");
        boxer.SetActive(true);
		fishHPObj.SetActive(true);
		opponentFish = Instantiate(fishTable.chooseFishToSpawn());
		boxer.GetComponent<PlayerController>().fish = opponentFish.GetComponent<FishAI>();
		opponentFish.GetComponent<FishAI>().player = boxer.GetComponent<PlayerController>();
		opponentFish.GetComponent<FishAI>().healthBar = fishHPBar.GetComponent<Image>();
		opponentFish.GetComponent<FishAI>().shieldIcon = shieldIcon;

		StartCoroutine(mouseInstructions());
	}

	public void fightComplete()
	{
		boxer.GetComponent<PlayerController>().endFight();
		fishHPObj.SetActive(false);
		WinPanel.SetActive(true);
		timerOn = true;
		moneyDisplay.updateMoney();
		animator.SetTrigger("ActivateBoat");
		defeatedFish++;
		fishTable.increaseHeat();
		//FishingMinigameManager.instance.startFishing();
	}

	IEnumerator mouseInstructions()
    {
		//turns instructions on at start of fight
		dodgeInstructions.SetActive(true);
		yield return new WaitForSecondsRealtime(4f);
		//Turns instructions off
		dodgeInstructions.SetActive(false);

		//turns instructions on at start of fight
		mouseClicks.SetActive(true);
		yield return new WaitForSecondsRealtime(4f);
		//Turns instructions off
		mouseClicks.SetActive(false);
	}

	


}
