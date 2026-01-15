using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    private void OnTriggerStay2D(Collider2D other)
    {
        TryDealDamage(other.gameObject);
    }

    private void TryDealDamage(GameObject target)
    {
        var playerMovement = target.GetComponentInParent<PlayerMovement>()
                             ?? target.GetComponentInChildren<PlayerMovement>();

        if (playerMovement == null)
        {
            return;
        }

        var healthController = playerMovement.GetComponent<HealthController>()
                               ?? playerMovement.GetComponentInParent<HealthController>()
                               ?? playerMovement.GetComponentInChildren<HealthController>();

        if (healthController == null)
        {
            return;
        }
        healthController.TakeDamage(_damageAmount);
    }
}

