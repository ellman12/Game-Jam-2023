using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed = 8f;
    private float direction = 0f;
    [SerializeField]
    private Rigidbody2D player;
    private bool jump = false;
    private PlayerInput pInput;
    [SerializeField]
    private LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        player = GetComponent<Rigidbody2D>();

        pInput.PlayerMovement.Jump.performed += JumpStart;
    }

    private void OnDisable()
    {
        pInput.PlayerMovement.Jump.performed -= JumpStart;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.2f);
        direction = Input.GetAxis("Horizontal");

        if (jump && hit.collider != null)
        {
            Debug.Log("Working");
            jump = false;
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
    }
    private void JumpStart(InputAction.CallbackContext c)
    {
        jump = true;
    }
}