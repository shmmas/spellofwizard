using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventorySlots = new List<GameObject>(); // Slot UI
    private List<GameObject> storedItems = new List<GameObject>(); // Item yang disimpan

    public void StoreItem(GameObject item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (storedItems.Count <= i || storedItems[i] == null)
            {
                storedItems.Insert(i, item);
                item.SetActive(false); // Sembunyikan item dari dunia
                Debug.Log("Item stored in slot: " + i);
                return;
            }
        }
        Debug.Log("Inventory full!");
    }

    public void RetrieveItem(int slotIndex)
    {
        if (slotIndex < storedItems.Count && storedItems[slotIndex] != null)
        {
            GameObject item = storedItems[slotIndex];
            item.SetActive(true); // Munculkan kembali
            item.transform.position = transform.position + transform.forward * 0.5f; // Spawn di depan player
            storedItems[slotIndex] = null; // Kosongkan slot
            Debug.Log("Item retrieved from slot: " + slotIndex);
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Pastikan inventory tetap ada
    }

}
