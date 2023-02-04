using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
	[SerializeField] private Rigidbody2D player;
	[SerializeField] private LayerMask jumpLayerMask;
	
	private bool jump, dJump;
	private PlayerInput pInput;
	
	public float jumpSpeed = 8f;

	private void Start()
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

	private void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.2f, jumpLayerMask);

		if (jump && hit.collider != null)
		{
			Debug.Log("Working");
			jump = false;
			player.velocity = new Vector2(player.velocity.x, jumpSpeed);
			dJump = true;
		}

		if (dJump && jump)
		{
			jump = false;
			player.velocity = new Vector2(player.velocity.x, jumpSpeed);
			dJump = false;
		}
	}

	private void JumpStart(InputAction.CallbackContext c)
	{
		jump = true;
	}
}