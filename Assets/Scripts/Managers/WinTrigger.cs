using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player_Combat>() != null)
        {
            GameManager.Instance.WinGame();
        }
    }
}