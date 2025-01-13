using UnityEngine;

[RequireComponent(typeof(ScaleEaser))]
public class ScaleOnJump : MonoBehaviour
{
    [SerializeField] private MovementJump _movementJump;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _scaleSpeed;

    private ScaleEaser _scaleEaser;

    private void Awake()
    {
        _scaleEaser = GetComponent<ScaleEaser>();
    }
    private void OnEnable()
    {
        _movementJump.OnJump += PlaySound;
    }

    private void OnDisable()
    {
        _movementJump.OnJump -= PlaySound;
    }

    private void PlaySound()
    {
        _scaleEaser.gameObject.transform.localScale = _targetScale;
        _scaleEaser.EaseScaleToInitial(_scaleSpeed);
    }
}