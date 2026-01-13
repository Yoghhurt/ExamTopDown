using UnityEngine;

public class EnemyCollectableDrop : MonoBehaviour
{
    [SerializeField]
    private float _chanceOfDrop;
    
    private CollectableSpawner _collectableSpawner;

    private void Awake()
    {
        _collectableSpawner = FindAnyObjectByType<CollectableSpawner>();
    }

    public void RandomDropCollectable()
    {
        float random = Random.Range(0f, 1f);

        if (_chanceOfDrop >= random)
        {
            _collectableSpawner.SpawnCollectable(transform.position);
        }
    }
}
