using Unity.VisualScripting;
using UnityEngine;

// Apple sınıfı Pickup sınıfından türetilmiş bir nesnedir
// Toplandığında oyundaki chunk (yol/parça) hareket hızını artırır
public class Apple : Pickup
{
    [SerializeField] float adjustChangeMoveSpeedAmount = 3f; // Toplandığında hareket hızını artırma miktarı
    LevelGenerator levelGenerator; // LevelGenerator referansı, hız değişikliği için

    // Apple spawn edildiğinde LevelGenerator referansı atanır
    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    // Pickup toplandığında çalışacak fonksiyon
    protected override void OnPickup()
    {
        // LevelGenerator üzerinden chunk hareket hızını artır
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
    }
}
