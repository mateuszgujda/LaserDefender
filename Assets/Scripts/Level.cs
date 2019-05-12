using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{

    [SerializeField] float delayInSeconds=2f;
    [SerializeField] float Score = 0f;
    [SerializeField] TextMeshProUGUI scoreText= null;
    [SerializeField] TextMeshProUGUI healthText = null;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Menu");

    }


    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        
    }

    private void Start()
    {
        Score = 0f;

        if (scoreText)
        {
            scoreText.text = Score.ToString();
        }
    }
    public void LoadOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void IncreaseScore(float toAdd)
    {
        Score += toAdd;
        scoreText.text = Score.ToString();
    }

    public void DisplayHealth()
    {
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            healthText.text = player.getHealth().ToString();
        }
    }

}
