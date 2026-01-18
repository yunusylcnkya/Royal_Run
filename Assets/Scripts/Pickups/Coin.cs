using UnityEngine;

// Coin, Pickup sınıfından türetilmiş bir nesnedir
// Toplandığında oyuncuya puan kazandırır
public class Coin : Pickup
{
    [SerializeField] int scoreAmount = 100;  // Bu coin toplandığında kazanılacak puan miktarı
    ScoreManager scoreManager;               // Puanı artırmak için ScoreManager referansı

    // Coin spawn edildiğinde ScoreManager referansı atanır
    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }

    // Coin toplandığında tetiklenen fonksiyon
    protected override void OnPickup()
    {
        // ScoreManager üzerinden oyuncunun skorunu artır
        scoreManager.IncreaseScore(scoreAmount);
    }
}
