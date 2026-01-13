using UnityEngine;

public class HealthPotBehavior : MonoBehaviour, CollectableBehavior
{
    [SerializeField] private float _healthAmount;
    
    public void OnCollected(GameObject player)
    {
        player.GetComponent<HealthController>().AddHealth(_healthAmount);
    }
}
