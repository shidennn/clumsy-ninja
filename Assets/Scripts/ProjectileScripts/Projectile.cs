

using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    protected float speed;
    protected float ifPlayerParryingBalanceLoss = 40f;
    protected float normalHitBalanceLoss = 100f;
    protected bool processed = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }



    protected virtual void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    protected void MoveTowardsTarget()
    {
        Vector3 targetDirection = (PlayerController.Instance.transform.position - transform.position).normalized;

        float distanceThisFrame = speed * Time.fixedDeltaTime;

        if (Physics.Raycast(transform.position, targetDirection, out RaycastHit hit, distanceThisFrame))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (processed) return;

                processed = true;
                HandlePlayerHit();
                return;
            }

        }
        rb.linearVelocity = targetDirection * speed;
    }

    protected void HandlePlayerHit()
    {
        Vector3 incomingDirection = rb.linearVelocity.normalized;
        if (incomingDirection == Vector3.zero)
        {
            incomingDirection = (PlayerController.Instance.transform.position - transform.position).normalized;
        }

        if (PlayerController.Instance.isParrying)
        {
            ScreenShake.Instance.Shake(0.2f, 0.2f);
            Balance.Instance.ChangeBalanceOnProjectileHit(-incomingDirection, ifPlayerParryingBalanceLoss);
            Debug.Log("parry basarili");
            TimerNCounter.Instance.IncrementParryCounter();
        }
        else
        {
            Balance.Instance.ChangeBalanceOnProjectileHit(-incomingDirection, normalHitBalanceLoss);
        }

        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (processed) return;

            processed = true;
            HandlePlayerHit();
        }
    }
}
