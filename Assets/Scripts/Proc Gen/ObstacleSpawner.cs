using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Bu obje, oyun sırasında engelleri (obstacle) otomatik olarak üretir.
    // obstaclePrefabs -> Üretilecek engellerin listesi (önceden tasarlanmış prefablar)
    // obstacleSpawnTime -> Engellerin ne kadar aralıkla çıkacağı
    // minObstacleSpawnTime -> Engellerin çıkma süresinin en kısa süresi
    // obstacleParent -> Üretilen engellerin sahnede hangi objenin altında tutulacağı
    // spawnWidth -> Engellerin X ekseninde hangi genişlikte rastgele çıkacağını belirler

    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obstacleSpawnTime = 1f;
    [SerializeField] float minObstacleSpawnTime = 1f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f;

    void Start()
    {
        // Oyun başladığında engel üretme döngüsünü başlatır
        StartCoroutine(SpawnObstacleRoutine());
    }

    public void DecreaseObstacleSpawnTime(float amount)
    {
        // Bu fonksiyon, engellerin çıkma hızını artırır (zaman aralığını kısaltır)
        // Eğer zaman minObstacleSpawnTime'dan küçük olursa onu min seviyeye sabitler
        if (obstacleSpawnTime <= minObstacleSpawnTime)
        {
            obstacleSpawnTime = minObstacleSpawnTime;
        }
        obstacleSpawnTime -= amount;
    }

    IEnumerator SpawnObstacleRoutine()
    {
        // Sonsuz döngü: Oyun boyunca engelleri sürekli üretir
        while (true)
        {
            // Random bir engel seçiyoruz
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            // X pozisyonunu rastgele belirliyoruz, Y ve Z pozisyonunu bu objeye göre ayarlıyoruz
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);

            // Bir sonraki engeli üretmeden önce belirlenen süre kadar bekle
            yield return new WaitForSeconds(obstacleSpawnTime);

            // Engeli sahneye ekle ve obstacleParent altına koy
            // Random.rotation ile engelin rastgele döndürülmesini sağlıyoruz
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
