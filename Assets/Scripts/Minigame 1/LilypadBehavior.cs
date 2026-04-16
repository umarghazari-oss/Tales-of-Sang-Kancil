using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadBehavior : MonoBehaviour
{
    public GameObject lilyPad;
    private GameObject sangKancil;
    private PlayerMovement1 pm1;
    private GameManager1 gm1;

    [SerializeField] private float timeToDestroy = 5f; 
    [SerializeField] private float currentDestroyTime = 0f;
    public bool destroyTimerActive = false;

    private Animator anim;
    public AudioSource lilypadSounds;
    public AudioClip rustleSound;

    private void Awake()
    {
        pm1 = FindFirstObjectByType<PlayerMovement1>();
        gm1 = FindFirstObjectByType<GameManager1>();
        anim = GetComponent<Animator>();
        sangKancil = GameObject.Find("Sang Kancil");
        lilypadSounds = GetComponent<AudioSource>();
        lilypadSounds.clip = rustleSound;
    }
    private void Update()
    {
        if (destroyTimerActive)
        {
            currentDestroyTime += Time.deltaTime;

            if (currentDestroyTime >= timeToDestroy)
            {
                sangKancil.transform.SetParent(null);
                // Timer has reached the desired time
                Debug.Log("Destroy times up");
                pm1.Death();
                Invoke(nameof(pm1.Respawn), 0.3f);
                DestroyLilypad(); // Call your function or trigger your event
                StopDestroyLPTimer(); // Stop the timer after it's done
                gm1.StartRespawnLPTimer();            
            }
        }      
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDestroyLPTimer();
            anim.SetBool("isShaking", true);
            lilypadSounds.loop = true;
            lilypadSounds.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopDestroyLPTimer();
            anim.SetBool("isShaking", false);
            lilypadSounds.Stop();
        }

        if (other.gameObject.CompareTag("Lilypad"))
        {
            pm1.Death();
        }
    }

    void StartDestroyLPTimer()
    {
        currentDestroyTime = 0f; // Reset the timer
        destroyTimerActive = true;
        Debug.Log("Destroy timer started!");
    }

    void StopDestroyLPTimer()
    {
        destroyTimerActive = false;
        Debug.Log("Destroy timer stopped!");
    }

    void DestroyLilypad()
    {       
        if (lilyPad != null)
        {  
            lilyPad.SetActive(false);
            gm1.lilyPadDestroyed = true;
           //gm1.destroyedLilyPadCount +=1;
            gm1.lpb = GetComponent<LilypadBehavior>();
            Debug.Log("Lilypad Destroyed");           
        }   
    }
}
