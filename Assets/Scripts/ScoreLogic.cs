using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreLogic : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    public int scoreType = 1;

    private float gameTimer;

    private bool gameOver = false;

	// Use this for initialization
	void Start ()
    {
        if (scoreText == null)
            scoreText = this.GetComponent<Text>();
        if (scoreType == 1)
            scoreText.text = "Score: 0";
        else if (scoreType == 2)
            scoreText.text = "Time: 0";
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (scoreType == 2 && !gameOver)
            AddToTimer();

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void AddToScore()
    {
        score = score + 1;
        PrintScore();
    }

    public void AddToTimer()
    {
        gameTimer += Time.deltaTime;
        PrintScore();
    }

    public void PrintScore()
    {
        if (scoreType == 1)
            scoreText.text = "Score: " + score;
        else if (scoreType == 2)
            scoreText.text = "Time: " + Mathf.Floor(gameTimer);
    }

    public void GameOver()
    {
        gameOver = true;
        scoreText.text += "\nGame Over, press R to restart.";
    }
}
