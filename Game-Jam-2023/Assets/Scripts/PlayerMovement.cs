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
    

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.PlayerMovement.Sprint.performed += StartSprint;
        pInput.PlayerMovement.Sprint.canceled += StopSprint;
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
    }

    private void Update()
    {
        direction.x = pInput.PlayerMovement.WASD.ReadValue<Vector2>().x;
        direction.y = 0; //y not used for position
        direction.z = 0; //z never used

        if (sprinting)
        {
            transform.position += direction * Time.deltaTime * speed * sprintCoefficient;
        }
        else
        {
            transform.position += direction * Time.deltaTime * speed;
        }

        if(pInput.PlayerMovement.WASD.ReadValue<Vector2>().y == -1)
        {
            //fall through platform
        }
    }

}
