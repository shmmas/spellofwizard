using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRInventory : MonoBehaviour
{
    public Transform[] inventorySlots; // 4 slot inventory
    public XRController controller; // Controller untuk mengaktifkan/nonaktifkan inventory
    public XRBaseInteractor interactor; // Interactor untuk mengambil item
    public AudioClip grabSound, storeSound, dropSound; // Suara feedback

    private bool isInventoryActive = false;

    void Update()
    {
        // Toggle inventory dengan tombol trigger
        if (controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            ToggleInventory();
        }

        // Drop item dengan tombol grip
        if (controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out bool gripPressed) && gripPressed)
        {
            DropItem(0); // Drop item dari slot pertama
        }

        // Jika inventory aktif, ikuti posisi controller
        if (isInventoryActive)
        {
            transform.position = controller.transform.position;
            transform.rotation = controller.transform.rotation;
        }
    }

    void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;
        gameObject.SetActive(isInventoryActive);
    }

    public void StoreItem(GameObject item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].childCount == 0) // Cari slot yang kosong
            {
                item.transform.SetParent(inventorySlots[i]);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
                item.GetComponent<Rigidbody>().isKinematic = true; // Nonaktifkan physics
                PlaySound(storeSound); // Mainkan suara saat menyimpan
                break;
            }
        }
    }

    public void DropItem(int slotIndex)
    {
        if (inventorySlots[slotIndex].childCount > 0)
        {
            Transform item = inventorySlots[slotIndex].GetChild(0);
            item.SetParent(null); // Lepaskan dari slot
            item.GetComponent<Rigidbody>().isKinematic = false; // Aktifkan physics
            item.GetComponent<Rigidbody>().AddForce(controller.transform.forward * 5f, ForceMode.Impulse); // Lempar item ke depan
            PlaySound(dropSound); // Mainkan suara saat melepaskan
        }
    }

    public void HighlightSlot(int slotIndex, bool highlight)
    {
        if (highlight)
        {
            inventorySlots[slotIndex].GetComponent<Renderer>().material.color = Color.yellow; // Highlight kuning
        }
        else
        {
            inventorySlots[slotIndex].GetComponent<Renderer>().material.color = Color.white; // Kembalikan ke warna default
        }
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position); // Mainkan suara di posisi inventory
    }
}