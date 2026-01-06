using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float timeBetweenShots;
   
    [SerializeField] private float bulletSpeed;

    private bool _fireContinuously;
    private float _lastFireTime;
    private bool _fireSingle;
    
    public static Vector2 Movement;
    
    private PlayerInput _playerInput;
    private InputAction _moveAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        
        _moveAction = _playerInput.actions["Move"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        
        if (_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= timeBetweenShots)
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
