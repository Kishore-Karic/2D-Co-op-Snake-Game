using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiPlayerGameOver : MonoBehaviour
{
    public bool snake1Attacked;
    public bool snake2Attacked;

    public ScoreController scoreController;
    public ScoreController2 scoreController2;

    public TextMeshProUGUI player1Score;
    public TextMeshProUGUI player2Score;

    public GameObject gameOverUI;
    public GameObject player1Win;
    public GameObject player2Win;

    public Button pauseButton, resumeButton, homeButton, restartButton;

    void Start()
    {
        gameOverUI.SetActive(false);
        player1Win.SetActive(false);
        player2Win.SetActive(false);
    }

    private void Awake()
    {
        pauseButton.onClick.AddListener(PauseButton);
        resumeButton.onClick.AddListener(ResumeButton);
        homeButton.onClick.AddListener(Mainmenu);
        restartButton.onClick.AddListener(ReloadLevel);
    }

    void Update()
    {
        RefreshUI();
        RefreshWin();
    }

    public void RefreshUI()
    {
        player1Score.text = "" + scoreController.score;
        player2Score.text = "" + scoreController2.score;
    }

    public void RefreshWin()
    {
        AudioManager.instance.Play("Win");
        if(snake1Attacked == true)
        {
            player1Win.SetActive(true);
        }
        if(snake2Attacked == true)
        {
            player2Win.SetActive(true);
        }
    }

    public void GameOver()
    {
        RefreshUI();
    }

    public void ResumeButton()
    {
        AudioManager.instance.Play("GameTheme");
        AudioManager.instance.Play("ButtonClick");
        Time.timeScale = 0.2f;
        gameOverUI.SetActive(false);
    }

    public void PauseButton()
    {
        AudioManager.instance.Stop("GameTheme");
        AudioManager.instance.Play("ButtonClick");
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void ReloadLevel()
    {
        AudioManager.instance.Play("ButtonClick");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void Mainmenu()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(0);
    }
}
