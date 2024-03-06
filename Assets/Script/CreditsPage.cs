using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class CreditsPage : MonoBehaviour
{
    public GameObject creditsPanel; 
    
    // Start is called before the first frame update
    void Start()
    {
        creditsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCredits()

    { 
        creditsPanel.SetActive(true);
    }

    public void CloseCredits() 
    { 
        creditsPanel.SetActive(false);
    
    }
}
