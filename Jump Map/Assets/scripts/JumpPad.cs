using UnityEngine;

public class jumppad : MonoBehaviour
{
    [Header("--- 점프대 설정 ---")]
    [Tooltip("위로 날릴 힘 (수직)")]
    public float launchForce = 15f;

    [Tooltip("앞으로 날릴 힘 (수평)")]
    public float forwardForce = 10f;

    [Tooltip("수평 속도 감속 속도 (클수록 빨리 멈춤)")]
    public float decayRate = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<StarterAssets.ThirdPersonController>();

            if (playerController != null)
            {
                LaunchPlayer(playerController);
            }
        }
    }

    private void LaunchPlayer(StarterAssets.ThirdPersonController player)
    {
        // 점프대 자체의 앞방향(Z축)으로 발사
        Vector3 forwardDir = transform.forward;
        player.LaunchFromPad(launchForce, forwardDir, forwardForce, decayRate);
    }
}