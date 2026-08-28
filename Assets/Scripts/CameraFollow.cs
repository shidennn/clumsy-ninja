using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 5f;
    public float rotationAngle = 0f;
    public float rotationSpeed = 50f;
    public float maxAngle = 45f;
    public static CameraFollow Instance;
    public Vector3 desiredPosition;
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
    // Update is called once per frame
    void LateUpdate()
    {
        if (!PlayerController.Instance.onRope)
        {
            offset = new Vector3(6f, 4f, -6f);
            smoothSpeed = 5f;
        }
        else
        {
            rotationAngle += PlayerController.Instance.lookInput.x * rotationSpeed * Time.deltaTime;
            rotationAngle = Mathf.Clamp(rotationAngle, -maxAngle, maxAngle);
            Vector3 baseOffset = new Vector3(0f, 0.8f, -2f);
            offset = Quaternion.Euler(0f, rotationAngle, 0f) * baseOffset;
            smoothSpeed = 2f;
        }
        desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(
        transform.position,
        desiredPosition,
        smoothSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}
