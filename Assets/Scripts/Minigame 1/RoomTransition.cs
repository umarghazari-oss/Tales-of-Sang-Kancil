using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public GameObject roomCam;
    public GameObject camBarrier;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomCam.SetActive(true);
            camBarrier.SetActive(true);         
        }      
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomCam.SetActive(false);
            camBarrier.SetActive(false);
        }
    }
}
