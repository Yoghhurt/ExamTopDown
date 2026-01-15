using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private float _obstacleCircleCastDistance;
    [SerializeField] private float _obstacleCircleCastRadius;
    [SerializeField] private LayerMask _obstacleLayerMask;

    private Rigidbody2D rb;

    private PlayerAwareness playerAwareness;

    //private Animator animator;
    private Vector2 targetDirection;
    private float _changeDirectionCooldown;
    private RaycastHit2D[] _obstacleCollisions;
    private float _obstacleAvoidanceCooldown;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwareness = GetComponent<PlayerAwareness>();
        //animator = GetComponent<Animator>();
        targetDirection = transform.up;
        _obstacleCollisions = new RaycastHit2D[10];
    }

    private void FixedUpdate()
    {
        SetVelocity();
        //UpdateAnimation();
    }

    private void SetVelocity()
    {
        {
            rb.linearVelocity = targetDirection.normalized * speed;
        }
    }


    private void HandlePlayerTargeting()
    {
        if (playerAwareness.AwareOfPlayer)
        {
            targetDirection = playerAwareness.DirectionToPlayer;
        }
        else
        {
            HandleRandomDirectionChange();
        }

        HandleObstacles();
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
        contactFilter.SetLayerMask(_obstacleLayerMask);

        int numberOfCollisions = Physics2D.CircleCast(transform.position,
            _obstacleCircleCastRadius,
            targetDirection,
            contactFilter, _obstacleCollisions,
            _obstacleCircleCastDistance);

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
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            targetDirection = rotation * Vector2.up;
            break;
        }
    }
}

/*private void UpdateAnimation()
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
}*/
