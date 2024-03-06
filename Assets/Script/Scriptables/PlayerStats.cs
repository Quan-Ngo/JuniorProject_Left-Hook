using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptables/Player Stats")]

public class PlayerStats : ScriptableObject
{
	[SerializeField] private int punchDamage;
	[SerializeField] private int lineStrength;
	//[SerializeField] private int playerHealth;
	[SerializeField] private int maxHealth;
	[SerializeField] private bool hasFishVitaminSupplement = false;
	[SerializeField] private int vitaminSupplementRegenAmount = 20;

	public int getPunchDamage()
	{
		return punchDamage;
	}

	public int getLineStrength()
	{
		return lineStrength;
	}

	/*public int getPlayerHealth()
	{
		return playerHealth;
	}*/
	
	public int getVitaminRegenAmount()
	{
		return vitaminSupplementRegenAmount;
	}

	/*public void setPlayerHealth(int health)
	{
		playerHealth = health;
	}*/

	public int getMaxHealth()
	{
		return maxHealth;
	}

	public void increasePunchDamage(int amount)
	{
		punchDamage += amount;
	}

	public void increaseLineStrength(int amount)
	{
		lineStrength += amount;
	}

	public void buyFishVitaminSupplement()
	{
		hasFishVitaminSupplement = true;
	}

	/*public void consumeFishVitaminSupplement()
	{
		hasFishVitaminSupplement = false;
	}*/

	public bool checkFishVitaminSupplement()
	{
		return hasFishVitaminSupplement;
	}

	/*public void regenerateHealth()
	{
		if (hasFishVitaminSupplement)
		{
			playerHealth += vitaminSupplementRegenAmount;
			if (playerHealth > maxHealth) 
			{
				playerHealth = maxHealth;
			}
			hasFishVitaminSupplement = false; 
		}
	}*/
}
