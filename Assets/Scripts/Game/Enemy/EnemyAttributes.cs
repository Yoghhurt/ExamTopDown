using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Attributes", menuName = "Scriptable Objects/Enemy Attributes")]

public class EnemyAttributes : ScriptableObject
{
    [field: SerializeField]
    public float Speed { get; private set; }
    
    [field: SerializeField]
    public float RotationSpeed { get; private set; }
    
    [field: SerializeField]
    public float Health { get; private set; }
    
    [field: SerializeField]
    public float PlayerAwarenessDistance { get; private set; }
    [field: SerializeField]
    public float DamageAmount { get; private set; }
    [field: SerializeField]
    public int KillScore { get; private set; }
    [field: SerializeField]
    public float ChanceOfCollectableDrop { get; private set; }
    [field: SerializeField]
    public float ObstacleCheckCircleRadius { get; private set; }
    [field: SerializeField]
    public float ObstacleCheckDistance { get; private set; }
    [field: SerializeField]
    public LayerMask ObstacleLayer { get; private set; }
    
}
