using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
	[SerializeField] private new Rigidbody2D rigidbody;
	[SerializeField] private LayerMask jumpLayerMask;
	[SerializeField] private float jumpSpeed = 8f;
	[SerializeField] private int extraJumpCount;
	
	private int jumpsLeft;
	private PlayerInput pInput;
	
	private bool Grounded => Physics2D.Raycast(transform.position, -Vector2.up, 1.2f, jumpLayerMask).collider != null;

	private void Start()
	{
		pInput = new PlayerInput();
		pInput.Enable();

		pInput.PlayerMovement.Jump.performed += JumpInput;
	}

	private void OnDisable()
	{
		pInput.PlayerMovement.Jump.performed -= JumpInput;
	}

	private void Update()
	{
		if (Grounded)
			jumpsLeft = extraJumpCount;
	}

	private void JumpInput(InputAction.CallbackContext c)
	{
		if (jumpsLeft > 0)
		{
			rigidbody.velocity = Vector2.up * jumpSpeed;
			jumpsLeft--;
		}
	}
}