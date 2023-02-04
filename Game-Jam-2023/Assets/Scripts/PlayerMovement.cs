using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput pInput;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float sprintCoefficient = 2f;

    private Vector3 direction;
    private bool sprinting;

    [SerializeField]
    private AnimationCurve acceleration;
    private float curveIndex = 0f;
    private bool bAccel;
    private bool bDecel;

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.PlayerMovement.WASD.performed += startAccel;
        pInput.PlayerMovement.WASD.canceled += stopAccel;

        pInput.PlayerMovement.Sprint.performed += StartSprint;
        pInput.PlayerMovement.Sprint.canceled += StopSprint;
    }

    private void startAccel(InputAction.CallbackContext c)
    {
        bAccel = true;
    }
    private void stopAccel(InputAction.CallbackContext c)
    {
        bAccel = false;
        curveIndex = 0f;
    }

    private void StartSprint(InputAction.CallbackContext c)
    {
        sprinting = true;
    }
    private void StopSprint(InputAction.CallbackContext c)
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

        if (bAccel)
        {
            curveIndex += Time.deltaTime;
            if (curveIndex >= 0.5f)
            {
                curveIndex = 0.5f;
            }
        }

        if (sprinting)
        {
            transform.position += direction * Time.deltaTime * speed * acceleration.Evaluate(curveIndex) * sprintCoefficient;
        }
        else
        {
            transform.position += direction * Time.deltaTime * speed * acceleration.Evaluate(curveIndex);
        }

        if(pInput.PlayerMovement.WASD.ReadValue<Vector2>().y == -1)
        {
            //fall through platform
        }
    }

}
