using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuartzSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableItem
    {
        public string itemName;         // Nama item (Quartz, Orb, dsb.)
        public GameObject prefab;       // Prefab item yang akan di-spawn
    }

    public List<SpawnableItem> itemsToSpawn; // List item yang bisa di-spawn
    public float respawnTime = 2f;         // Waktu respawn
    public int maxItems = 10;              // Maksimum jumlah item yang bisa ada di dunia

    private int currentItemCount = 0;

void Start()
{
    StartCoroutine(DelayedSpawnStart());
}

IEnumerator DelayedSpawnStart()
{
    yield return new WaitForSeconds(1f); // Tunggu 1 detik agar fisika stabil
    SpawnRandomItem();
}

    public void ItemTaken()
    {
        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);
        if (currentItemCount < maxItems)
        {
            SpawnRandomItem();
        }
    }

    void SpawnRandomItem()
    {
        if (itemsToSpawn.Count == 0) return;

        // Pilih item random dari daftar itemsToSpawn
        int randomIndex = Random.Range(0, itemsToSpawn.Count);
        SpawnableItem selectedItem = itemsToSpawn[randomIndex];

        // Spawn item di lokasi spawner
        GameObject newItem = Instantiate(selectedItem.prefab, transform.position, transform.rotation);
        CollectibleItem itemScript = newItem.GetComponent<CollectibleItem>();
        if (itemScript != null)
        {
            itemScript.SetSpawner(this);
        }
        currentItemCount++;
    }

    public void DecreaseItemCount()
    {
        currentItemCount--;
    }
}
