using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTest : MonoBehaviour
{
 

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null)
        {
            Debug.Log(Mouse.current.position.ReadValue());
        }
        else
        {
            Debug.Log("Mouse not detected");
        }
    }
}
