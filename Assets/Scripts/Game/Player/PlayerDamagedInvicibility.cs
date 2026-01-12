using UnityEngine;

[SerializeField]


;

public class PlayerDamagedInvicibility : MonoBehaviour

private InvincibilityController _invincibilityController;

void Awake()
{
    _invincibilityController = GetComponent<InvincibilityController>();
}

{
    void StartInvincibility()
    {
        _invincibilityController.StartInvincibility(_invincibilityDuration);
    }
}
