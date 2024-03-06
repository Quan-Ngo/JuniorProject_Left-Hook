using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsManager : MonoBehaviour
{
	public enum ShopKeepChildrenName {FINN, FLOW, FITZ};
	
	[SerializeField]private GameObject currentShopKeeper;
	
	private readonly ShopKeepChildrenName[] shopKeepChildren = {ShopKeepChildrenName.FINN, ShopKeepChildrenName.FLOW, ShopKeepChildrenName.FITZ};
	
	public static KidsManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
		pickShopKeepChild();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void pickShopKeepChild()
	{
		currentShopKeeper.GetComponent<ChildScript>().setIdentity(shopKeepChildren[Random.Range(0, shopKeepChildren.Length)]);
		currentShopKeeper.SetActive(true);
	}
}

