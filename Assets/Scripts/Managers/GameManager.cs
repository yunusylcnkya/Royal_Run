using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOver => gameOver;

    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver = false;
    void Start()
    {
        timeLeft = startTime;
    }


    void Update()
    {
        DecreaseTime();

    }
    private void DecreaseTime()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");


        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    private void PlayerGameOver()
    {
        gameOver = true;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
        playerController.enabled = false;
    }
    public void IncreaseTime(float time)
    {
        timeLeft += time;
    }
}
