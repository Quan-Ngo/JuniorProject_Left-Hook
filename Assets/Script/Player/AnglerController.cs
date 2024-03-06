using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerController : MonoBehaviour
{
	private bool reeling;
	
	[SerializeField] Animator animator;
	
    // Start is called before the first frame update
    void Start()
    {
        reeling = false;
        animator.SetTrigger("startingReel");
    }

    // Update is called once per frame
    void Update()
    {
        if (reeling)
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			{
				animator.SetTrigger("reelL");
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			{
				animator.SetTrigger("reelR");
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			{
				animator.SetTrigger("reelD");
			}
		}
    }
	
	public void startReeling()
	{
		reeling = true;
       
    }
	
	public void endFish()
	{
		reeling = false;
		gameObject.SetActive(false);
	}
	
	public void waitForBite()
	{
		reeling = false;
		animator.SetTrigger("neutral");
	}
}
