using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingSettingPanel : MonoBehaviour
{
    public GameObject settingsPanel;
    private bool settingPanelClosed = true; 

    private void Update()
    {
        
    }

    public void closingPanel()
    {
        settingsPanel.SetActive(false);  
    }
}
