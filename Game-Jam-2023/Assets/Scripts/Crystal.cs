using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private List<Sprite> progressSprites;
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
    [SerializeField]
    private Animator anim;
    [Range(0f, 1f)]
    [SerializeField]
    private float hold;

    [Header("Map-Related Tools")]
    [SerializeField]
    private GameObject RotFoliage, PureFoliage;
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
    public Image ebackground;
    private bool startFade = false;
    private bool stopFade = false;
    private bool bEaster = false;
    private bool anim1, anim2;

    private void Start()
    {
        #region E Key
        pInput = new PlayerInput();
        pInput.Enable();
        eSprite = interactPrompt.GetComponent<SpriteRenderer>();
        eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 0);
        pInput.PlayerMovement.Interact.performed += ShrineInteract;
        pInput.PlayerMovement.EasterInteract.performed += eShrineInteract;
        #endregion

        #region End of Game
        background.color = new Vector4(background.color.r, background.color.g,
                                           background.color.b, 0);
        ebackground.color = new Vector4(ebackground.color.r, ebackground.color.g,
                                           ebackground.color.b, 0);
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

        progressImage.sprite = progressSprites[completedShrines];
        
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
                anim1 = true;
                anim.SetBool("Crystalt", anim1);
                anim2 = true;
                anim.SetBool("CrystalAnimFinished", anim2);
                rate += Time.deltaTime * hold;
                if (bEaster)
                {
                    ebackground.color = new Vector4(ebackground.color.r, ebackground.color.g,
                                                   ebackground.color.b, fadeIn.Evaluate(rate));
                }
                else
                {
                    background.color = new Vector4(background.color.r, background.color.g,
                                                   background.color.b, fadeIn.Evaluate(rate));
                }
            }
            else
            {
                
                rate += Time.deltaTime;
                if (bEaster)
                {
                    ebackground.color = new Vector4(ebackground.color.r, ebackground.color.g,
                               ebackground.color.b, 1 - fadeIn.Evaluate(rate));
                }
                else
                {
                    background.color = new Vector4(background.color.r, background.color.g,
                                                   background.color.b, 1 - fadeIn.Evaluate(rate));
                }

                if (rate >= 1)
                {
                    stopFade = true;
                }
            }

            if (rate >= 1)
            {
                if (startFade)
                {
                    Destroy(RotFoliage);
                    Instantiate(PureFoliage, new Vector3(0, 0, 0), Quaternion.identity);
                    RottedMap.enabled = false;
                    PureMap.enabled = true;
                }
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
    private void eShrineInteract(InputAction.CallbackContext c)
    {
        if (canPurify)
        {
            canPurify = !canPurify;
            used = true;
            StartCoroutine(nameof(SpriteTransition));
            startFade = true;
            bEaster = true;
        }
    }

    IEnumerator SpriteTransition()
    {
        eSprite.color = new Vector4(eSprite.color.r, eSprite.color.g, eSprite.color.b, 0);
        yield return new WaitForSeconds(5);
    }
}
