using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using TMPro;

public class HowToButton : MonoBehaviour
{

    public GameObject howToButton;
    public GameObject howToPanel;
    public Transform tutorialPages;

    private int pageNum = 0;


    // Start is called before the first frame update
    void Start()
    {
        howToPanel.SetActive(false);
        tutorialPages.SetSiblingIndex(pageNum);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenHowToScreen()
    {
        howToPanel.SetActive(true);
        tutorialPages.transform.GetChild(pageNum).gameObject.SetActive(true);
    }

    public void ExitHowToScreen()
    {
        howToPanel.SetActive(false);
    }

    public void NextPageRight()
    {
        if (pageNum >= 0)
        {
            pageNum += 1;
            turnPage();
        }
        if (pageNum > 3)
        {
            pageNum = 0;
            turnPage();
        }
    }

    public void NextPageLeft()
    {
        if (pageNum >= 0)
        {
            pageNum -= 1;
            turnPageLeft();
        }
        if (pageNum < 0)
        {
            pageNum = 3;
            turnPageLeft();
        }
    }

    void turnPage()
    {
        if (pageNum <= 3)
        {
            tutorialPages.transform.GetChild(pageNum).gameObject.SetActive(true);
            tutorialPages.transform.GetChild(pageNum - 1).gameObject.SetActive(false);
        }
        if (pageNum > 3)
        {
            tutorialPages.transform.GetChild(0).gameObject.SetActive(true);
            tutorialPages.transform.GetChild(3).gameObject.SetActive(false);
        }
        
    }

    void turnPageLeft()
    {
        if (pageNum >= 0)
        {
            tutorialPages.transform.GetChild(pageNum).gameObject.SetActive(true);
            tutorialPages.transform.GetChild(pageNum + 1).gameObject.SetActive(false);
        }
        if (pageNum < 0)
        {
            tutorialPages.transform.GetChild(3).gameObject.SetActive(true);
            tutorialPages.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
}