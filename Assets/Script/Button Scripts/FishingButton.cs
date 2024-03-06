using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishingButton : MonoBehaviour
{
    /*public FishingMinigameManager fishingMinigameManager; 
    public GameObject shopButton; */

    // Start is called before the first frame update
    void Start()
    {
        /*Button btn = this.GetComponent<Button>(); 
        btn.onClick.AddListener(StartFishing); */
    }

    public void StartFishing()
    {
        FishingMinigameManager.instance.startFishing(); 

        
        /*this.gameObject.SetActive(false);
        shopButton.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
