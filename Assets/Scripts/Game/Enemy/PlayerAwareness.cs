using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    
    public Vector2 DirectionToPlayer { get; private set; }
    
    [SerializeField] private float playerAwarenessDistance;
    
    private Transform _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
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
}