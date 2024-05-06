using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Transform player;
    private GameObject gameOverUI;
    private GameObject pausedGameUI;
    private GameObject pauseButton;
    TextMeshProUGUI scoreText;
    public bool isPaused;
    public int score = 0;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        pauseButton = GameObject.Find("PauseGameButton");
        pausedGameUI = GameObject.Find("PausedGameUI");
        gameOverUI = GameObject.Find("GameOverUI");
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
        PrepareUI();
    }

    private void PrepareUI()
    {
        pausedGameUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool playerAlive = player.GetComponent<Human>().isAlive;
        if (!playerAlive)
        {
            gameOverUI.SetActive(true);
        }
        scoreText.text = score.ToString();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pausedGameUI.SetActive(true);
        pauseButton.gameObject.SetActive(false);

    }

    public void PlayerDie()
    {
        pauseButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseButton.gameObject.SetActive(true);
        pausedGameUI.SetActive(false);
    }
}
