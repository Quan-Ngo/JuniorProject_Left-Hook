using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class moneyManager : MonoBehaviour
{
	public static moneyManager instance;
	
	[SerializeField] private int playerMoney;

	//public TextMeshProUGUI shopMoneyText; 

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this);
		}
		
    }

	// Update is called once per frame
	void Update()
	{
		//shopMoneyText.text = playerMoney.ToString(); 
	}
	
	public void addMoney(int money)
	{
		playerMoney += money; 
    }
	
	public void subtractMoney(int amount)
	{
		playerMoney -= amount;
	}

    public int getCurrentMoney()
	{
		return playerMoney;
	}
	
	public bool checkAmount(int amount)
	{
		return playerMoney >= amount;
	}
}
