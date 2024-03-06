using UnityEngine;
using UnityEngine.UI;

public class FishingReel : MonoBehaviour
{
    public Button fishingReelButton;
    public PlayerStats playerStats;
    public moneyManager moneyManager;
    private bool fishingReelUsed = false;

    void Start()
    {
        fishingReelButton.onClick.AddListener(UseFishingReel);
    }

    void UseFishingReel()
    {
        if (!fishingReelUsed)
        {
            playerStats.increaseLineStrength(10);
            fishingReelUsed = true;
            fishingReelButton.interactable = false;
        }
    }
}
