using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private SpriteRenderer sRen;
	[Header("Movement Speed")]
	[SerializeField] private float speed = 10f;
	[SerializeField] private float sprintCoefficient = 2f;
	[SerializeField] private AnimationCurve acceleration;
	
	[Header("Wall Check")]
	[SerializeField] private LayerMask wallCheckLayerMask;
	[SerializeField] private float wallCheckRayLength;

	[Header("Step Audio")]
	[SerializeField] private AudioSource step1, step3, step4;

	private PlayerInput pInput;
	private Vector3 direction;
	private float curveIndex;
	private bool bAccel, sprinting;

	public static bool canMove = true;
	public bool canSound = true;

	private void OnEnable()
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
		if (!canMove) return;
		
		direction.x = pInput.PlayerMovement.WASD.ReadValue<Vector2>().x;
		direction.y = 0; //y not used for position
		direction.z = 0; //z never used

		if (direction.x > 0)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, wallCheckRayLength, wallCheckLayerMask);
			sRen.flipX = false;
			if (canSound) StartCoroutine(StepSound());
			if (hit.collider != null)
				return;
		}
		else if (direction.x < 0)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, wallCheckRayLength, wallCheckLayerMask);
            sRen.flipX = true;
			if (canSound) StartCoroutine(StepSound());
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

		//feel free to change this
		anim.SetFloat("MovementSpeed",pInput.PlayerMovement.WASD.ReadValue<Vector2>().magnitude);
	}

	IEnumerator StepSound()
    {
		canSound = false;
		int random = Random.Range(0, 4);
		switch(random)
        {
			case 0:
				step1.Play();
				break;
			case 1:
				step3.Play();
				break;
			case 2:
				step4.Play();
				break;
        }
		yield return new WaitForSeconds(0.35f);
		canSound = true;
    }

}