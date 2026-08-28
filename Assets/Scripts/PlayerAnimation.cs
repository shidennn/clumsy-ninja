using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    protected Rigidbody rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        //Debug.Log(animator);
    }

    private void Update()
    {
        animator.SetFloat("Speed", PlayerController.Instance.moveInput.magnitude);
        animator.SetBool("onRope", PlayerController.Instance.onRope);
        animator.SetBool("isParrying", PlayerController.Instance.isParrying);
    }
}
