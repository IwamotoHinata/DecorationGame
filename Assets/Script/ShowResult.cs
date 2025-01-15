using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowResult : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;

    
    void Start()
    {
        int score = ScoreManager.Instance.GetScore();

        resultText.text = "Your Score is : " + score.ToString();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
          Application.Quit();
    }
}
