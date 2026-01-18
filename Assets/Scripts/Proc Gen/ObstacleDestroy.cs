using UnityEngine;

public class obstacleDestroy : MonoBehaviour
{
    // Bu fonksiyon, başka bir obje bu objeye çarptığında çalışır
    void OnTriggerEnter(Collider other)
    {
        // Çarpan objeyi sahneden kaldır (yok et)
        Destroy(other.gameObject);
    }
}
