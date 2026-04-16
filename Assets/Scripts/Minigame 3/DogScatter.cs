using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScatter : DogBehavior
{
    private void OnDisable()
    {
        Debug.Log("scatter timeout");
        dog.chase.Enable();
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
            int index =  Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections.Count > 1 && node.availableDirections[index] ==  -dog.pm3.direction)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            dog.pm3.SetDirection(node.availableDirections[index]);
        }
    }
}
