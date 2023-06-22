using UnityEngine;

[CreateAssetMenu(fileName = "Trap", menuName = "Scriptable Objects / Trap")]
public class TrapTemplate : BuildingTemplate
{
    public float Damage;
    public float Range;
    public float ResistDamage;
    public float EnemyCheckRate;
    public float TrapActivationTime;
}