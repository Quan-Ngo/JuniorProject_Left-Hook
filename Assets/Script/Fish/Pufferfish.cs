using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pufferfish : FishAI
{
	[SerializeField] private int spikeDamage;
	[SerializeField] private int minDeflatedAttacks;
	[SerializeField] private int maxDeflatedAttacks;
	
	private bool inflated;
	private int deflatedAttacksLeft;
	private Positions.Position vulnerableSide;

	public AudioSource inflateNoise;
	
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
		inflated = false;
		deflatedAttacksLeft = Random.Range(minDeflatedAttacks, maxDeflatedAttacks + 1);
    }
	
	public override void recover()
	{
		base.recover();
		
		if (inflated)
		{
			animator.SetTrigger("PuffNeutral");
		}
	}

	protected override void attack()
	{
		blockState = BlockStates.INVULN;

		if (inflated)
		{
			pickInflatedAttack();
		}
		else
		{
			pickDeflatedAttack();
		}
	}
	
	private void pickDeflatedAttack()
	{
		int chosenAttack;
		
		if (deflatedAttacksLeft >= 0)
		{			
			chosenAttack = Random.Range(0, 2);
			switch (chosenAttack)
			{
				case 0:
					animator.SetTrigger("AttackR");
					break;
				case 1:
					animator.SetTrigger("AttackL");
					break;
			}
			deflatedAttacksLeft -= 1;
		}
		else
		{
			currentStunVal = 0;
			inflated = true;
			inflateNoise.Play();
			animator.SetTrigger("PuffUp");
		}
	}
	
	private void pickInflatedAttack()
	{
		int chosenAttack;
		
		chosenAttack = Random.Range(0, 2);
		
		switch (chosenAttack)
		{
			case 0:
				animator.SetTrigger("SlamR");
				vulnerableSide = Positions.Position.RIGHT;
				break;
			case 1:
				animator.SetTrigger("SlamL");
				vulnerableSide = Positions.Position.LEFT;
				break;
		}
	}
	
	public override bool getHit(int damage, Positions.Position directionOfPunch)
	{
		if (!inflated)
		{
			return base.getHit(damage, directionOfPunch);
		}
		else
		{
			StopAllCoroutines();
			if (blockState == BlockStates.ALL)
			{
				animator.SetTrigger("PuffBlock");
				player.selfHurt(spikeDamage);
				return false;
			}
			else if(blockState == BlockStates.NONE)
			{
				if (directionOfPunch == vulnerableSide)
				{
					takeDamage(System.Math.Min(1, damage/2), directionOfPunch);
					return true;
				}
				else
				{
					player.selfHurt(spikeDamage);
					return false;
				}
			}
			else
			{
				return false;
			}
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
			
			if (inflated)
			{
				animator.SetTrigger("PuffGetHitFrom" + animationTriggerDirection);
			}
			else
			{
				animator.SetTrigger("GetHitFrom" + animationTriggerDirection);
			}
			checkStun();
		}
	}
	
	protected override void checkStun()
	{
		if (!stunLock)
		{
			currentStunVal++;
			displayShieldIcon();
		}
		
		if (currentStunVal == stunThreshhold)
		{
			inflated = false;
			deflatedAttacksLeft = Random.Range(minDeflatedAttacks, maxDeflatedAttacks + 1);
			StartCoroutine(stunLockCountDown());
		}
	}
}
