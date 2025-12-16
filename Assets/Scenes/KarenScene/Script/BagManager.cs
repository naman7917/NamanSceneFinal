using UnityEngine;
using TMPro;

public class BagManager : MonoBehaviour
{
    public static BagManager Instance;

    [Header("UI text that shows the count")]
    public TMP_Text bagCountText;

    private int bagCount = 0;

    void Awake()
    {
        // Simple singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        UpdateUI();
    }

    public void AddItems(int amount)
    {
        bagCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (bagCountText != null)
        {
            bagCountText.text = bagCount.ToString();
        }
    }
}
