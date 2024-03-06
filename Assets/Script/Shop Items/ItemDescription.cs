using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;  // Assign your tooltip (Panel or Image) in the Inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
