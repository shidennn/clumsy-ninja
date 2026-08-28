using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveBestRun(float currentTime, int currentParries)
    {
        float bestRunTime = PlayerPrefs.GetFloat("BestRunTime", 0f);

        if (currentTime > bestRunTime)
        {
            PlayerPrefs.SetFloat("BestRunTime", currentTime);
            PlayerPrefs.SetInt("BestRunsParryCounter", currentParries);
            PlayerPrefs.Save();
        }
    }

    public float GetBestTime()
    {
        return PlayerPrefs.GetFloat("BestRunTime", 0f);
    }

    public int GetBestRunsParryCounter()
    {
        return PlayerPrefs.GetInt("BestRunsParryCounter", 0);
    }
}
