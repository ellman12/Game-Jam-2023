using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump_v2 : MonoBehaviour
{
    [SerializeField]
    private Transform posA, posB;
    private PlayerInput pInput;
    private Vector2 playerPos, yOffset;
    private bool jumpReady, touching, jumpHappening;
    [SerializeField]
    private float jumpSpeed;

    private void Start()
    {
        posA.position = transform.position;
        posB.position = transform.position + new Vector3(0, 10, 0);
        jumpReady = false;
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
        playerPos.x = transform.position.x;
        posA.position = playerPos;
        posB.position = playerPos + new Vector2(0, 10);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.2f);

        if (hit.collider != null)
        {
            posA.position = transform.position;
            posB.position = transform.position + new Vector3(0, 10, 0);
            touching = true;
        }

        if(jumpReady)
            jumpHappening = true;


        if(jumpHappening && touching)
        {
            transform.position = Vector2.Lerp(posA.position, posB.position, jumpSpeed * Time.deltaTime);
            Debug.Log("It's working");
/*            touching = false;
            jumpHappening = false;*/
        }
    }
    private void JumpStart(InputAction.CallbackContext c)
    {
        jumpReady = true;
    }
    private void JumpStop(InputAction.CallbackContext c)
    {
        jumpReady = false;
    }
}
