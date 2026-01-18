using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [SerializeField] float obstacleDecreaseTimeAmount = .2f; // Engel spawn süresini azaltma miktarı
    [SerializeField] float checkpointTimeExtension = 5f;    // Checkpoint ile oyuncuya eklenen süre

    // Oyun yöneticisi ve engel spawner referansları
    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;

    const string playerString = "Player"; // Checkpoint'i tetikleyecek obje etiketi

    void Start()
    {
        // Sahnedeki GameManager ve ObstacleSpawner bileşenlerini bul
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Checkpoint sadece oyuncu ile temas ettiğinde çalışır
        if (other.CompareTag(playerString))
        {
            // Oyuncuya süre ekle
            gameManager.IncreaseTime(checkpointTimeExtension);

            // Engel spawn hızını artır (spawn sürelerini azalt)
            obstacleSpawner.DecreaseObstacleSpawnTime(obstacleDecreaseTimeAmount);
        }
    }
}
