using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private GameObject storedItem;

    public bool HasItem()
    {
        return storedItem != null;
    }

    public void StoreItem(GameObject item)
    {
        if (storedItem == null)
        {
            storedItem = item;
            item.SetActive(false); // Sembunyikan item dari dunia
            Debug.Log($"Item {item.name} stored in {gameObject.name}");
        }
    }

    public void RetrieveItem()
    {
        if (storedItem != null)
        {
            storedItem.SetActive(true);
            storedItem.transform.position = transform.position + transform.forward * 0.5f; // Spawn di depan player
            Debug.Log($"Item {storedItem.name} retrieved from {gameObject.name}");
            storedItem = null;
        }
    }
}
