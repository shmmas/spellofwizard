using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronPotionMaker : MonoBehaviour
{
    public Transform cauldronTop; // Posisi spawn potion
    public GameObject PotionHealPrefab, PotionShieldPrefab, poisonPotionPrefab, perfumePotionPrefab; // Prefab potion
    private List<string> ingredients = new List<string>(); // List bahan yang masuk

    private Dictionary<string, GameObject> potionRecipes = new Dictionary<string, GameObject>();

    void Start()
    {
        // Inisialisasi resep
        potionRecipes["MagicOrb+LeafOfLife"] = PotionHealPrefab;
        potionRecipes["MagicOrb+CrystalQuartz"] = PotionShieldPrefab;
        potionRecipes["MagicOrb+ToxicSlime"] = poisonPotionPrefab;
        potionRecipes["MagicOrb+SmellingFlower"] = perfumePotionPrefab;
    }

    private void OnTriggerEnter(Collider other)
    {
        string ingredientName = other.gameObject.tag; // Gunakan tag objek sebagai nama bahan
       
        if (other.CompareTag("Potion")) 
         {
        Debug.Log("Potion sudah dibuat, abaikan!");
        return; // Potion tidak dianggap sebagai bahan
         }
        if (!ingredients.Contains(ingredientName))
        {
            ingredients.Add(ingredientName);
            Destroy(other.gameObject); // Hancurkan bahan setelah masuk ke tungku
            Debug.Log("Bahan masuk: " + ingredientName);
        }

        // Jika sudah ada 2 bahan, coba buat potion
        if (ingredients.Count == 2)
        {
            TryCreatePotion();
        }
    }

private void TryCreatePotion()
{
    if (!ingredients.Contains("MagicOrb"))
    {
        Debug.LogWarning("❌ MagicOrb harus selalu ada sebagai bahan utama!");
        ingredients.Clear();
        return;
    }

    // Ambil bahan tambahan selain MagicOrb
    string otherIngredient = ingredients.Find(i => i != "MagicOrb");

    // Pastikan MagicOrb selalu di depan dalam key
    string key = "MagicOrb+" + otherIngredient;

    Debug.Log("✅ Key yang dibuat setelah diperbaiki: " + key);

    if (potionRecipes.ContainsKey(key))
    {
        Debug.Log("✅ Potion ditemukan di resep!");
        SpawnPotion(potionRecipes[key]);
    }
    else
    {
        Debug.LogWarning("❌ Kombinasi bahan tidak valid! Periksa apakah key sesuai dengan dictionary.");
    }

    ingredients.Clear();
}



void SpawnPotion(GameObject potionPrefab)
{
    if (cauldronTop == null)
    {
        Debug.LogError("❌ potionSpawnPoint belum diatur di Inspector!");
        return;
    }

    // Buat potion dari prefab
    GameObject potion = Instantiate(potionPrefab, cauldronTop.position, Quaternion.identity);

    // Ambil komponen Rigidbody dari prefab (tidak dibuat lewat script)
    Rigidbody rb = potion.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.isKinematic = true; // Biarkan melayang di awal
    }

    Debug.Log("📦 Potion melayang dan bisa diambil!");
}

}
