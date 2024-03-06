using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : FishAI
{
	private bool electrified;
	
	[SerializeField] private int elecFieldDamage;
	[SerializeField] private int minNormalPunches;
	[SerializeField] private int maxNormalPunches;
	[SerializeField] private int minElecPunches;
	[SerializeField] private int maxElecPunches;

	private int normalPunchesLeft;
	private int elecPunchesLeft;

	public AudioSource zapSound;
	public AudioSource superSound;
	
    // Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
        electrified = false;
		normalPunchesLeft = Random.Range(minNormalPunches, maxNormalPunches + 1);
    }

	public override void recover()
	{
		if (electrified)
		{
			animator.SetTrigger("ElecNeutral");
			StartCoroutine(neutralState());
		}
		else
		{
			base.recover();
		}
	}
	
	public override bool getHit(int damage, Positions.Position directionOfPunch)
	{
		if (electrified)
		{
			player.selfHurt(elecFieldDamage);
            return false;
		}
		else
		{
			superSound.Stop();
			return base.getHit(damage, directionOfPunch);
		}
	}
	
    protected override void attack()
	{
		
		blockState = BlockStates.INVULN;
		
		if (!electrified)
		{
			normalAttack();
		}
		else
		{
			elecAttack();
		}
	}
	
	private void normalAttack()
	{
		int chosenAttack;
		
		if (normalPunchesLeft > 0)
		{
			chosenAttack = Random.Range(0, 2);
			switch (chosenAttack)
			{
				case 0:
					animator.SetTrigger("PunchR");
					break;
				case 1:
					animator.SetTrigger("PunchL");
					break;
			}
			normalPunchesLeft -= 1;
		}
		else
		{
			electrified = true;
			elecPunchesLeft = Random.Range(minElecPunches, maxElecPunches + 1);
			timeBetweenAttacks = timeBetweenAttacks/2;
			animator.SetTrigger("Electrify");
			zapSound.Play();
		}
	}
	
	private void elecAttack()
	{
		int chosenAttack;
		
		if (elecPunchesLeft > 0)
		{
			chosenAttack = Random.Range(0, 2);
			switch (chosenAttack)
			{
				case 0:
					animator.SetTrigger("UpperCutR");
					break;
				case 1:
					animator.SetTrigger("UpperCutL");
					break;
			}
			elecPunchesLeft -= 1;
		}
		else
		{
			blockState = BlockStates.NONE;
			electrified = false;
			animator.SetTrigger("Super");
			normalPunchesLeft = Random.Range(minNormalPunches, maxNormalPunches + 1);
			timeBetweenAttacks = timeBetweenAttacks * 2;
			zapSound.Stop();
			superSound.Play();
		}
	}
}
