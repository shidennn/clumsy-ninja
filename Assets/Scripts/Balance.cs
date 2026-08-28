using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    public static Balance Instance;
    public float balance = 50f;
    public float maxBalance = 100f;
    float minBalance = 0f;
    float balanceVelocityOnHit;
    public float damping = 200f;
    public Slider balanceSlider;
    public GameObject gameOverPanel;
    public bool isGameOver = false;

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
    }
    void Start()
    {
        balance = 50f;
        balanceSlider.value = balance;
    }
    private void Update()
    {
        
        balance += balanceVelocityOnHit * Time.deltaTime;

        balanceVelocityOnHit = Mathf.MoveTowards(balanceVelocityOnHit, 0f, damping * Time.deltaTime);
        balance = Mathf.Clamp(balance, minBalance, maxBalance);
        balanceSlider.value = balance;
        if (balance == minBalance || balance == maxBalance)
        {
            if (!isGameOver)
            {
                isGameOver = true;
                Debug.Log("Game Over");
                SaveManager.Instance.SaveBestRun(TimerNCounter.Instance.timer, TimerNCounter.Instance.parryCounter);
                Time.timeScale = 0f; // Pause the game
                gameOverPanel.SetActive(true);
            }
            
        }
    }
    public void AdjustBalance(float amount)
    {
        balance += amount * Time.deltaTime;
        balance = Mathf.Clamp(balance, minBalance, maxBalance);
    }
    public void ChangeBalanceOnProjectileHit(Vector3 hitDirection, float amount)
    {
        Vector3 localDir = transform.InverseTransformDirection(hitDirection);
        if (localDir.x > 0)
        {
            balanceVelocityOnHit -= amount;
        }
        else if (localDir.x < 0)
        {
            balanceVelocityOnHit += amount;
        }
    }
}
