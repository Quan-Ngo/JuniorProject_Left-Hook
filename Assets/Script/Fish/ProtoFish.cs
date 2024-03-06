using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoFish : FishAI
{	
	protected override void attack()
	{
		int chosenAttack;
		
		chosenAttack = Random.Range(0, 2);
		blockState = BlockStates.INVULN;
		switch (chosenAttack)
		{
			case 0:
				animator.SetTrigger("AttackR");
				break;
			case 1:
				animator.SetTrigger("AttackL");
				break;
		}
	}
}
