using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int slotIndex;
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        GetComponent<Button>().onClick.AddListener(() => playerInventory.RetrieveItem(slotIndex));
    }
}
