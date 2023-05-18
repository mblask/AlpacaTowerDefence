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
    /// Shooting precision of <see cref="Tower"/> missiles
    /// </summary>
    public float Precision;
    /// <summary>
    /// Number of shots per second
    /// </summary>
    public float ShootingRate;
    /// <summary>
    /// <see cref="Tower"/> range when checking for <see cref="Enemy"/> objects
    /// </summary>
    public float Range;
    /// <summary>
    /// Portion of damage resisted, from 0 to 1
    /// </summary>
    public float ResistDamage;
}
