using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishSpawnTable", menuName = "Scriptables/Fish Spawn Table")]

public class FishTable : ScriptableObject
{
	[SerializeField] private GameObject[] easyFishBracket;
	[SerializeField] private GameObject[] mediumFishBracket;
	[SerializeField] private GameObject[] hardFishBracket;
	[SerializeField] private GameObject[] bossFishBracket;
	
	[SerializeField] int mediumFishThreshhold;
	[SerializeField] int hardFishThreshhold;
	[SerializeField] int bossFishThreshhold;
	[SerializeField] private int heatLevel;
	
	public GameObject chooseFishToSpawn()
	{
		if (heatLevel < mediumFishThreshhold)
		{
			return grabEasyFish();
		}
		else if (heatLevel < hardFishThreshhold)
		{
			return grabEasyOrMediumFish();
		}
		else if (heatLevel < bossFishThreshhold)
		{
			return grabMediumOrHardFish();
		}
		else 
		{
			return bossFishBracket[0];
		}
	}
	
    private GameObject grabEasyFish()
	{
		return easyFishBracket[Random.Range(0, easyFishBracket.Length)];
	}
	
	private GameObject grabEasyOrMediumFish()
	{
		int choice = Random.Range(0, 2);
		
		if (choice == 0)
		{
			return easyFishBracket[Random.Range(0, easyFishBracket.Length)];
		}
		else
		{
			return mediumFishBracket[Random.Range(0, mediumFishBracket.Length)];
		}
	}
	
	private GameObject grabMediumOrHardFish()
	{
		int choice = Random.Range(0, 2);
		
		if (choice == 0)
		{
			return mediumFishBracket[Random.Range(0, mediumFishBracket.Length)];
		}
		else
		{
			return hardFishBracket[Random.Range(0, hardFishBracket.Length)];
		}
	}
	
	public void increaseHeat()
	{
		heatLevel += 1;
	}
}
