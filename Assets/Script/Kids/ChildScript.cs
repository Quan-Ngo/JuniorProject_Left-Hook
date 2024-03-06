using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
	protected KidsManager.ShopKeepChildrenName currentIdentity;
	protected Dictionary<string, string> dialogueDict;
	
	[SerializeField]protected Sprite FlowSprite;
	[SerializeField]protected Sprite FinnSprite;
	[SerializeField]protected Sprite FitzSprite;
	[SerializeField]protected string[] FlowDialogueEntry;
	[SerializeField]protected string[] FinnDialogueEntry;
	[SerializeField]protected string[] FitzDialogueEntry;
		
	public void setIdentity(KidsManager.ShopKeepChildrenName newIdentity)
	{
		currentIdentity = newIdentity;
		initializeIdentity();
	}
	
	protected void initializeIdentity()
	{
		switch (currentIdentity)
		{
			case KidsManager.ShopKeepChildrenName.FLOW:
				GetComponent<SpriteRenderer>().sprite = FlowSprite;
				initializeDialogueDict(FlowDialogueEntry);
				break;
			case KidsManager.ShopKeepChildrenName.FINN:
				GetComponent<SpriteRenderer>().sprite = FinnSprite;
				initializeDialogueDict(FinnDialogueEntry);
				break;
			case KidsManager.ShopKeepChildrenName.FITZ:
				GetComponent<SpriteRenderer>().sprite = FitzSprite;
				initializeDialogueDict(FitzDialogueEntry);
				break;
		}
	}
	
	protected void initializeDialogueDict(string[] dialogueEntry)
	{
		string[] splittedLine;
		
		dialogueDict = new Dictionary<string, string>();
		
		foreach(string entry in dialogueEntry)
		{
			splittedLine = entry.Split('|');
			dialogueDict.Add(splittedLine[0], splittedLine[1]);
		}
	}
	
	public void speakItemDescription(string itemName)
	{
		Debug.Log(dialogueDict[itemName]);
	}
}
