using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake2 : MonoBehaviour
{
    private Vector2 direction = Vector2.left;

    private List<GameObject> snakeBody;

    public GameObject snakeBodyPrefab;

    public PowerUp powerUp;
    public GameObject powerUpGameObject;
    public bool shield;
    public bool scoreBoost;
    public bool speed;

    public ScoreController2 scoreController2;
    public MultiPlayerGameOver multiPlayerGameOver;
    public Snake snake;

    float originalSpeed;
    public float moveSpeed;
    bool[] directionArrays = new bool[4];

    public GameObject gameOverUI;

    private void Start()
    {
        Time.timeScale = 0.2f;
        originalSpeed = 1f;
        moveSpeed = originalSpeed;
        direction = new Vector3(moveSpeed * (-1), 0, 0);
        directionArrays[1] = true;
        speed = false;
        shield = false;
        scoreBoost = false;
        snakeBody = new List<GameObject>();
        snakeBody.Add(this.gameObject);
    }

    private void Update()
    {
        Movemennt();
        ValidatePosition();
    }

    private void FixedUpdate()
    {
        for (int i = snakeBody.Count - 1; i > 0; i--)
        {
            snakeBody[i].transform.position = snakeBody[i - 1].transform.position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );
    }

    private void Movemennt()
    {
        if (Input.GetKeyDown(KeyCode.W) && !directionArrays[3])
        {
            direction = new Vector3(0, moveSpeed, 0);
            SetDirectionBools(0);
        }
        else if (Input.GetKeyDown(KeyCode.A) && !directionArrays[2])
        {
            direction = new Vector3(moveSpeed * (-1), 0, 0);
            SetDirectionBools(1);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !directionArrays[1])
        {
            direction = new Vector3(moveSpeed, 0, 0);
            SetDirectionBools(2);
        }
        else if (Input.GetKeyDown(KeyCode.S) && !directionArrays[0])
        {
            direction = new Vector3(0, moveSpeed * (-1), 0);
            SetDirectionBools(3);
        }
    }

    private void SetDirectionBools(int s)
    {
        for (int i = 0; i < 4; i++)
        {
            directionArrays[i] = false;
        }
        directionArrays[s] = true;
    }

    private void Grow()
    {
        GameObject segment = Instantiate(this.snakeBodyPrefab);
        segment.transform.position = snakeBody[snakeBody.Count - 1].transform.position;

        snakeBody.Add(segment);
    }

    private void Shrink()
    {
        Destroy(snakeBody[snakeBody.Count - 1]);
        snakeBody.RemoveAt(snakeBody.Count - 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FoodGainer")
        {
            AudioManager.instance.Play("FoodG");
            Grow();
            if (scoreBoost == true)
            {
                scoreController2.IncreaseScore(30);
            }
            else
            {
                scoreController2.IncreaseScore(10);
            }
        }
        if (collision.tag == "FoodBurner")
        {
            if (snakeBody.Count > 1)
            {
                AudioManager.instance.Play("FoodB");
                Shrink();
                scoreController2.DecreaseScore(10);
            }
            else
            {
                if (shield == false)
                {
                    AudioManager.instance.Play("Lose");
                    Time.timeScale = 0f;
                    gameOverUI.SetActive(true);
                    AudioManager.instance.Stop("GameTheme");
                    multiPlayerGameOver.snake1Attacked = true;
                }
            }
        }
        if (collision.tag == "PowerUp")
        {
            if (powerUp.randomNumber == 0)
            {
                speed = true;
                moveSpeed = 1.1f;
                Debug.Log("PowerUp Timer");
                scoreController2.speedUI2.SetActive(true);
            }
            if (powerUp.randomNumber == 1)
            {
                shield = true;
                Debug.Log("PowerUp Shield");
                scoreController2.shieldUI2.SetActive(true);
            }
            if (powerUp.randomNumber == 2)
            {
                scoreBoost = true;
                Debug.Log("PowerUp Score boost");
                scoreController2.scoreUI2.SetActive(true);
            }
            Debug.Log("PowerUp");
            powerUpGameObject.SetActive(false);
            StartCoroutine("powerUpSpawner");
            StartCoroutine("powerUpTimer");
        }
        if (collision.tag == "SnakeBody2")
        {
            if (shield == false)
            {
                Time.timeScale = 0f;
                gameOverUI.SetActive(true);
                AudioManager.instance.Stop("GameTheme");
                multiPlayerGameOver.snake1Attacked = true;
            }
        }
        if (collision.tag == "SnakeBody1")
        {
            if (snake.shield == false)
            {
                Time.timeScale = 0f;
                gameOverUI.SetActive(true);
                AudioManager.instance.Stop("GameTheme");
                multiPlayerGameOver.snake2Attacked = true;
            }
        }
    }

    IEnumerator powerUpTimer()
    {
        yield return new WaitForSeconds(3f);
        speed = false;
        moveSpeed = originalSpeed;
        shield = false;
        scoreBoost = false;
        scoreController2.scoreUI2.SetActive(false);
        scoreController2.shieldUI2.SetActive(false);
        scoreController2.speedUI2.SetActive(false);
    }

    IEnumerator powerUpSpawner()
    {
        yield return new WaitForSeconds(3f);
        powerUpGameObject.SetActive(true);
    }

    private void ValidatePosition()
    {
        Vector2 lowerLimit = new Vector2(-8, 1);
        Vector2 upperLimit = new Vector2(28, 19);
        if (this.transform.position.x > upperLimit.x)
        {
            this.transform.position = new Vector3(lowerLimit.x, transform.position.y, transform.position.z);
        }
        if (this.transform.position.x < lowerLimit.x)
        {
            this.transform.position = new Vector3(upperLimit.x, transform.position.y, transform.position.z);
        }
        if (this.transform.position.y > upperLimit.y)
        {
            this.transform.position = new Vector3(transform.position.x, lowerLimit.y, transform.position.z);
        }
        if (this.transform.position.y < lowerLimit.y)
        {
            this.transform.position = new Vector3(transform.position.x, upperLimit.y, transform.position.z);
        }
    }
}
