using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;
    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

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

    void Update()
    {
        originalPosition = CameraFollow.Instance.desiredPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }
        shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    IEnumerator ShakeCoroutine(float duration, float magnidute)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float strength = Mathf.Lerp(magnidute, 0f, elapsed / duration);
            Vector3 offset = Random.insideUnitSphere * strength;
            offset.z = 0; // Keep the shake in the XY plane
            transform.localPosition = originalPosition + offset;

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
