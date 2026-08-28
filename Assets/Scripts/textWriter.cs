using TMPro;
using UnityEngine;

public class textWriter : MonoBehaviour
{

    public static textWriter Instance;
    public TMP_Text bestRunTimeText;
    public TMP_Text bestRunsParryCounterText;
    public float bestRunTime;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ShowGameOverStats();
    }

    public void ShowGameOverStats()
    {
        bestRunTime = SaveManager.Instance.GetBestTime();
        int minutes = Mathf.FloorToInt(bestRunTime / 60f);
        int seconds = Mathf.FloorToInt(bestRunTime % 60f);
        bestRunTimeText.text = $"Best Run: {minutes:00}:{seconds:00}";
        int bestRunsParryCounter = SaveManager.Instance.GetBestRunsParryCounter();
        bestRunsParryCounterText.text = $"Best Run Parries: {bestRunsParryCounter}";
    }
}
