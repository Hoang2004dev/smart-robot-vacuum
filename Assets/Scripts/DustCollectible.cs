using UnityEngine;
using UnityEngine.UI;

public class DustCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance?.CollectDust();
            Destroy(gameObject);
        }
    }
}
