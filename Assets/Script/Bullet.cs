using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float timeToDestroy;
    AudioSource audioSource;
    public AudioClip deadthSound;
    public GameObject deathEffect;

    private Rigidbody2D m_rb2d;
    private GameController m_gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_gameController = FindObjectOfType<GameController>();
        audioSource = FindObjectOfType<AudioSource>();
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        m_rb2d.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            // làm ra tiếng động khi enemy bị bắn trúng
            audioSource.PlayOneShot(deadthSound);
            GameObject temp = null;
            if (deathEffect)
            {
                 temp = Instantiate(deathEffect, col.transform.position, Quaternion.identity);
                 
                 Destroy(temp, 0.5f);
            }
            
            
            m_gameController.IncreaseScore();
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        if (col.CompareTag("SceneLimit"))
        {
            Destroy(gameObject);
        }
    }
}
