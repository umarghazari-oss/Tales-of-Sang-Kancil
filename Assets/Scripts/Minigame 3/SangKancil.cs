using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SangKancil : MonoBehaviour
{
    [SerializeField]
    private float threshold = 50f;
    public PlayerMovement3 pm3;
    public SpriteRenderer spriteRenderer;
    private Vector3 startPos, endPos;

    // Start is called before the first frame update
    private void Awake()
    {
        pm3 = GetComponent<PlayerMovement3>();
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

        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            DetectSwipe();
        }

        SpriteDirection();        
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
                    pm3.SetDirection(Vector2.right);
                    FlipSprite(true);
                    Debug.Log("Swipe Right");
                }

                else
                {
                    pm3.SetDirection(Vector2.left);
                    FlipSprite(false);
                    Debug.Log("Swipe Left");
                }
            }

            else
            {
                if (swipeDelta.y > 0)
                {
                    pm3.SetDirection(Vector2.up);
                    Debug.Log("Swipe Up");
                }

                else
                {
                    pm3.SetDirection(Vector2.down);
                    Debug.Log("Swipe Down");
                }
            }
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
        Quaternion rightSide =  Quaternion.AngleAxis(rightAngle * Mathf.Rad2Deg, Vector3.forward);
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

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        pm3.ResetState();
        gameObject.SetActive(true);
    }
}
