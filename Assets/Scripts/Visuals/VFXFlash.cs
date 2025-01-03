using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXFlash : MonoBehaviour
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

    public void Play(VFXFlashData_SO data)
    {
        StartCoroutine(BeginFlash(data));
    }

    private IEnumerator BeginFlash(VFXFlashData_SO data)
    {
        ChangeMaterials(data.FlashMaterial);
        yield return new WaitForSeconds(data.FlashDuration);
        ResetMaterials();
        // invoke callback here...
    }

    private void ChangeMaterials(Material material)
    {
        foreach (Renderer renderer in _renderers)
        {
            renderer.sharedMaterial = material;
        }
    }

    private void ResetMaterials()
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].sharedMaterials = _startMaterials[i];
        }
    }
}