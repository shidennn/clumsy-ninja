using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Restart button clicked");
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
