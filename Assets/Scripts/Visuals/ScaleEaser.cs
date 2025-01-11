using System.Collections;
using UnityEngine;

public class ScaleEaser : MonoBehaviour
{
    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    public void EaseScaleToTarget(Vector3 targetScale, float speed)
    {
        StartCoroutine(EaseScaleCoroutine(targetScale, speed));
    }

    public void EaseScaleToInitial(float speed)
    {
        StartCoroutine(EaseScaleCoroutine(_initialScale, speed));
    }

    // speed = units per second
    private IEnumerator EaseScaleCoroutine(Vector3 targetScale, float speed)
    {
        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.MoveTowards(
                transform.localScale,
                targetScale,
                speed * Time.deltaTime
            );
            yield return null;
        }
    }
}