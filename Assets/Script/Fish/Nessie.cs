using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nessie : FishAI
{
	[SerializeField] private GameObject nessieTailObject;
	[SerializeField] private int minHeadLoweredAttacks;
	[SerializeField] private int maxHeadLoweredAttacks;
	[SerializeField] private int minHeadRaisedAttacks;
	[SerializeField] private int maxHeadRaisedAttacks;
	[SerializeField] private Positions.Position thrashBiteDirTracker;
	
	private int headLoweredAttacksLeft;
	private int headRaisedAttacksLeft;
	private bool headLowered;
	private Animator nessieTail;

	public AudioSource nessieGroan;
	public AudioSource nessieGrunt;
	
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
        headLowered = true;
		nessieGroan.Play();
		nessieTail = nessieTailObject.GetComponent<Animator>();
		blockState = BlockStates.INVULN;
		headLoweredAttacksLeft = Random.Range(minHeadLoweredAttacks, maxHeadLoweredAttacks + 1);
    }
	
	public override void recover()
	{
		nessieGroan.Play();
		if (!stunLock)
		{
			switch (headLowered)
			{
				case true:
					blockState = BlockStates.INVULN;
					animator.SetTrigger("HeadLoweredNeutral");
					nessieTail.SetTrigger("HeadLoweredNeutral");
					break;
				case false:
					blockState = BlockStates.INVULN;
					animator.SetTrigger("HeadRaisedNeutral");
					break;
			}
			StartCoroutine(neutralState());
		}
	}

    protected override void attack()
	{
		if (headLowered)
		{
			chooseHeadLoweredAttack();
		}
		else
		{
			chooseHeadRaisedAttack();
		}
	}
	
	private void chooseHeadLoweredAttack()
	{
		blockState = BlockStates.INVULN;
		if (headLoweredAttacksLeft > 0)
		{
			switch (Random.Range(0, 2))
			{
				case 0:
					animator.SetTrigger("LeftHeadLoweredBite");
					pickLeftTailAttack();
					break;
				case 1:
					animator.SetTrigger("RightHeadLoweredBite");
					pickRightTailAttack();
					break;
			}
			headLoweredAttacksLeft -= 1;
		}
		else
		{
			animator.SetTrigger("RaiseHead");
			nessieTailObject.SetActive(false);
			headLowered = false;
			headRaisedAttacksLeft = Random.Range(minHeadRaisedAttacks, maxHeadRaisedAttacks + 1);
		}
	}
	
	private void chooseHeadRaisedAttack()
	{
		blockState = BlockStates.INVULN;
		if (headRaisedAttacksLeft > 0)
		{
			switch (Random.Range(1, 2))
			{
				case 0:
					animator.SetTrigger("LeftThrashBiteStart");
					thrashBiteDirTracker = Positions.Position.LEFT;
					break;
				case 1:
					animator.SetTrigger("RightThrashBiteStart");
					thrashBiteDirTracker = Positions.Position.RIGHT;
					break;
			}
			headRaisedAttacksLeft -= 1;
		}
		else
		{
			animator.SetTrigger("LowerHead");
			nessieTailObject.SetActive(true);
			headLowered = true;
			headLoweredAttacksLeft = Random.Range(minHeadLoweredAttacks, maxHeadLoweredAttacks + 1);
		}
	}

	public void thrashBiteLink()
	{
		if ((headRaisedAttacksLeft - 1) > 0 && Random.Range(0, 100) <= 79)
		{
			switch (thrashBiteDirTracker)
			{
				case Positions.Position.LEFT:
					animator.SetTrigger("RightThrashBiteChain");
					thrashBiteDirTracker = Positions.Position.RIGHT;
					break;
				case Positions.Position.RIGHT:
					animator.SetTrigger("LeftThrashBiteChain");
					thrashBiteDirTracker = Positions.Position.LEFT;
					break;
			}
		}
		else
		{
			switch (thrashBiteDirTracker)
			{
				case Positions.Position.LEFT:
					animator.SetTrigger("RightThrashBiteEnder");
					break;
				case Positions.Position.RIGHT:
					animator.SetTrigger("LeftThrashBiteEnder");
					break;
			}
		}
		headRaisedAttacksLeft -= 1;
	}
	
	private void pickLeftTailAttack()
	{
		switch (Random.Range(0, 2))
		{
			case 0:
				nessieTail.SetTrigger("LeftSwipeLeft");
				break;
			case 1:
				nessieTail.SetTrigger("LeftSwipeRight");
				break;
		}
	}
	
	private void pickRightTailAttack()
	{
		switch (Random.Range(0, 2))
		{
			case 0:
				nessieTail.SetTrigger("RightSwipeLeft");
				break;
			case 1:
				nessieTail.SetTrigger("RightSwipeRight");
				break;
		}
	}
	
	public override bool getHit(int damage, Positions.Position directionOfPunch)
	{
		nessieGrunt.Play();
		if (stunLock)
		{
			quickPunchSound.Play();
			takeDamage(damage, directionOfPunch);
            return true;
		}
		else
		{
			switch (blockState)
			{
				case BlockStates.ALL:
					StopAllCoroutines();
					animator.SetTrigger("Block");
					nessieTail.SetTrigger("Block");
					blockSound.Play();
					break;
				case BlockStates.NONE:
					StopAllCoroutines();
					takeDamage(damage, directionOfPunch);
                    return true;
				case BlockStates.INVULN:
					break;
			}
            return false;
		}
	}
	
	protected override void takeDamage(int damage, Positions.Position directionOfPunch)
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
			nessieTail.SetTrigger("GetHitFrom" + animationTriggerDirection);
			checkStun();
		}
	}
	
	protected override void playDefeatAnimation()
	{
		animator.SetTrigger("Dies");
		nessieTail.SetTrigger("Dies");
	}
}
