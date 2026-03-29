using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public AudioSource audioSource;
    public AudioClip shootingSound;
    public AudioClip gameOverSound;
    
    private GameController m_gameController;
    private UIManager m_uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        m_gameController = FindObjectOfType<GameController>(); 
        m_uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gameController.IsStartGame())
        {
            return;
        }
        
        if (m_gameController.isGameOver()) return;
        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos += new Vector3(xDir, yDir, 0) * (moveSpeed * Time.deltaTime);

        pos.x = Mathf.Clamp(pos.x, -10.5f, 9.5f);
        pos.y = Mathf.Clamp(pos.y, 0f, 3.8f);

        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    
    public void Shoot()
    {
        if (bulletPrefab && bulletSpawn)
        {
            // Làm tiếng lazer khi bắn ra viên đạn
            if (audioSource && shootingSound)
            {
                audioSource.PlayOneShot(shootingSound);
            }
            
            // Khởi tạo đối tượng bullet ở vị trí bulletSpawn
            Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            // Dừng nhạc nen
            audioSource.Stop();
            audioSource.loop = false;
            //tạo tiếng động game over
            if(audioSource && gameOverSound && !m_gameController.isGameOver()) audioSource.PlayOneShot(gameOverSound);
            
            Destroy(col.gameObject);
            m_gameController.setIsGameOver(true);
            m_uiManager.ShowGameOverPanel(true);
            m_uiManager.SetPlayerScore("Your Score: " + m_gameController.getScore());
        }
    }

}
