using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public void DestroyInSeconds(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
