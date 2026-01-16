using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class HealthController : MonoBehaviour
{
  [SerializeField]
  private float _currentHealth; 
  [SerializeField]
  private float _maxHealth;

  public UnityEvent OnDeath;
  
  public UnityEvent OnDamage;

  public UnityEvent OnHealthChange;
  
  public bool IsInvincible{get;set;}
  
  public float RemainingHealthPercantage
  {
      get
      {
          return _currentHealth / _maxHealth;
      }
  }
  
  public void TakeDamage(float damageAmount)
  {
      if (_currentHealth == 0)
      {
          return;
      }

      if (IsInvincible)
      {
          return;
      }
      
      _currentHealth -= damageAmount;
      
      OnHealthChange.Invoke();

      if (_currentHealth < 0)
      {
          _currentHealth = 0;
      }

      if (_currentHealth == 0)
      {
          OnDeath.Invoke();
      }
      else
      {
          OnDamage.Invoke();
      }
  }

  public void SetHealth(float amount)
  {
      _currentHealth = amount;

      if (_currentHealth > _maxHealth)
      {
          _currentHealth = _maxHealth;
      }
      
      OnHealthChange.Invoke();
  }
  public void AddHealth(float amountToAdd)
  {
      if (_currentHealth == _maxHealth)
      {
          return;
      }
      
      _currentHealth += amountToAdd;
      
      OnHealthChange.Invoke();

      if (_currentHealth > _maxHealth)
      {
          _currentHealth = _maxHealth;
      }
  }
}
