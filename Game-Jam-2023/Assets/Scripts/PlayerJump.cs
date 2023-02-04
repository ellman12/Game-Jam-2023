using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rBody;
    private PlayerInput pInput;
    private bool bJump;
    [SerializeField]
    private float vertForce;
    private BoxCollider2D hit;
    private bool bTouching = false;
    private bool forceAdded = false;

    private void Start()
    {
        bJump = false;

        pInput = new PlayerInput();
        pInput.Enable();

        pInput.PlayerMovement.Jump.performed += JumpStart;
        pInput.PlayerMovement.Jump.canceled += JumpStop;
    }
    private void OnDisable()
    {
        pInput.PlayerMovement.Jump.performed -= JumpStart;
        pInput.PlayerMovement.Jump.canceled -= JumpStop;
        pInput.Disable();
    }
    private void Update()
    {
        if (bJump && bTouching)
        {
            rBody.AddForce(new Vector2(0.0f, vertForce), ForceMode2D.Impulse);
            forceAdded = true;
        }
        forceAdded = false;
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bTouching = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        bTouching = false;
    }
    private void JumpStart(InputAction.CallbackContext c)
    {
        bJump = true;
    }
    private void JumpStop(InputAction.CallbackContext c)
    {
        bJump= false;
    }
}
