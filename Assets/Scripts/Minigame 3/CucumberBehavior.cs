using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CucumberBehavior : MonoBehaviour
{
    private void Eat()
    {
        GameManager3.Instance.CucumberEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("cucumber trigger enter");
        if (other.gameObject.layer == LayerMask.NameToLayer("Kancil"))
        {
            Debug.Log("cucumber trigger enter w/ kancil");
            Eat();
            GameManager3.Instance.scoreSounds.PlayOneShot(GameManager3.Instance.cucumbSound, 0.6f);           
        }
    }
}
