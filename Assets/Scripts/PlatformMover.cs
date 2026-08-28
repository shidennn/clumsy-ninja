using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
    }

    //this script is only for start area.
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.onRope)
        {
            transform.Translate(Vector3.back * Time.deltaTime * 4f);
            Destroy(gameObject, 5f);
        }
    }
}
