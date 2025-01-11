using UnityEngine;

[CreateAssetMenu(fileName = "VFXFlashData", menuName = "ScriptableObjects/VFXFlashData")]
public class MaterialFlashData_SO : ScriptableObject
{
    [SerializeField] Material _flashMaterial = null;
    public Material FlashMaterial => _flashMaterial;
    [SerializeField] float _flashDuration = 0.1f;
    public float FlashDuration => _flashDuration;
}
