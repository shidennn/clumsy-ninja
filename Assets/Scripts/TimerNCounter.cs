using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimerNCounter : MonoBehaviour
{

    public static TimerNCounter Instance;
    public TMP_Text timerText;
    public TMP_Text parryCounterText;
    public float timer;
    public int parryCounter = 0;

    public GameObject pausemenu;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (PlayerController.Instance.onRope)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            timerText.text = $"{minutes:00}:{seconds:00}";

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                pausemenu.SetActive(!pausemenu.activeSelf);
                Time.timeScale = pausemenu.activeSelf ? 0f : 1f;
            }
        }       
    }

    public void IncrementParryCounter()
    {
        parryCounter++;
        parryCounterText.text = parryCounter.ToString();
    }
}
