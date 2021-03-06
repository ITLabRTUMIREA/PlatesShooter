﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private int score;

    public Text scoreText;
    public Text timerText;
    public Text wonText;

    public float timerLength = 60.0f;
    private float timeLeft;
    private bool stop = true;

    private float minutes;
    private float seconds;
    
    void Start ()
    {
        ResetScore();
    }
	
	void Update ()
    {
        UpdateTimer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetScore();
            StartTimer(timerLength);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScore();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "0 / 100";
        timerText.text = "waiting";
        wonText.gameObject.SetActive(false);
        stop = true;
    }

    public void StartTimer(float from)
    {
        stop = false;
        timeLeft = from;
        StartCoroutine(UpdateCoroutine());
    }

    private void UpdateTimer()
    {
        if (stop) return;
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            wonText.gameObject.SetActive(true);
            if (score >= 100)
            {
                wonText.text = "You won!";
            }
            else
            {
                wonText.text = "You loose!";
            }
        }

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
            stop = true;
            minutes = 0;
            seconds = 0;
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString() + " / 100";
    }

    private IEnumerator UpdateCoroutine()
    {
        while (!stop)
        {
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
