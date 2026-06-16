using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 10;

    void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(coinValue);

            Destroy(gameObject);
        }
    }
}