using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;   // Kamerayı kontrol eden script
    [SerializeField] GameObject[] chunkPrefabs;          // Oyun yolu parçaları (normal chunks)
    [SerializeField] GameObject checkpointChunkPrefab;  // Oyuncuya bonus veya ekstra zaman veren özel chunk
    [SerializeField] Transform chunkParent;             // Oyun sahnesinde chunk'ları düzenleyecek obje
    [SerializeField] ScoreManager scoreManager;         // Toplanan puanları yöneten script

    [Header("Level Settings")]
    [SerializeField] int startingChunkAmount = 12;      // Oyuna başlarken oluşturulacak chunk sayısı
    [SerializeField] int checkpointChunkInterval = 8;  // Kaç chunk sonra bir checkpoint spawn edilecek
    [Tooltip("Do not change chunk lenght value unless chunk prefab size reflects change")]
    [SerializeField] float chunkLength = 10f;          // Her chunk'ın uzunluğu
    [SerializeField] float moveSpeed = 8f;             // Oyun yolunun hızı
    [SerializeField] float minMoveSpeed = 2f;          // Minimum hız
    [SerializeField] float maxMoveSpeed = 20f;         // Maximum hız
    [SerializeField] float minGravityZ = -22f;         // Minimum yerçekimi
    [SerializeField] float maxGravityZ = -2f;          // Maximum yerçekimi

    List<GameObject> chunks = new List<GameObject>();   // Sahnedeki tüm chunk'lar burada tutuluyor
    int chunksSpawned = 0;                              // Kaç chunk spawn edildiğini takip eder

    void Start()
    {
        // Oyuna başlarken gerekli sayıda chunk oluşturulur
        SpawnStartingChunks();
    }

    void Update()
    {
        // Her frame chunk'lar hareket ettirilir
        MoveChunks();
    }

    // Chunk hızını arttırıp azaltmak için kullanılır
    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed); // Hız sınırların dışına çıkmaz

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            // Ziplama veya düşme etkisi için yerçekimini değiştiriyoruz
            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);

            // Hız değişirse kamera da zoom yapıyor
            cameraController.ChangeCameraFOV(speedAmount);
        }
    }

    // Oyuna başlarken chunk'ları spawn eder
    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            SpawnChunk();
        }
    }

    // Tek bir chunk spawn eder
    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ(); // Chunk'ı nereye koyacağını hesaplar
        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);

        // Hangi chunk spawn edilecek onu seç
        GameObject chunkToSpawn = ChoseChunkToSpawn();

        // Chunk'ı sahneye ekle
        GameObject newChunkGO = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO); // Listede tut

        // Chunk'ı başlat, böylece içinde coin, apple vs spawn edilebilir
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);

        chunksSpawned++; // Spawn edilen chunk sayısını arttır
    }

    // Hangi chunk spawn edilecek karar verir
    private GameObject ChoseChunkToSpawn()
    {
        if (chunksSpawned % checkpointChunkInterval == 0 && chunksSpawned != 0)
        {
            // Belirli aralıklarla checkpoint chunk spawn et
            return checkpointChunkPrefab;
        }
        else
        {
            // Normal chunk'lardan rastgele seç
            return chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }
    }

    // Yeni chunk spawn etmek için Z pozisyonunu hesaplar
    private float CalculateSpawnPositionZ()
    {
        if (chunks.Count == 0)
        {
            return transform.position.z; // Eğer hiç chunk yoksa baştan başla
        }
        else
        {
            // Son chunk'ın arkasına spawn et
            return chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }
    }

    // Chunk'ları hareket ettirir, ekranın arkasına gelirse yok eder ve yeni chunk spawn eder
    private void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * moveSpeed * Time.deltaTime); // Z ekseninde geri hareket

            // Kamera arkasına geçtiyse chunk'ı yok et
            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk); // Listeden çıkar
                Destroy(chunk);        // Sahneden kaldır

                SpawnChunk();          // Yeni chunk spawn et
            }
        }
    }
}
