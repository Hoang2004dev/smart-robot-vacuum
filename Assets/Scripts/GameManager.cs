using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Stats")]
    private int totalDusts = 0;
    private int collectedDusts = 0;

    [Header("UI")]
    public Text winMessage;

    public Text dustCounterText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (winMessage != null)
            winMessage.gameObject.SetActive(false);
    }

    public void SetTotalDusts(int count)
    {
        totalDusts = count;
        collectedDusts = 0;
        UpdateDustCounterUI();
    }

    public void CollectDust()
    {
        collectedDusts++;
        Debug.Log($"[GameManager] Collected {collectedDusts}/{totalDusts}");

        UpdateDustCounterUI();

        if (collectedDusts >= totalDusts && winMessage != null)
        {
            winMessage.text = "You Win!";
            winMessage.gameObject.SetActive(true);
        }
    }

    private void UpdateDustCounterUI()
    {
        if (dustCounterText != null)
        {
            dustCounterText.text = $"Dust: {collectedDusts}/{totalDusts}";
        }
    }

}
