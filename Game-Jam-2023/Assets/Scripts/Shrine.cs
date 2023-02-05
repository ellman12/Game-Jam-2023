using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shrine : MonoBehaviour
{
    public bool bIsCleansed;
    private PlayerInput pInput;

    [Header("Shrine")]
    [SerializeField]
    private Sprite CleansedSprite;
    [SerializeField]
    private SpriteRenderer shrineSprite;

    [Header("Prompt")]
    [SerializeField]
    private GameObject interactPrompt;
    private SpriteRenderer eSprite;
    private bool canPurify;

    [Header("Customization")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float detectionDistance = 3;
    private Ray2D range;
    private RaycastHit2D hit;
    [SerializeField]
    private LayerMask layer;

    [Header("Audio")]
    [SerializeField]
    private AudioSource cleanse;
    private void Start()
    {
        pInput = new PlayerInput();
        pInput.Enable();
        eSprite = interactPrompt.GetComponent<SpriteRenderer>();
        eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 0);

        bIsCleansed = false;
        pInput.PlayerMovement.Interact.performed += PurifyShrine;
    }

    private void OnDisable()
    {
        pInput.Disable();
        pInput.PlayerMovement.Interact.performed -= PurifyShrine;
    }

    private void Update()
    {
        range.direction = target.position - transform.position;
        range.origin = transform.position;
        Debug.DrawRay(range.origin, range.direction.normalized * detectionDistance, Color.red);
        hit = Physics2D.Raycast(transform.position, target.position - transform.position, detectionDistance, layer);

        if (hit.collider != null && !bIsCleansed)
        {
            eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 1-hit.fraction);
            canPurify = true;
        } else
        {
            eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 0);
            canPurify = false;
        }
    }

    private void PurifyShrine(InputAction.CallbackContext c)
    {
        if (canPurify)
        {
            cleanse.Play();
            bIsCleansed = true;
            eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 0);
            shrineSprite.sprite = CleansedSprite;
        }
    }
}
