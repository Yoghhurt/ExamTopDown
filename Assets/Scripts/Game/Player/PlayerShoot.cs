using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShoot : MonoBehaviour
{
   [SerializeField] private GameObject bulletPrefab;

   [SerializeField] private float _timeBetweenShots;
   
   [SerializeField] private float bulletSpeed;

   private bool _fireContinuously;
   private float _lastFireTime;
   private bool _fireSingle;

   void Update()
   {
      if (_fireContinuously || _fireSingle)
      {
         float timeSinceLastFire = Time.time - _lastFireTime;

         if (timeSinceLastFire >= _timeBetweenShots)
         {
            FireBullet();
                     
            _lastFireTime = Time.time;
            _fireSingle = false;
         }
         
      }
   }

   private void FireBullet()
   {
      GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
      Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
      
      rb.linearVelocity = bulletSpeed * transform.up;
   }
   
   private void OnAttack(InputValue inputValue)
   {
      _fireContinuously = inputValue.isPressed;

      if (inputValue.isPressed)
      {
         _fireSingle = true;
      }
   }
}
