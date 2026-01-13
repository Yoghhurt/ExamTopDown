using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody2D rb;
    private PlayerAwareness playerAwareness;
    private Animator animator;
    private Vector2 targetDirection;
    private float _changeDirectionCooldown;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwareness = GetComponent<PlayerAwareness>();
        animator = GetComponent<Animator>();
        targetDirection = transform.up;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
        UpdateAnimation();
    }
    
    private void SetVelocity()
    {
        {
            rb.linearVelocity = targetDirection.normalized * speed;
        }
    }

    private void UpdateTargetDirection()
    { 
        if (playerAwareness.AwareOfPlayer)
             {
                 targetDirection = playerAwareness.DirectionToPlayer;
             }
             
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
    }

    private void HandlePlayerTargeting()
    {
        if (playerAwareness.AwareOfPlayer)
        {
            targetDirection = playerAwareness.DirectionToPlayer;
        }
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotaion = Quaternion.AngleAxis(angleChange, transform.forward);
            targetDirection = rotaion * targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }
    
    private void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        Vector2 direction = rb.linearVelocity.normalized;
        animator.SetFloat(horizontal, direction.x);
        animator.SetFloat(vertical, direction.y);

        if (direction != Vector2.zero)
        {
            animator.SetFloat(lastHorizontal, direction.x);
            animator.SetFloat(lastVertical, direction.y);
        }
    }
}
