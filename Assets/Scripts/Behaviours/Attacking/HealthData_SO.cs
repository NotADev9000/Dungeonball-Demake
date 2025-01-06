using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/HealthData", order = 1)]
public class HealthData_SO : ScriptableObject
{
    [field: SerializeField] public int MaxHealth { get; set; } = 10;
}