using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TMP_Text timerText;
    public GameObject gameOverText;

    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;

            gameOverText.SetActive(true);

            Time.timeScale = 0f;
        }

        timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }
}