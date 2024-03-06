using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationListener : MonoBehaviour
{
	[SerializeField] private PlayerController player;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void endAnimationLock()
	{
		player.endAnimationLock();
	}
	
	public void resetDodgePosition()
	{
		player.resetDodgePosition();
	}
	
	public void leftPunch()
	{
		player.hitFish(Positions.Position.LEFT);
	}
	
	public void rightPunch()
	{
		player.hitFish(Positions.Position.RIGHT);
	}
}
