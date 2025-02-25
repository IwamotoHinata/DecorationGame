using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int score = 0;

    [SerializeField]
    private TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
        Invoke("ResetScore", 5);
    }

    public void IncreaseScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    public void DecreaseScore(int value)
    {
        score -= value;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }

    }

    private void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}