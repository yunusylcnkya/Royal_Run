using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOver => gameOver; // Dışarıdan oyunun bitip bitmediğini kontrol etmek için

    [SerializeField] PlayerController playerController; // Oyuncu kontrol scripti
    [SerializeField] TMP_Text timeText;                // Ekrandaki zaman göstergesi
    [SerializeField] GameObject gameOverText;         // Oyun bittiğinde gösterilecek UI
    [SerializeField] float startTime = 5f;            // Oyunun başlangıç süresi (saniye)

    float timeLeft;     // Kalan zamanı tutar
    bool gameOver = false; // Oyunun bitip bitmediğini kontrol eder

    void Start()
    {
        timeLeft = startTime; // Başlangıçta süreyi ayarla
    }

    void Update()
    {
        DecreaseTime(); // Her frame kalan süreyi azalt
    }

    // Kalan süreyi azaltan metod
    private void DecreaseTime()
    {
        if (gameOver) return; // Eğer oyun bitti ise süreyi azaltma

        timeLeft -= Time.deltaTime; // DeltaTime kadar süreyi azalt
        timeText.text = timeLeft.ToString("F1"); // Ekranda 1 ondalık basamak göster

        if (timeLeft <= 0f) // Süre bitti mi kontrol et
        {
            PlayerGameOver(); // Oyuncu kaybetti
        }
    }

    // Oyun bittiğinde çağrılır
    private void PlayerGameOver()
    {
        gameOver = true; // Oyunun bittiğini işaretle
        gameOverText.SetActive(true); // Game Over UI göster
        Time.timeScale = 0.1f; // Oyunu yavaşlat (0.1x hız)
        playerController.enabled = false; // Oyuncu artık hareket edemez
    }

    // Oyun sırasında süreyi artırmak için kullanılabilir
    public void IncreaseTime(float time)
    {
        timeLeft += time;
    }
}
