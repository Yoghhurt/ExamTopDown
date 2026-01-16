using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyAttributes _enemyAttributes;
    
    private Rigidbody2D rb;
    private PlayerAwareness playerAwareness;
    private Animator animator;
    private Vector2 targetDirection;
    private float _changeDirectionCooldown;
    private RaycastHit2D[] _obstacleCollisions; 
    private float _obstacleAvoidanceCooldown;
    private Vector2 _obstacleAvoidanceTargetDirection;
    
    

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
        _obstacleCollisions = new RaycastHit2D[10];
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
            rb.linearVelocity = targetDirection.normalized * _enemyAttributes.Speed;
        }
    }


    private void HandlePlayerTargeting()
    {
        if (playerAwareness.AwareOfPlayer)
        {
            targetDirection = playerAwareness.DirectionToPlayer;
        }
    }
    
    private void UpdateTargetDirection()
    { 
        if (playerAwareness.AwareOfPlayer)
        {
            targetDirection = playerAwareness.DirectionToPlayer;
        }
             
        HandleRandomDirectionChange();
        HandleObstacles();
        HandlePlayerTargeting();
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

    private void HandleObstacles()
    {
        _obstacleAvoidanceCooldown -= Time.deltaTime;

        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_enemyAttributes.ObstacleLayer);

        int numberOfCollisions = Physics2D.CircleCast(transform.position,
            _enemyAttributes.ObstacleCheckCircleRadius,
            targetDirection,
            contactFilter, _obstacleCollisions,
            _enemyAttributes.ObstacleCheckDistance);

        for (int index = 0; index < numberOfCollisions; index++)
        {
            var obstacleCollisions = _obstacleCollisions[index];

            if (obstacleCollisions.collider.gameObject == gameObject)
            {
                continue;
            }

            if (_obstacleAvoidanceCooldown <= 0)
            {
                _obstacleAvoidanceCooldown = 0.5f;
            }

            var targetRotation = Quaternion.LookRotation(transform.forward, obstacleCollisions.normal);
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _enemyAttributes.RotationSpeed * Time.deltaTime);

            targetDirection = rotation * Vector2.up;
            break;
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