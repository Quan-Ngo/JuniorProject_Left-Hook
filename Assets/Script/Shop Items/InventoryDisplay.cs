using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject boxingGlovesIcon;
    public TextMeshProUGUI boxingGlovesCountText;

    public GameObject fishingReelIcon;
    public TextMeshProUGUI fishingReelCountText;

    public GameObject fishVitaminSupplementIcon;
    public TextMeshProUGUI fishVitaminSupplementCountText;

    void Update()
    {
        // Update Boxing Gloves Display
        if (InventoryManager.instance.boxingGlovesCount > 0)
        {
            boxingGlovesIcon.SetActive(true);
            boxingGlovesCountText.text = InventoryManager.instance.boxingGlovesCount.ToString();
        }

        // Update Fishing Reel Display
        if (InventoryManager.instance.fishingReelCount > 0)
        {
            fishingReelIcon.SetActive(true);
            fishingReelCountText.text = InventoryManager.instance.fishingReelCount.ToString();
        }

        // Update Fish Vitamin Supplement Display
        if (InventoryManager.instance.fishVitaminSupplementCount > 0)
        {
            fishVitaminSupplementIcon.SetActive(true);
            fishVitaminSupplementCountText.text = InventoryManager.instance.fishVitaminSupplementCount.ToString();
        }
    }
}
