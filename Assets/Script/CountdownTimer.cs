using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text countdownText;

    [SerializeField]
    private float countdownTime = 5f; // Countdown starts at 5 seconds

    private bool isCountingDown = true;


    void Start()
    {
        
    }

    void Update()
    {
        if (isCountingDown)
        {
            countdownTime -= Time.deltaTime;
            countdownTime = Mathf.Max(countdownTime, 0); // Ensure time doesn't go below 0

            // Update the UI text
            if (countdownText != null)
            {
                countdownText.text = countdownTime > 0 
                    ? $"Time: {countdownTime:F2}" 
                    : "The flower has wilted!";  
            }

            // Stop counting and check flower status when the timer ends
            if (countdownTime <= 0 && isCountingDown)
            {
                OnCountdownEnd();
            }
        }
    }

    public void OnCountdownEnd()
    {
        isCountingDown = false;
        Debug.Log($"{gameObject.name}: Countdown has ended.");
        countdownText.gameObject.SetActive(false);                   
    }

    
}
