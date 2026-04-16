using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogChase : DogBehavior
{
    private void OnDisable()
    {
        Debug.Log("chase timeout");
        dog.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes"))
        {
            Debug.Log("got node");
        }

        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Debug.Log("node not null");
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirections  in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirections.x, availableDirections.y, 0.0f);
                float distance = (dog.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirections;
                    minDistance = distance;
                }
            }

            dog.pm3.SetDirection(direction);
        }
    }
}
