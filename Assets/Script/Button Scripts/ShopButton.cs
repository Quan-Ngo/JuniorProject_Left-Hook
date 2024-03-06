using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour
{
	[SerializeField] private int boxingGlovesCost;
	[SerializeField] private int fishingReelCost;
	[SerializeField] private int fishVitaminSupplementCost;
	[SerializeField] private updateMoneyDisplay moneyDisplay;
	[SerializeField] private PlayerStats stat;
	
    //public GameObject fishingButton; 

    // Start is called before the first frame update
    void Start()
    {
        /*Button button = this.GetComponent<Button>(); 
        button.onClick.AddListener(OpenShop);*/
    }

    public void OpenShop()
    {
        
        SceneManager.LoadScene("Shop");

       /* // Make the ShopButton and FishingButton invisible
        this.gameObject.SetActive(false);
        fishingButton.SetActive(false);*/
    }
	
	public void buyBoxingGloves()
	{
		if (moneyManager.instance.checkAmount(boxingGlovesCost))
		{
			confirmPurchase(boxingGlovesCost);
			stat.increasePunchDamage(1);
			InventoryManager.instance.boxingGlovesCount++;
			Debug.Log("boxing gloves bought");
		}
	}
	
	public void buyFishingReel()
	{
		if (moneyManager.instance.checkAmount(fishingReelCost))
		{
			confirmPurchase(fishingReelCost);
			stat.increaseLineStrength(1);
			InventoryManager.instance.fishingReelCount++;
			Debug.Log("fishing reel bought");
		}
	}

	public void buyFishVitaminSupplement()
	{
		if (moneyManager.instance.checkAmount(fishVitaminSupplementCost))
		{
			confirmPurchase(fishVitaminSupplementCost);
			stat.buyFishVitaminSupplement();
			InventoryManager.instance.fishVitaminSupplementCount++;
			Debug.Log("fish vitamin supplement bought");
		}
	}

	private void confirmPurchase(int moneyAmount)
	{
		moneyManager.instance.subtractMoney(moneyAmount);	
		moneyDisplay.updateMoney();
	}
}
