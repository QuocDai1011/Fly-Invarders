using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public AudioClip GameOverSound;
    
    private Rigidbody2D m_rb;
    private GameController m_gameController;
    
    AudioSource audioSource;
    private UIManager m_uiManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gameController = FindObjectOfType<GameController>();
        audioSource = FindObjectOfType<AudioSource>();
        m_uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.velocity = Vector2.down * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathZone"))
        {
            //Tắt tiếng nhạc nền
            audioSource.Stop();
            audioSource.loop = false;
            
            //Tạo ra tiếng động game over
            if (audioSource && GameOverSound && !m_gameController.isGameOver())
            {
                audioSource.PlayOneShot(GameOverSound);
            }
             
            Destroy(other.gameObject);
            m_gameController.setIsGameOver(true);
            m_uiManager.ShowGameOverPanel(true);
            m_uiManager.SetPlayerScore("Your Score: " + m_gameController.getScore());
        }
    }
}
