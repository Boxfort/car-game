using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIScript : MonoBehaviour {

    private GameObject pausePanel;
    private GameObject gameOverPanel;
    public GameObject[] uiToPause;

    private int score;
    private int collectables;
    private int collectablesTotal;
    private Text scoreText;
    private Text collectText;


    //TODO: COLLECTABLE COUNTING

    // Use this for initialization
    void Start()
    {
        score = 0;
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        collectText = GameObject.FindGameObjectWithTag("CollectableText").GetComponent<Text>();
        pausePanel = GameObject.Find("Pause Panel");
        gameOverPanel = GameObject.Find("GameOver Panel");

        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        collectablesTotal = PlayerPrefs.GetInt("collectables", 0);
        collectText.text = collectablesTotal.ToString("D3");

        InvokeRepeating("IncreaseScore", 1.0f, 1.0f);
        EventManagerScript.OnCollectableGetMethods += IncreaseCollectables;
        EventManagerScript.OnPlayerDestroyedMethods += GameOver;       
    }

    void OnDestroy()
    {
        //Remove methods from events so that when the scene is re-loaded the event manager does not contain null references.
        EventManagerScript.OnCollectableGetMethods -= IncreaseCollectables;
        EventManagerScript.OnPlayerDestroyedMethods -= GameOver;
    }

    public void PauseGame()
    {
        GameManagerScript.PauseGame();

        Time.timeScale = 0;
        pausePanel.SetActive(true);

        foreach(GameObject obj in uiToPause)
        {
            obj.GetComponent<Button>().enabled = false;
        }
    }

    public void UnPauseGame()
    {
        GameManagerScript.UnPauseGame();

        Time.timeScale = 1;
        pausePanel.SetActive(false);

        foreach (GameObject obj in uiToPause)
        {
            obj.GetComponent<Button>().enabled = true;
        }
    }

    public void RestartGame()
    {
        GameManagerScript.UnPauseGame();
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    void GameOver()
    {
        if (GameManagerScript.State == GameState.GameOver)
            return;

        GameManagerScript.GameOver();
        Time.timeScale = 0;
        foreach (GameObject obj in uiToPause)
        {
            obj.GetComponent<Button>().enabled = false;
        }

        if(score > PlayerPrefs.GetInt("bestScore", 0))
        {
            PlayerPrefs.SetInt("bestScore", score);
        }

        PlayerPrefs.SetInt("collectables", collectablesTotal + collectables);

        PlayerPrefs.Save();

        gameOverPanel.SetActive(true);

        GameObject.FindGameObjectWithTag("FinalScore").GetComponent<Text>().text = score.ToString();
        GameObject.FindGameObjectWithTag("BestScore").GetComponent<Text>().text = PlayerPrefs.GetInt("bestScore").ToString();
        GameObject.Find("CollectablesFound").GetComponent<Text>().text = "You Found " + collectables.ToString() + " car parts";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void IncreaseScore()
    {
        if (GameManagerScript.State != GameState.Running)
            return;

        score++;
        scoreText.text = score.ToString();
    }

    void IncreaseCollectables()
    {
        if (GameManagerScript.State != GameState.Running)
            return;

        collectables++;

        collectText.text = (collectablesTotal + collectables).ToString("D3");
    }
}
