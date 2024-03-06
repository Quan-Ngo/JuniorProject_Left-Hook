using UnityEngine;
using UnityEngine.UI;

public class BoxingGloves : MonoBehaviour
{
    public Button glovesButton;
    public PlayerStats playerStats;
    public moneyManager moneyManager;
    private bool glovesUsed = false;
    private int glovesCost = 20;
    void Start()
    {
        glovesButton.onClick.AddListener(UseGloves);
        UpdateButtonInteractable();
    }

    void Update()
    {
        UpdateButtonInteractable(); 
    }

    void UseGloves()
    {
        if (!glovesUsed && moneyManager.checkAmount(glovesCost))
        {
            playerStats.increasePunchDamage(20);
            glovesUsed = true;
            glovesButton.interactable = false;
            moneyManager.subtractMoney(glovesCost);
        }
    }
    void UpdateButtonInteractable()
    {
        glovesButton.interactable = !glovesUsed && moneyManager.checkAmount(glovesCost);
    }
}
