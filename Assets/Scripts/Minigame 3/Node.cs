using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public readonly List<Vector2> availableDirections = new();

    private void Start()
    {
        //availableDirections.Clear();

        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        Debug.Log("Checking");
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1.5f, obstacleLayer);
        if(hit.collider == null)
        {
            Debug.Log("Add Direction");
            availableDirections.Add (direction);
        }
    }
}
