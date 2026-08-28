using Unity.VisualScripting;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}
