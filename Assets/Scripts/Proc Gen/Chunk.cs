using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject fencePrefab; // Çit prefab'ı
    [SerializeField] GameObject applePrefab; // Elma prefab'ı
    [SerializeField] GameObject coinPrefab;  // Para prefab'ı

    [Header("Spawn Chances")]
    [SerializeField] float appleSpawnChance = .3f; // Elma spawn olma olasılığı
    [SerializeField] float coinSpawnChance = .3f;  // Para spawn olma olasılığı
    [SerializeField] float coinSeperationLength = 2f; // Para spawn aralığı

    [Header("Lanes")]
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f }; // Oyuncu yolları (sol-orta-sağ)

    LevelGenerator levelGenerator; // Chunk hareketini ve oyun akışını yöneten
    ScoreManager scoreManager;     // Toplanan paraları skorlayacak manager

    List<int> availableLanes = new List<int> { 0, 1, 2 }; // O an kullanılabilir yollar

    void Start()
    {
        // Chunk spawn olduğunda çit, elma ve paraları üret
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }

    // LevelGenerator ve ScoreManager referanslarını alır
    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    // Kullanılabilir yollardan rastgele seçim yapar ve seçilen yolu kullanılabilirler listesinden çıkarır
    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];

        availableLanes.RemoveAt(randomLaneIndex); // Aynı yol tekrar kullanılmasın
        return selectedLane;
    }

    // Çitleri spawn eder
    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length); // Spawn edilecek çit sayısı

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break; // Kullanılabilir yol kalmadıysa çık

            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    // Elma spawn eder
    void SpawnApple()
    {
        // Spawn olasılığına göre veya yol kalmadıysa çık
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator); // Elmayı hareket eden chunk ile ilişkilendir
    }

    // Paraları spawn eder
    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();

        int maxCoinToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinToSpawn);

        // Chunk'ın üst kısmından başla
        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (i * coinSeperationLength); // Her bir coin'i aralıkla yerleştir
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoint = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoint.Init(scoreManager); // Coin score manager ile ilişkilendir
        }
    }
}
