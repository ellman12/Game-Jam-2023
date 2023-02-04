using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput pInput;
    [SerializeField]
    private GameObject attack;
    private bool attackReady, destroyTrigger;

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();

        pInput.PlayerMovement.Attack.performed += DoAttack;
    }

    private void Update()
    {
        if (attackReady)
        {
            Instantiate(attack, transform.position, Quaternion.identity);
            attackReady = false;
        }   
    }
    private void OnDisable()
    {
        pInput.PlayerMovement.Attack.performed -= DoAttack;
        pInput.Disable();
    }

    private void DoAttack(InputAction.CallbackContext c)
    {
        attackReady = true;
    }
}
