using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhipAttack : MonoBehaviour
{
    [SerializeField] private GameObject whip;
    [SerializeField] private float offset, whipUseTime, cooldown;

    private bool cooldownActive;
    private int side = 1;
    private PlayerInput pInput;

    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        pInput.PlayerMovement.Attack.performed += AttackInput;
    }

    private void Update()
    {
        float xInput = pInput.PlayerMovement.WASD.ReadValue<Vector2>().x;
        if (xInput > 0) side = 1;
        else if (xInput < 0) side = -1;
    }
    
    private void OnDisable()
    {
        pInput.PlayerMovement.Attack.performed -= AttackInput;
        pInput.Disable();
    }

    private void AttackInput(InputAction.CallbackContext c)
    {
        if (!cooldownActive)
            StartCoroutine(RunCooldown());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(other.gameObject);
    }

    private IEnumerator RunCooldown()
    {
        PlayerMovement.canMove = false;
        cooldownActive = true;
        GameObject newWhip = Instantiate(whip, transform.position + new Vector3(offset * side, 0, 0), Quaternion.identity, transform);
        yield return new WaitForSeconds(whipUseTime);
        Destroy(newWhip);
        PlayerMovement.canMove = true;
        yield return new WaitForSeconds(cooldown);
        cooldownActive = false;
    }
}
