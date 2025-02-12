using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public VRInventory inventory; // Referensi ke skrip inventory
    public Image[] slotIcons; // UI Image untuk setiap slot inventory

    void Update()
    {
        // Update UI setiap frame
        for (int i = 0; i < inventory.inventorySlots.Length; i++)
        {
            if (inventory.inventorySlots[i].childCount > 0)
            {
                // Jika ada item di slot, tampilkan ikonnya
                slotIcons[i].sprite = inventory.inventorySlots[i].GetChild(0).GetComponent<Item>().icon;
                slotIcons[i].color = Color.white;
            }
            else
            {
                // Jika slot kosong, hilangkan ikon
                slotIcons[i].sprite = null;
                slotIcons[i].color = Color.clear;
            }
        }
    }
}