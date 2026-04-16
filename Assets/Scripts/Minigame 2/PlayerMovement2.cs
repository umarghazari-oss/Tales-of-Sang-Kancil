using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float jump;
    private Rigidbody2D rb;
    private bool isGrounded;
    public MainMenuManager mainMenu;
    public GameManager2 gm2;
    public ScreenShake shake;

    AudioSource kancilSounds;
    public AudioClip hitSound;
    public AudioClip jumpSound;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm2 = FindFirstObjectByType<GameManager2>();
        anim = GetComponent<Animator>();
        kancilSounds = GetComponent<AudioSource>();
        shake = FindFirstObjectByType<ScreenShake>();
    }

    public void Jump()
    {    
        if (isGrounded)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));           
        }       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            kancilSounds.PlayOneShot(jumpSound,0.5f);
            anim.SetBool("isJumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gm2.Died();
            kancilSounds.PlayOneShot(hitSound, 0.7f);
            StartCoroutine(shake.Shaking());
        }
    }
}
