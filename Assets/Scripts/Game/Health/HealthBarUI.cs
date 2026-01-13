using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForeGroundImage;
    
    public void UpdateHealthBar(HealthController healthController)
    {
        _healthBarForeGroundImage.fillAmount = healthController.RemainingHealthPercantage;
    }
}
