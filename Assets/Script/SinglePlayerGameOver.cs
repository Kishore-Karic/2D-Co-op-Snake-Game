using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SinglePlayerGameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    public ScoreController scoreController;

    public TextMeshProUGUI totalScoreText;

    public Button pauseButton, resumeButton, homeButton, restartButton;

    void Start()
    {
        gameOverUI.SetActive(false);
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
    }

    private void RefreshUI()
    {
        totalScoreText.text = "" + scoreController.score;
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
