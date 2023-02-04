using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement Speed")]
	[SerializeField] private float speed = 10f;
	[SerializeField] private float sprintCoefficient = 2f;
	[SerializeField] private AnimationCurve acceleration;
	
	[Header("Wall Check")]
	[SerializeField] private LayerMask wallCheckLayerMask;
	[SerializeField] private float wallCheckRayLength;

	private PlayerInput pInput;
	private Vector3 direction;
	private float curveIndex;
	private bool bAccel, sprinting, canMove;

	private void Start()
	{
		pInput = new PlayerInput();
		pInput.Enable();

		pInput.PlayerMovement.WASD.performed += startAccel;
		pInput.PlayerMovement.WASD.canceled += stopAccel;

		pInput.PlayerMovement.Sprint.performed += StartSprint;
		pInput.PlayerMovement.Sprint.canceled += StopSprint;
	}

	private void startAccel(InputAction.CallbackContext _)
	{
		bAccel = true;
	}

	private void stopAccel(InputAction.CallbackContext _)
	{
		bAccel = false;
		curveIndex = 0f;
	}

	private void StartSprint(InputAction.CallbackContext _)
	{
		sprinting = true;
	}

	private void StopSprint(InputAction.CallbackContext _)
	{
		sprinting = false;
	}

	private void OnDisable()
	{
		pInput.Disable();

		pInput.PlayerMovement.WASD.performed -= startAccel;
		pInput.PlayerMovement.WASD.canceled -= stopAccel;

		pInput.PlayerMovement.Sprint.performed -= StartSprint;
		pInput.PlayerMovement.Sprint.canceled -= StopSprint;
	}

	private void Update()
	{
		direction.x = pInput.PlayerMovement.WASD.ReadValue<Vector2>().x;
		direction.y = 0; //y not used for position
		direction.z = 0; //z never used

		if (direction.x > 0)
		{
			Debug.DrawRay(transform.position, Vector3.right * wallCheckRayLength, Color.cyan);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, wallCheckRayLength, wallCheckLayerMask);
			if (hit.collider != null)
				return;
		}
		else if (direction.x < 0)
		{
			Debug.DrawRay(transform.position, Vector3.left * wallCheckRayLength, Color.green);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, wallCheckRayLength, wallCheckLayerMask);
			if (hit.collider != null)
				return;
		}

		if (bAccel)
		{
			curveIndex += Time.deltaTime;
			if (curveIndex >= 0.5f)
			{
				curveIndex = 0.5f;
			}
		}

		if (sprinting)
			transform.position += direction * (Time.deltaTime * speed * acceleration.Evaluate(curveIndex * sprintCoefficient));
		else
			transform.position += direction * (Time.deltaTime * speed * acceleration.Evaluate(curveIndex));
	}
}