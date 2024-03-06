using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public abstract class FishAI : MonoBehaviour
{
	protected enum BlockStates {ALL, NONE, INVULN};
	
	[SerializeField] protected int maxHealth;
	[SerializeField] protected float timeBetweenAttacks;
	[SerializeField] protected Animator animator;
	[SerializeField] protected BlockStates blockState;
	[SerializeField] protected bool stunLock;
	[SerializeField] protected float stunLockDuration;
	[SerializeField] protected int stunThreshhold;
	[SerializeField] protected int currentStunVal;
	[SerializeField] protected int moneyValue;
	[SerializeField] protected int damage;
	[SerializeField] protected GameObject dizzyStars;
	[SerializeField] protected Sprite[] shieldSprites;
	
	public int currentHealth;

	public Image healthBar;
	public Image shieldIcon;
	public PlayerController player;
	public AudioSource punchSound;
	public AudioSource blockSound;
	public AudioSource quickPunchSound;

	private PunchingGameManager punchGameScript; 


	
    // Start is called before the first frame update
    protected virtual void Start()
    {
		currentHealth = maxHealth;
		currentStunVal = 0;
		healthBar.fillAmount = (float) currentHealth / (float) maxHealth;
		stunLock = false;
		punchGameScript = GetComponent<PunchingGameManager>();
		//punchGameScript.BlockTexted();
		dizzyStars.SetActive(false);
		displayShieldIcon();
		StartCoroutine(neutralState());
    }

    protected IEnumerator neutralState()
	{
		if (maxHealth > 0){
			yield return new WaitForSeconds(System.Math.Max(timeBetweenAttacks + Random.Range(-0.5f, 0.5f), 0));
			attack();
            
        }
	}
	
	public virtual void recover()
	{
		if (!stunLock)
		{
			animator.SetTrigger("Neutral");
			blockState = BlockStates.ALL;
			StartCoroutine(neutralState());
		}
	}
	
    // Returns true if punch deals damage.
	public virtual bool getHit(int damage, Positions.Position directionOfPunch)
	{
		if (stunLock)
		{
			quickPunchSound.Play();
			takeDamage(damage, directionOfPunch);
            return true;
		}
		else
		{
			StopAllCoroutines();
			switch (blockState)
			{
				case BlockStates.ALL:
					animator.SetTrigger("Block");
					blockSound.Play();
					//punchGameScript.BlockTexted();
					break;
				case BlockStates.NONE:
					takeDamage(damage, directionOfPunch);
                    return true;
				case BlockStates.INVULN:
					break;
			}
            return false;
		}
	}
	
	protected IEnumerator stunLockCountDown()
	{
		stunLock = true;
		dizzyStars.SetActive(true);
		currentStunVal = 0;
		yield return new WaitForSeconds(stunLockDuration);
		stunLock = false;
		dizzyStars.SetActive(false);
		displayShieldIcon();
		recover();
	}
	
	protected virtual void takeDamage(int damage, Positions.Position directionOfPunch)
	{
		string animationTriggerDirection;
		
		currentHealth -= damage;
		healthBar.fillAmount = (float) currentHealth / (float) maxHealth;
		punchSound.Play();
		
		if (currentHealth <= 0)
		{
			stunLock = false;
			StopAllCoroutines();
			blockState = BlockStates.INVULN;
			//player.fishDefeated();
			playDefeatAnimation();
		}
		else 
		{
			if (directionOfPunch == Positions.Position.LEFT)
			{
				animationTriggerDirection = "Left";
			}
			else
			{
				animationTriggerDirection = "Right";
			}
			
			animator.SetTrigger("GetHitFrom" + animationTriggerDirection);
			checkStun();
		}
	}
	
	protected virtual void checkStun()
	{
		if (!stunLock)
		{
			currentStunVal++;
			displayShieldIcon();
		}
		
		if (currentStunVal == stunThreshhold)
		{
			StartCoroutine(stunLockCountDown());
		}
	}
	
	protected void displayShieldIcon()
	{
		shieldIcon.sprite = shieldSprites[currentStunVal];
		shieldIcon.gameObject.GetComponent<Animator>().SetTrigger("Shake");
	}
	
	protected virtual void playDefeatAnimation()
	{
		dizzyStars.SetActive(false);
		animator.SetTrigger("Dies");
	}
	
	public void Death()
	{
		moneyManager.instance.addMoney(moneyValue);
		PunchingGameManager.instance.fightComplete();
		Destroy(gameObject);
	}
	
	public void hitPlayer(Positions.Position hitPos)
	{
		player.getHit(hitPos, damage);
	}
	
	public bool IsStunned()
	{
		return stunLock;
	}
	
	public void blockStateToNone()
	{
		blockState = BlockStates.NONE;
	}
	
	protected abstract void attack();



}
