using System;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemyAttributes _enemyAttributes;
    
    private HealthController _healthController;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    void Start()
    {
        _healthController.SetHealth(_enemyAttributes.Health);
    }
}
