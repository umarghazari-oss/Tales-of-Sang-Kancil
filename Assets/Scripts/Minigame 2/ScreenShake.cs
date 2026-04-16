using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 1f;
    public GameManager2 gm2;

    void Start()
    {
        gm2 = FindFirstObjectByType<GameManager2>();
    }

    public IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            if (gm2.livesCount <= 0)
            {
                yield break;
            }
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
               
        }        

        transform.position = startPosition;
    }
}
