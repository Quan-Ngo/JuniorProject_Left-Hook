using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishingGameArrowCode : MonoBehaviour
{
    [SerializeField] private float arrowSpeed;
	[SerializeField] private string curColor;
	[SerializeField] private FishingMinigameManager fishingMinigameManager;
	
	private Vector2 arrowDefaultCoord;

    void Start()
    {
        arrowDefaultCoord = transform.position;
    }
	
	public void OnDisable()
	{
		transform.position = arrowDefaultCoord;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
			updateFishingManager();
        }
    }
	
	void FixedUpdate()
	{
		transform.Translate(Vector2.right * arrowSpeed);
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		curColor = collision.gameObject.tag;
	}
	
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Reset")
		{
			curColor = "Red";
			updateFishingManager();
		}
	}
	
	private void updateFishingManager()
	{
		transform.position = arrowDefaultCoord;
		fishingMinigameManager.timingResult(curColor);
	}
}
