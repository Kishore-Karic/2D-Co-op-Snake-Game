using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController2 : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int score = 0;

    public Snake2 snake2;

    public GameObject shieldUI2;
    public GameObject scoreUI2;
    public GameObject speedUI2;

    private void Start()
    {
        shieldUI2.SetActive(false);
        scoreUI2.SetActive(false);
        speedUI2.SetActive(false);
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
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = "" + score;
    }

    private void PowerUpUI()
    {
        if (snake2.speed == true)
        {
            speedUI2.SetActive(true);
        }
        else if (snake2.speed == false)
        {
            speedUI2.SetActive(false);
        }
        else if (snake2.shield == true)
        {
            shieldUI2.SetActive(true);
        }
        else if (snake2.shield == false)
        {
            shieldUI2.SetActive(false);
        }
        else if (snake2.scoreBoost == true)
        {
            scoreUI2.SetActive(true);
        }
        else if (snake2.scoreBoost == false)
        {
            scoreUI2.SetActive(false);
        }
    }
}
