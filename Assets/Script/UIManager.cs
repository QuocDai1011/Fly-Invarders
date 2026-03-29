using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public GameObject GameOverPanel;
    public TextMeshProUGUI PlayerScoreText;

    public void setScore(string score)
    {
        if (ScoreText)
        {
            ScoreText.text = score;
        }
    }

    public void ShowGameOverPanel(bool isShow)
    {
        if(GameOverPanel) GameOverPanel.SetActive(isShow);
    }

    public void SetPlayerScore(string score)
    {
        if(PlayerScoreText) PlayerScoreText.text = score;
    }
}
