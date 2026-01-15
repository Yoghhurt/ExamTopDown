using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float _miniumSpawnTime;
    
    [SerializeField] private float _maxiumSpawnTime;

    private float _timeUntilSpawn;

    void Awake()
    {
        SetTimeUntilSpawn();
    }
    
    private void OnValidate()
    {
        if (_miniumSpawnTime < 0f)
        {
            _miniumSpawnTime = 0f;
        }

        if (_maxiumSpawnTime < _miniumSpawnTime)
        {
            _maxiumSpawnTime = _miniumSpawnTime;
        }
    }

    void Update()
    {
        if (_enemyPrefab == null)
        {
            return;
        }
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn > 0f)
        {
            return;
        }

        Instantiate(_enemyPrefab, transform.position, transform.rotation);
        SetTimeUntilSpawn();
    }
    
    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_miniumSpawnTime, _maxiumSpawnTime);
    }
}