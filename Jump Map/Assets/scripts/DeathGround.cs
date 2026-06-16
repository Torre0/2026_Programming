using UnityEngine;
using StarterAssets;

public class DeathGround : MonoBehaviour
{
    public Transform RespawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThirdPersonController player =
                other.GetComponent<ThirdPersonController>();

            if (player != null)
            {
                player.Respawn(RespawnPoint.position);
            }
        }
    }
}