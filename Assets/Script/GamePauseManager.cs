using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    private bool paused = false;
    private GameController m_gameController;

    private void Start()
    {
        m_gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void ResumeGame()
    {
        paused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GamePlay");
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
