using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    [Header("UI")]
    public GameObject clearPanel;
    public TMP_Text finalScoreText;

    [Header("Bonus")]
    public int clearBonus = 100;

    private bool isCleared = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCleared) return;

        if (other.CompareTag("Player"))
        {
            isCleared = true;

            // 클리어 보너스 지급
            GameManager.Instance.AddScore(clearBonus);

            // 최종 점수 표시
            finalScoreText.text =
                "FINAL SCORE : " + GameManager.Instance.score;

            // 클리어 UI 표시
            clearPanel.SetActive(true);

            // 게임 정지
            Time.timeScale = 0f;

            Debug.Log("GAME CLEAR!");
        }
    }
}