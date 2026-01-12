using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody2D rb;
    private PlayerAwareness playerAwareness;
    private Vector2 targetDirection;
    private float _changeDirectionCooldown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwareness = GetComponent<PlayerAwareness>();
        targetDirection = transform.up;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }
    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation,  targetRotation, rotationSpeed * Time.deltaTime );
        
        rb.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        {
            rb.linearVelocity = transform.up * speed;
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
}
