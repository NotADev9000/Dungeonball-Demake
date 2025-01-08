using System.Collections;
using UnityEngine;

public class ObjectBlinker : MonoBehaviour
{
    public void Play(GameObject objectToBlink, float duration = 0f)
    {
        StartCoroutine(StartBlinking(objectToBlink));
        if (duration > 0) StartCoroutine(StopBlinking(duration));
    }

    private static IEnumerator StartBlinking(GameObject objectToBlink)
    {
        while (true)
        {
            objectToBlink.SetActive(false);
            yield return null;
            objectToBlink.SetActive(true);
            yield return null;
        }
    }

    private IEnumerator StopBlinking(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopAllCoroutines();
    }

    public void StopBlinking()
    {
        StopAllCoroutines();
    }
}
