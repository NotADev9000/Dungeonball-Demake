using System;
using System.Collections;
using UnityEngine;

public class MaterialFlasher : MonoBehaviour
{
    public void Play(VFXFlashData_SO data, MaterialChanger materialChanger, Action action = null)
    {
        StartCoroutine(BeginFlash(data, materialChanger, action));
    }

    private static IEnumerator BeginFlash(VFXFlashData_SO data, MaterialChanger materialChanger, Action action)
    {
        materialChanger.ChangeMaterials(data.FlashMaterial);
        yield return new WaitForSeconds(data.FlashDuration);
        materialChanger.ResetMaterials();
        action?.Invoke();
    }
}
