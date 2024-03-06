using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class MenuButtons : MonoBehaviour
{
	public GameObject settingPanel;
	public GameObject inGameHowTo;
	 

	private bool _settingsPanelActive; 
	private bool _howToPanelActive; 

    public void Start()
    {
		settingPanel.SetActive(false);
	
    }

    public void Update()
    {
		if (Input.GetKeyUp(KeyCode.Escape) && _settingsPanelActive == false)
		{
			settingPanel.SetActive(true);
			_settingsPanelActive = true;
			Time.timeScale = 0f; 
		  
        }

        if (_settingsPanelActive == true && Input.GetKeyDown(KeyCode.Escape))
        {
         
            settingPanel.SetActive(false);
            StartCoroutine(ResetEscape());
			Time.timeScale = 1f; 
        }
    }

    public void Quit()
	{
		Application.Quit();
	}
	
	public void Play()
	{
		SceneManager.LoadScene("MainGame");
	}

    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

	public void MainMenuButton()
	{
		SceneManager.LoadScene("NewMenu");
	}	


    public void openSettingPanel()
	{
		settingPanel.gameObject.SetActive(true);
	}
	
	public void closePanel()
	{
        settingPanel.gameObject.SetActive(false);

		Time.timeScale = 1f;
	}

	public void openHowToPanelInGame ()
	{
		inGameHowTo.SetActive(true); 
	}

	public void closeHowtoPanelInGame ()
	{
		inGameHowTo.SetActive(false);
	}

	IEnumerator ResetEscape()
	{
		// This allows for the functions in Update to not overlap each other as the escape key function waits to interact with second escape call
		yield return new WaitForSeconds(1);
		_settingsPanelActive = false; 
	}
}
