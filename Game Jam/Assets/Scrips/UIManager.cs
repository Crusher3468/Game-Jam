using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Slider healthMeter;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] TMP_Text timeUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject victoryUI;
    
    public void ShowTitle(bool show)
    {
        titleUI.SetActive(show);
    }

    public void ShowGameOver(bool show)
    {
        gameOverUI.SetActive(show);
    }

    public void ShowVictory(bool show)
    {
        victoryUI.SetActive(show);
    }

    public void SetHealth(int health)
    {
        healthMeter.value = Mathf.Clamp(health, 0, 100);
    }

    public void SetScore(int score)
    {
        Debug.Log(score);
        scoreUI.text = score.ToString();
    }

    public void SetLives(int lives)
    {
        livesUI.text = "Lives: " + lives.ToString();
    }

    public void SetTime(float statetime)
    {
        timeUI.text = statetime.ToString("F2");
    }
}
