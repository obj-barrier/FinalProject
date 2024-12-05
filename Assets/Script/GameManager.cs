using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime = 180f;
    public int totalTowers = 6;
    public TextMeshProUGUI timerText;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject restartPanel;

    private float timeRemaining;
    private int remainingTowers;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = gameTime;
        UpdateTimerDisplay();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        restartPanel.SetActive(false);
        remainingTowers = totalTowers;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
            return;
        }
        if (isGameOver) 
        {
            return;
        }
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 1)
        {
            WinGame();
        }
        UpdateTimerDisplay();
    }
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Time Remaining: {minutes:00}:{seconds:00}";
    }

    public void TowerDestroyed()
    {
        remainingTowers--;
        if (remainingTowers <= 0)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0;        
        winPanel.SetActive(true);
        restartPanel.SetActive(true);
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayWinSound();
    }

    void LoseGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0;     
        losePanel.SetActive(true);
        restartPanel.SetActive(true);
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayLoseSound();
    }
    void RestartGame()
    {
        Time.timeScale = 1;  
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
