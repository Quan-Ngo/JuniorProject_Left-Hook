using System.Collections;
using UnityEngine;

public class Salmon : FishAI
{
	[SerializeField] private int bigDamage;
	[SerializeField] private int normalDamage; // Damage for normal attack
	[SerializeField] private float normalAttackChance; // Chance to perform normal attack
	[SerializeField] private float gimmickCooldown; // Cooldown time for the gimmick attack

	private Positions.Position attackSide;
	private float lastGimmickAttackTime;

	private enum SalmonState { SWIMMING, PAUSED, ATTACKING, NORMAL_ATTACKING };
	private SalmonState currentState;

	protected override void Start()
	{
		base.Start();
		currentState = SalmonState.SWIMMING;
		lastGimmickAttackTime = -gimmickCooldown; // To allow gimmick attack at the beginning
	}

	protected override void attack()
	{
		blockState = BlockStates.NONE;

		if (Time.time - lastGimmickAttackTime >= gimmickCooldown && Random.Range(0f, 1f) < normalAttackChance)
		{
			NormalAttack();
			return;
		}

		switch (currentState)
		{
			case SalmonState.SWIMMING:
				if (Time.time - lastGimmickAttackTime >= gimmickCooldown)
				{
					animator.SetTrigger("SwimUpWaterfall");
					StartCoroutine(PauseAtTop());
					lastGimmickAttackTime = Time.time;
				}
				break;
			case SalmonState.PAUSED:
				break;
			case SalmonState.ATTACKING:
				PlummetAttack();
				break;
		}
	}

	private void NormalAttack()
	{
		attackSide = Random.Range(0, 2) == 0 ? Positions.Position.LEFT : Positions.Position.RIGHT;
		animator.SetTrigger(attackSide == Positions.Position.LEFT ? "AttackL" : "AttackR");
		player.getHit(attackSide, normalDamage);
	}

	private void PlummetAttack()
	{
		animator.SetTrigger("PlummetAttack");

		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) 
		{
			// KO the fish
			currentHealth = 0;
			animator.SetTrigger("KO");
			currentState = SalmonState.SWIMMING;
		}
		else
		{
			player.getHit(attackSide, bigDamage);
			currentState = SalmonState.SWIMMING;
		}
	}

	private IEnumerator PauseAtTop()
	{
		// Pause for a brief moment
		yield return new WaitForSeconds(1f);

		attackSide = Random.Range(0, 2) == 0 ? Positions.Position.LEFT : Positions.Position.RIGHT;
		animator.SetTrigger(attackSide == Positions.Position.LEFT ? "TelegraphLeft" : "TelegraphRight");

		currentState = SalmonState.ATTACKING;
	}

	public override bool getHit(int damage, Positions.Position directionOfPunch)
	{
		if (currentState == SalmonState.ATTACKING)
		{
			takeDamage(damage, directionOfPunch);
			return true;
		}
		else
		{
			return false;
		}
	}
}
