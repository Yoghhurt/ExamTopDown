using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    
    private Rigidbody2D rigidbody;
    private Vector2 movementInput;
    private Vector2 smoothMovementInput;
    private Vector2 movementUnputSmoothVelocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        smoothMovementInput = Vector2.SmoothDamp(smoothMovementInput,
            movementInput, ref movementUnputSmoothVelocity,
            0.1f);
        
        rigidbody.linearVelocity = smoothMovementInput * speed;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
}
