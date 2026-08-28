using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    Renderer renderer;

    public bool onRope = false;
    public bool isParrying = false;
    public float moveSpeed = 10f;
    public float balanceSpeed = 15f;
    public float maxBalanceSpeed = 40f;
    public float balanceAcceleration = 20f;
    public int lastDirection = 0;
    protected Rigidbody rb;
    public Vector2 moveInput;
    private PlayerControls controls;
    private Camera cam;
    public Vector2 lookInput;
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
        controls = new PlayerControls();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //Debug.Log(PlayerController.Instance.name);
    }
    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Parry.performed += onParry;
        controls.Player.Look.performed += onLook;
        controls.Player.Look.canceled += onLookCanceled;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMoveCanceled;
        controls.Player.Parry.performed -= onParry;
        controls.Player.Look.performed -= onLook;
        controls.Player.Look.canceled -= onLookCanceled;

        controls.Disable();
    }

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void onParry(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Parry();
    }

    private void onLook(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        //Debug.Log("Look Input: " + lookInput);
    }

    private void onLookCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        lookInput = Vector2.zero;
    }
    // Update is called once per frame
    private void Update()
    {

        if (!onRope)
        {
            Move();
        }

        int direction = 0;
        if (onRope)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                direction = -1;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                direction = 1;
            }

            if (direction != 0 && direction != lastDirection)
            {
                balanceSpeed = 20f;
            }

            if (direction != 0)
            {
                balanceSpeed = Mathf.Min(balanceSpeed + balanceAcceleration * Time.deltaTime, maxBalanceSpeed);
                Balance.Instance.AdjustBalance(direction * balanceSpeed);
                //Debug.Log("balanceSpeed:" + balanceSpeed + "balanceacc:" + balanceAcceleration);
            }
            else
            {
                balanceSpeed = 20f;
                //Debug.Log("balanceSpeed:" + balanceSpeed);
            }
            lastDirection = direction;
        }
    }

    public void Move()
    {
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = camForward * moveInput.y + camRight * moveInput.x;
        
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                20f * Time.deltaTime
            );
        }
        transform.position += direction * moveSpeed * Time.deltaTime;
    }



    public void Parry()
    {
        Debug.Log("Parry action triggered.");
        StartCoroutine(ParryCoroutine());
        Debug.Log("onrope:" + onRope);
    }
    IEnumerator ParryCoroutine()
    {
        Debug.Log(isParrying);
        isParrying = true;
        Debug.Log(isParrying);
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        renderer.material.color = Color.white;
        isParrying = false;
        Debug.Log(isParrying);
    }
}
