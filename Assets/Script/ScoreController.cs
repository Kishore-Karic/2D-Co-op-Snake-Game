using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int score = 0;

    public Snake snake;

    public GameObject shieldUI;
    public GameObject scoreUI;
    public GameObject speedUI;

    private void Start()
    {
        shieldUI.SetActive(false);
        scoreUI.SetActive(false);
        speedUI.SetActive(false);
        RefreshUI();
    }

    private void Awake()
    {
        //scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        RefreshUI();
        PowerUpUI();
    }

    public void IncreaseScore(int increament)
    {
        score += increament;
        RefreshUI();
    }

    public void DecreaseScore(int decreament)
    {
        score -= decreament;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if(score < 0)
        {
            score = 0;
        }
        scoreText.text = "" + score;
    }

    private void PowerUpUI()
    {
        if(snake.speed == true)
        {
            speedUI.SetActive(true);
        }
        else if(snake.speed == false)
        {
            speedUI.SetActive(false);
        }
        else if(snake.shield == true)
        {
            shieldUI.SetActive(true);
        }
        else if(snake.shield == false)
        {
            shieldUI.SetActive(false);
        }
        else if(snake.scoreBoost == true)
        {
            scoreUI.SetActive(true);
        }
        else if(snake.scoreBoost == false)
        {
            scoreUI.SetActive(false);
        }
    }
}