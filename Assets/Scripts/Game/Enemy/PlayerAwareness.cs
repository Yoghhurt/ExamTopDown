using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    
    public Vector2 DirectionToPlayer { get; private set; }
    
    [SerializeField] private float playerAwarenessDistance;
    
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        TryResolvePlayer();
    }

    void Update()
    {
        if (playerMovement == null)
        {
            TryResolvePlayer();
        }

        if (playerMovement == null)
        {
            AwareOfPlayer = false;
            DirectionToPlayer = Vector2.zero;
            return;
        }

        Vector2 enemyToPlayerVector = playerMovement.transform.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }

    private void TryResolvePlayer()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
}