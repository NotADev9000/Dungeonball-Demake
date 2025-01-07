using System;
using System.Collections;
using UnityEngine;

public class MaterialFlasher : MonoBehaviour
{
    public void Play(MaterialFlashData_SO data, MaterialChanger materialChanger, Action action = null)
    {
        StartCoroutine(BeginFlash(data, materialChanger, action));
    }

    private static IEnumerator BeginFlash(MaterialFlashData_SO data, MaterialChanger materialChanger, Action action)
    {
        materialChanger.ChangeMaterials(data.FlashMaterial);
        yield return new WaitForSeconds(data.FlashDuration);
        materialChanger.ResetMaterials();
        action?.Invoke();
    }
}
