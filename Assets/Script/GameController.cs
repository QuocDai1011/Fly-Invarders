using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime;
    public GameObject StartGamePanel;

    private float m_spawnTime;
    int m_score;
    bool m_isGameOver;
    UIManager m_uiManager;
    private bool m_isStart;
    
    // Start is called before the first frame update
    void Start()
    {
        m_spawnTime = 0;
        m_uiManager = FindObjectOfType<UIManager>();
        m_uiManager.setScore("Score: 0");
        m_isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && !m_isStart)
        {
            StartGame();
        }
        
        if (!m_isStart) return;
        
        if (m_isGameOver)
        {
            m_spawnTime = 0;
            return;
        }
        
        m_spawnTime -= Time.deltaTime;

        if (m_spawnTime <= 0)
        {
            SpawnEnemy();
            m_spawnTime = spawnTime;
        }
    }

    public void SpawnEnemy()
    {
        // position spawn: x(-9.5, 9.5), y(6.5, 6.5)
        float xPos = Random.Range(-9f, 9f);
        float yPos = 6.5f;
        Vector2 pos = new Vector2(xPos, yPos);

        if (enemy)
        {
            Instantiate(enemy, pos, Quaternion.identity);
        }
    }

    public void setScore(int score)
    {
        m_score = score;
    }

    public int getScore()
    {
        return m_score;
    }

    public void IncreaseScore()
    {
        if (isGameOver()) return;
        if (m_score % 4 == 0 && m_score != 0)
        {
            if(spawnTime > 1) spawnTime -= 0.6f;
        }
        m_score++;
        m_uiManager.setScore("Score: " + m_score);
    }

    public void setIsGameOver(bool isGameOver)
    {
        m_isGameOver = isGameOver;
    }

    public bool isGameOver()
    {
        return m_isGameOver;
    }

    public void Replay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public bool IsStartGame()
    {
        return m_isStart;
    }
    
    public void StartGame()
    {
        m_isStart = true;
        StartGamePanel.SetActive(false);
    }
}
