using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Start: Get all self & child renderers and store their materials.
/// Methods: Change or reset stored materials.
/// </summary>
public class MaterialChanger : MonoBehaviour
{
    private List<Renderer> _renderers = new();
    private List<Material[]> _startMaterials = new();

    private void Start()
    {
        _renderers.AddRange(GetComponentsInChildren<Renderer>());
        foreach (Renderer renderer in _renderers)
        {
            _startMaterials.Add(renderer.sharedMaterials);
        }
    }

    public void ChangeMaterials(Material material)
    {
        foreach (Renderer renderer in _renderers)
        {
            renderer.sharedMaterial = material;
        }
    }

    public void ResetMaterials()
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].sharedMaterials = _startMaterials[i];
        }
    }
}