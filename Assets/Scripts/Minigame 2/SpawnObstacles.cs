using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private GameManager2 gm;

    private float timer = 6f;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager2>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {        
        rb.linearVelocity = Vector2.left * (speed + gm.speedMultiplier);
    }
}
