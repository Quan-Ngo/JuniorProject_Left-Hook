using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnMouseEnter : MonoBehaviour
{
	[SerializeField] private ChildScript shopKeeper;
	
	[SerializeField] private string itemName;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void MouseEntered()
	{
		shopKeeper.speakItemDescription(itemName);
	}
}
