using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
	private enum LastPunchDir {LEFT, RIGHT}
	private bool animationLock;
	private bool gotHitAnimationLock;
	private LastPunchDir lastPunchDir; // Added this line to track the last punch direction
	private bool fishStunned;
	
	[SerializeField] private int currentHealth; 
	[SerializeField] private Positions.Position dodgePosition;
	[SerializeField] private int maxHealth;
	

	
    public Animator anim;
	public FishAI fish;
	public int damage;
	public GameObject losePanel;
	public AudioSource owieSound;
	public AudioSource dodgeSound;
	public Image playerHealthBar;

    public GameObject hitFlashEffect;
    public Transform leftFlashPos;
    public Transform rightFlashPos;
    private Transform currentFlashPos;
	public PlayerStats stat;

	//public GameObject player; 

	// Start is called before the first frame update
	void Start()
    {
		maxHealth = stat.getMaxHealth();
		currentHealth = maxHealth;
		dodgePosition = Positions.Position.MID;
		damage = stat.getPunchDamage();
		playerHealthBar.fillAmount = currentHealth / maxHealth;
		animationLock = false;
		gotHitAnimationLock = false;
		//player.SetActive(true); 
	}

    // Update is called once per frame
    void Update()
    {	
		if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Z)) && !animationLock && !gotHitAnimationLock)
		{
			resetDodgePosition();
            currentFlashPos = leftFlashPos; // Sets the position of the flash to the left
			
			if (fish.IsStunned() && lastPunchDir == LastPunchDir.RIGHT)
			{
				playAnimation("Quick Left Punch");
				endAnimationLock();
			}
			else
			{
				playAnimation("Left Punch");
			}
			lastPunchDir = LastPunchDir.LEFT;
		}
		else if ((Input.GetButtonDown("Fire2")  || Input.GetKeyDown(KeyCode.X)) && !animationLock && !gotHitAnimationLock)
		{
			resetDodgePosition();
			currentFlashPos = rightFlashPos; // Sets the position of the flash to the right
			
			if (fish.IsStunned() && lastPunchDir == LastPunchDir.LEFT)
			{
				playAnimation("Quick Right Punch");
				endAnimationLock();
			}
			else
			{
				playAnimation("Right Punch");
			}
			lastPunchDir = LastPunchDir.RIGHT;
		}
		
		else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !animationLock && !gotHitAnimationLock)
		{
			dodgePosition = Positions.Position.LEFT;
			playAnimation("Left Dodge");
			dodgeSound.Play();
		}
		else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !animationLock && !gotHitAnimationLock)
		{
			dodgePosition = Positions.Position.RIGHT;
			playAnimation("Right Dodge");
			dodgeSound.Play();
		}
    }
	
	public void hitFish(Positions.Position punchDirection)
	{
		bool successfulHit;

		successfulHit = fish.getHit(damage, punchDirection);
        if (successfulHit)
        {
            GameObject newFlash = Instantiate(hitFlashEffect, currentFlashPos.position, currentFlashPos.rotation);
        }
	}
	
	private void playAnimation(string animation)
	{
		animationLock = true;
		anim.SetTrigger(animation);
	}
	
	public void endAnimationLock()
	{
		animationLock = false;
		gotHitAnimationLock = false;
	}
	
	public void resetDodgePosition()
	{
		dodgePosition = Positions.Position.MID;
	}
	
	public void getHit(Positions.Position hitPos, int damage)
	{	
		if (hitPos == dodgePosition)
		{
			takeDamage(damage);
		}
		else
		{
			animationLock = false;
		}
	}
	
	public void selfHurt(int damage)
	{
		takeDamage(damage);
	}
	
	private void takeDamage(int damage)
	{
		//int currentHealth = stat.getPlayerHealth();
		currentHealth -= damage;
		//stat.setPlayerHealth(currentHealth);
		gotHitAnimationLock = true;
		playAnimation("GetHit");
		owieSound.Play();
		playerHealthBar.fillAmount = (float) currentHealth / (float) stat.getMaxHealth();

		if (currentHealth <= 0)
		{
			losePanel.SetActive(true);
			gameObject.SetActive(false); 
			
		}
	}
	
	public void endFight()
	{
		endAnimationLock();
		resetDodgePosition();
		gameObject.SetActive(false);
		if (stat.checkFishVitaminSupplement())
		{
			currentHealth = System.Math.Min(maxHealth, currentHealth + stat.getVitaminRegenAmount());
			playerHealthBar.fillAmount = (float) currentHealth / (float) stat.getMaxHealth();
		}
	}
}
