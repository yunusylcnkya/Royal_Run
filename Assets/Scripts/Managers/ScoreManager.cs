using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
  [SerializeField] GameManager gameManager; // Oyunun durumunu kontrol etmek için GameManager

  [SerializeField] TMP_Text scoreText; // Ekranda gösterilecek skor texti
  int score = 0; // Skorun başlangıç değeri

  // Skoru artıran metod
  public void IncreaseScore(int amount)
  {
    if (gameManager.GameOver) return; // Oyun bitti ise skoru artırma

    score += amount; // Skoru verilen miktar kadar artır
    scoreText.text = score.ToString(); // Ekrandaki yazıyı güncelle
  }
}
