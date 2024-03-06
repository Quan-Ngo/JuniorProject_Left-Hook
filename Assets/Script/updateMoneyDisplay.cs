using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class updateMoneyDisplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI moneyDisplay;



	
    // Start is called before the first frame update
    void Start()
    {
		if (moneyManager.instance != null)
		{
			updateMoney();
		}
    }

    // Update is called once per frame
    public void updateMoney()
    {
        moneyDisplay.text = moneyManager.instance.getCurrentMoney().ToString();
    }
}
