using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(PlayerMovement3))]
public class Dog : MonoBehaviour
{
    public PlayerMovement3 pm3 { get; private set; }
    public DogScatter scatter { get; private set; }
    public DogChase chase { get; private set; }
    public DogBehavior initialBehavior;
    AudioSource dogSounds;
    public AudioClip killSound;
    public Transform target;
    public SpriteRenderer spriteRenderer;
    private Vector3 currentPos, lastPos;

    private void Awake()
    {
        pm3 = GetComponent<PlayerMovement3>();
        dogSounds = GetComponent<AudioSource>();
        scatter = GetComponent<DogScatter>();
        chase = GetComponent<DogChase>();
        initialBehavior = GetComponent<DogBehavior>();
    }

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        lastPos = currentPos;
        currentPos = transform.position;
        if (currentPos.x > lastPos.x)
        {
            Debug.Log("dog going right");
            FlipSprite(true);
        }
        else if (currentPos.x < lastPos.x)
        {
            Debug.Log("dog going left");
            FlipSprite(false);
        }
        
        SpriteDirection();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        pm3.ResetState();

        chase.Disable();
        scatter.Enable();

        if(initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {     
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Kancil"))
        {
            Debug.Log("collided w kancil");
            GameManager3.Instance.Died();
            dogSounds.PlayOneShot(killSound, 0.3f);
        }
    }

    public void FlipSprite(bool flip)
    {
        spriteRenderer.flipX = flip;
    }

    public void SpriteDirection()
    {
        float rightAngle = Mathf.Atan2(pm3.direction.y, pm3.direction.x);
        float leftAngle = Mathf.Atan2(pm3.direction.y, -pm3.direction.x);
        Quaternion rightSide = Quaternion.AngleAxis(rightAngle * Mathf.Rad2Deg, Vector3.forward);
        Quaternion leftSide = Quaternion.AngleAxis(-leftAngle * Mathf.Rad2Deg, Vector3.forward);
        if (spriteRenderer.flipX)
        {
            transform.rotation = rightSide;
        }
        else
        {
            transform.rotation = leftSide;
        }
    }
}
