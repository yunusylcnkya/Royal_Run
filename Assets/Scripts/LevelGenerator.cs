using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] Transform chunkParent;

    [SerializeField] float chunkLength = 10f;


    void Start()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            float spawnPositionZ;
            if (i == 0)
            {
                spawnPositionZ = transform.position.z;
            }
            else
            {
                spawnPositionZ = transform.position.z + (i * chunkLength);
            }

            Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
            Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);

        }
    }
}

