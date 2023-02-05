using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    public bool used;
    private PlayerInput pInput;

    [Header("Sprites")]
    [SerializeField]
    private List<Shrine> shrines = new List<Shrine>();

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

    [Header("Map-Related Tools")]
    [SerializeField]
    private TilemapRenderer RottedMap;
    [SerializeField]
    private TilemapRenderer PureMap;

    [Header("Fade to White")]
    [SerializeField]
    private AnimationCurve fadeIn;
    [SerializeField]
    private AnimationCurve fadeOut;
    private float rate;
    [SerializeField]
    public Image background;
    private bool startFade = false;
    private bool stopFade = false;


    private void Start()
    {
        #region E Key
        pInput = new PlayerInput();
        pInput.Enable();
        eSprite = interactPrompt.GetComponent<SpriteRenderer>();
        eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 0);
        pInput.PlayerMovement.Interact.performed += ShrineInteract;
        #endregion

        #region End of Game
        background.color = new Vector4(background.color.r, background.color.g,
                                           background.color.b, 0);
        used = false;
        #endregion
    }

    private void OnDisable()
    {
        pInput.Disable();
        pInput.PlayerMovement.Interact.performed -= ShrineInteract;
    }

    void Update()
    {
        #region Checking Shrines
        int completedShrines = 0;
        foreach(Shrine s in shrines)
        {
            if (s.bIsCleansed == true)
                completedShrines++;
        }
        #endregion

        if (!used)
        {
            if (completedShrines == shrines.Count)
            {
                #region Raycast
                range.direction = target.position - transform.position;
                range.origin = transform.position;
                Debug.DrawRay(range.origin, range.direction.normalized * detectionDistance, Color.red);
                hit = Physics2D.Raycast(transform.position, target.position - transform.position, detectionDistance, layer);
                #endregion

                if (hit.collider != null && !used)
                {
                    eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 1 - hit.fraction);
                    canPurify = true;
                }
                else
                {
                    eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 0);
                    canPurify = false;
                }
            }
        } else if (used && !stopFade)
        {
            if (startFade)
            {
                rate += Time.deltaTime;
                background.color = new Vector4(background.color.r, background.color.g,
                                               background.color.b, fadeIn.Evaluate(rate));
            }
            else
            {
                rate += Time.deltaTime;
                background.color = new Vector4(background.color.r, background.color.g,
                                               background.color.b, 1 - fadeIn.Evaluate(rate));
                if (rate >= 1)
                {
                    stopFade = true;
                }
            }

            if (rate >= 1)
            {
                rate = 0;
                startFade = !startFade;
            }
        }


    }
    private void ShrineInteract(InputAction.CallbackContext c)
    {
        if (canPurify)
        {
            canPurify = !canPurify;
            used = true;
            StartCoroutine(nameof(SpriteTransition));
            startFade = true;
        }
    }

    IEnumerator SpriteTransition()
    {
        eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 0);
        yield return new WaitForSeconds(5);
    }
}
