using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitShopButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       /* Button btn = this.GetComponent<Button>(); // Get the Button component
        btn.onClick.AddListener(ExitShop); // Add a listener to the onClick event*/
    }

    public void ExitShop()
    {
        // Switch back to the MainGame scene
        SceneManager.LoadScene("MainGame"); // replace "MainGame" with the name of your MainGame scene
    }

    // Update is called once per frame
    void Update()
    {

    }
}
