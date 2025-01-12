using System;
using UnityEngine;

[Serializable]
public class InstantiateParticleAction : IAmCollisionAction
{
    [SerializeField] private GameObject _particlePrefab;

    public void Execute(Collision other)
    {
        ContactPoint contact = other.GetContact(0);
        UnityEngine.Object.Instantiate(_particlePrefab, contact.point, Quaternion.LookRotation(contact.normal));
    }

    public void Execute() { }
}