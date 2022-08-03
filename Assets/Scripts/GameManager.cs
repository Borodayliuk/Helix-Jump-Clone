using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int best;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
        best = PlayerPrefs.GetInt("Highscore");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<PlayerController>().RestartBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);

    }
    public void RestartLevel()
    {
        singleton.score = 0;
        FindObjectOfType<PlayerController>().RestartBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }
    public void AddScore(int scoreAdd)
    {
        score += scoreAdd;

        if(score > best)
        {
            best = score;
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
