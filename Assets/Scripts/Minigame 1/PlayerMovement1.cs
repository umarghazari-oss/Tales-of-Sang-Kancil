using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField]
    private float threshold = 5f;

    private Vector3 startPos, endPos;
    private bool cooldown;

    public GameManager1 gm1;
    public LilypadBehavior lpb;
    private CutsceneManager1 cutsceneManager1;

    AudioSource kancilSounds;
    public AudioClip deathSound;
    public AudioClip jumpSound;

    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite leapSprite;
    public Sprite deadSprite;

    private Vector3 spawnPos;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        kancilSounds = GetComponent<AudioSource>();
        gm1 = FindFirstObjectByType<GameManager1>();
        lpb = GetComponent<LilypadBehavior>();
        cutsceneManager1 = FindFirstObjectByType<CutsceneManager1>();
        spawnPos = transform.position;      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }

            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                endPos = touch.position;
                DetectSwipe();
            }
        }

        else if (!Input.GetMouseButtonDown(0)) 
        {
            startPos = Input.mousePosition;
        }

        else if(Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            DetectSwipe();
        }
 
    }

    void DetectSwipe()
    {
        Vector3 swipeDelta = endPos - startPos;

        if (swipeDelta.sqrMagnitude > threshold)
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f,-90f);
                    Move(Vector3.right);
                    kancilSounds.PlayOneShot(jumpSound, 0.3f);
                    Debug.Log("Swipe Right");
                }

                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    Move(Vector3.left);
                    kancilSounds.PlayOneShot(jumpSound, 0.3f);
                    Debug.Log("Swipe Left");
                }
            }

            else
            {
                if (swipeDelta.y > 0)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    Move(Vector3.up);
                    kancilSounds.PlayOneShot(jumpSound, 0.3f);
                    Debug.Log("Swipe Up");
                }

                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    Move(Vector3.down);
                    kancilSounds.PlayOneShot(jumpSound, 0.3f);
                    Debug.Log("Swipe Down");
                }
            }
        }
    }

   private void Move(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;

        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacles = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        Collider2D deathZone = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Death Zone"));

        if (cooldown)
        {
            return;
        }

        if (barrier != null)
        {
            return;
        }

        if (platform != null)
        {
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if (obstacles != null && platform == null)
        {
            transform.position = destination;
            Death();
        }
        else
        {
            StartCoroutine(Leap(destination));
        }

        if (deathZone != null)
        {
            transform.position = destination;
            transform.SetParent(null);
            Death();
            Debug.Log("Death Zone detected");
        }

    }

    public void Death()
    {
        StopAllCoroutines();

        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deadSprite;
        StartCoroutine(CoPlayDelayedClip(deathSound, 0.15f));

        enabled = false;
        gm1.Died();

        Invoke(nameof(Respawn), 1f);     
    }

    public void Respawn()
    {
        transform.SetPositionAndRotation(spawnPos, Quaternion.identity);
        enabled = true; 
        spriteRenderer.sprite = idleSprite;       
    }

    private IEnumerator Leap(Vector3 destination)
    {
        cooldown = true;
        Vector3 startPosition = transform.position;

        spriteRenderer.sprite = leapSprite;

        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = destination;
        spriteRenderer.sprite = idleSprite;
        cooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {       
        if (enabled && other.gameObject.layer == LayerMask.NameToLayer("Death Zone"))
        {
            transform.SetParent(null);
            Death();
            Debug.Log("Death Zone detected");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Win Zone"))
        {
            Debug.Log("Win Zone detected");
            cutsceneManager1.endingCutscene = true;
            AudioManager.Instance.ChangeToEndMusic();
        }
    }

    private IEnumerator CoPlayDelayedClip(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        kancilSounds.PlayOneShot(deathSound, 1f);
    }
}
