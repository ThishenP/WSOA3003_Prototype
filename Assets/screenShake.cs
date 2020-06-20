using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenShake : MonoBehaviour
{
    private float timeShaken;
    private float x;
    private float y;
    public IEnumerator Shake(float duration, float amountOfShake)
    {
        Vector3 startPos = transform.localPosition;
        timeShaken = 0;
        while (timeShaken < duration)
        {
            x = Random.Range(-1f, 1f) * amountOfShake;
            y = Random.Range(-1f, 1f) * amountOfShake;
            transform.localPosition = new Vector3(x, y, startPos.z);
            timeShaken = timeShaken + Time.deltaTime;
            yield return null;
        }
        transform.localPosition = startPos;
    }
}