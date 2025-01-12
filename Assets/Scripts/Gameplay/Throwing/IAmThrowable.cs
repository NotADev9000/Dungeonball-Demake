using UnityEngine;

public interface IAmThrowable
{
    void Throw(Vector3 direction, Collider throwerCollider = null);
}
