using UnityEngine;

/// <summary>
/// Template that contains basic building information for <see cref="Tower"/> objects
/// </summary>
[CreateAssetMenu(fileName = "New Tower Settings", menuName = "Scriptable Objects / Tower")]
public class TowerTemplate : BuildingTemplate
{
    /// <summary>
    /// <see cref="Tower"/> damage
    /// </summary>
    public Vector2 Damage;

    /// <summary>
    /// Tower checks for enemies in range every <see cref="EnemyCheckRate"/> seconds
    /// </summary>
    public float EnemyCheckRate;

    /// <summary>
    /// Shooting precision of <see cref="Tower"/> missiles
    /// </summary>
    public float Precision;

    /// <summary>
    /// Number of shots per second
    /// </summary>
    public float ShootingRate;

    /// <summary>
    /// Percentage of shooting rate while repairing tower. From 0 to 1
    /// </summary>
    public float ShootingRatePercentageWhileRepairing;

    /// <summary>
    /// <see cref="Tower"/> range when checking for <see cref="Enemy"/> objects
    /// </summary>
    public float Range;

    /// <summary>
    /// Portion of damage resisted, from 0 to 1
    /// </summary>
    public float ResistDamage;

    /// <summary>
    /// Chance from 0 to 100
    /// </summary>
    public float ChanceToKillCrewMember;
}
