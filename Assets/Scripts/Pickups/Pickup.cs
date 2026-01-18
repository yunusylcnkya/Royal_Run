using UnityEngine;

// Tüm toplanabilir nesneler için temel sınıf
// Örneğin Coin, Apple gibi pickup'lar bu sınıftan türetilir
public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;  // Pickup nesnesinin dönme hızı

    void Update()
    {
        // Pickup objesini her frame Y ekseni etrafında döndür
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    // Player tag'i ile çarpışma kontrolü için sabit string
    const string playerString = "Player";

    void OnTriggerEnter(Collider other)
    {
        // Eğer çarpışan obje player ise
        if (other.CompareTag(playerString))
        {
            OnPickup();       // Pickup etkisini tetikle (alt sınıfta tanımlanır)
            Destroy(gameObject); // Pickup nesnesini sahneden kaldır
        }
    }

    // Alt sınıflarda override edilmesi gereken abstract metod
    // Örneğin Coin -> puan artırır, Apple -> hareket hızını artırır
    protected abstract void OnPickup();
}
