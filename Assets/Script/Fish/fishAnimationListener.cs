using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishAnimationListener : MonoBehaviour
{
	[SerializeField] protected FishAI fish;
	//[SerializeField] private PlayerController player;
	
	public void startRecovery()
	{
		fish.recover();
	}
	
	public void hitLeft()
	{
		fish.hitPlayer(Positions.Position.LEFT);
		//player.getHit("left");
	}
	
	public void hitRight()
	{
		fish.hitPlayer(Positions.Position.RIGHT);
		//player.getHit("right");
	}
	
	public void hitMid()
	{
		fish.hitPlayer(Positions.Position.MID);
		//player.getHit("mid");
	}
	
	public void Death()
	{
		fish.Death();
	}
	
	public void blockStateToNone()
	{
		fish.blockStateToNone();
	}
}
