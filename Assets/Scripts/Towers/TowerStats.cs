using UnityEngine;
using System;

[Serializable]
public class TowerStats
{
    public TowerStats() { }

    public TowerStats(TowerStats towerStats)
    {
        Cost = towerStats.Cost;
        Health = towerStats.Health;
        BaseRepair = towerStats.BaseRepair;
        CrewNumber = towerStats.CrewNumber;
        Damage = towerStats.Damage;
        EnemyCheckRate = towerStats.EnemyCheckRate;
        Precision = towerStats.Precision;
        ShootingRate = towerStats.ShootingRate;
        ShootingRatePercentageWhileRepairing = towerStats.ShootingRatePercentageWhileRepairing;
        Range = towerStats.Range;
        ResistDamage = towerStats.ResistDamage;
        ChanceToKillCrewMember = towerStats.ChanceToKillCrewMember;
    }

    public TowerStats(TowerTemplate template, TowerUpgrades upgrades)
    {
        Cost = template.Cost;
        Health = template.Health + upgrades.HealthModifier.Value;
        BaseRepair = template.BaseRepair + upgrades.BaseRepairModifier.Value;
        CrewNumber = template.CrewNumber + upgrades.CrewModifier.Value;
        Damage = template.Damage;
        EnemyCheckRate = template.EnemyCheckRate;
        Precision = template.Precision;
        ShootingRate = template.ShootingRate + upgrades.ShootingRateModifier.Value;
        ShootingRatePercentageWhileRepairing = template.ShootingRatePercentageWhileRepairing;
        Range = template.Range;
        ResistDamage = template.ResistDamage + upgrades.DamageResistModifier.Value;
        ChanceToKillCrewMember = template.ChanceToKillCrewMember;
    }

    public static TowerStats operator +(TowerStats a, TowerStats b)
    {
        return new TowerStats
        {
            Cost = a.Cost + b.Cost,
            Health = a.Health + b.Health,
            BaseRepair = a.BaseRepair + b.BaseRepair,
            CrewNumber = a.CrewNumber + b.CrewNumber,
            Damage = a.Damage + b.Damage,
            EnemyCheckRate = a.EnemyCheckRate + b.EnemyCheckRate,
            Precision = a.Precision + b.Precision,
            ShootingRate = a.ShootingRate + b.ShootingRate,
            ShootingRatePercentageWhileRepairing = 
            a.ShootingRatePercentageWhileRepairing + b.ShootingRatePercentageWhileRepairing,
            Range = a.Range + b.Range,
            ResistDamage = a.ResistDamage + b.ResistDamage,
            ChanceToKillCrewMember = a.ChanceToKillCrewMember + b.ChanceToKillCrewMember
        };
    }

    public static TowerStats operator -(TowerStats a, TowerStats b)
    {
        return new TowerStats
        {
            Cost = a.Cost - b.Cost,
            Health = a.Health - b.Health,
            BaseRepair = a.BaseRepair - b.BaseRepair,
            CrewNumber = a.CrewNumber - b.CrewNumber,
            Damage = a.Damage - b.Damage,
            EnemyCheckRate = a.EnemyCheckRate - b.EnemyCheckRate,
            Precision = a.Precision - b.Precision,
            ShootingRate = a.ShootingRate - b.ShootingRate,
            ShootingRatePercentageWhileRepairing = 
            a.ShootingRatePercentageWhileRepairing - b.ShootingRatePercentageWhileRepairing,
            Range = a.Range - b.Range,
            ResistDamage = a.ResistDamage - b.ResistDamage,
            ChanceToKillCrewMember = a.ChanceToKillCrewMember - b.ChanceToKillCrewMember
        };
    }

    /// <summary>
    /// <see cref="Tower"/> cost
    /// </summary>
    public int Cost;

    /// <summary>
    /// <see cref="Tower"/> health
    /// </summary>
    public float Health;

    /// <summary>
    /// Base repair of a <see cref="Tower"/>
    /// </summary>
    public float BaseRepair;

    /// <summary>
    /// Crew in the <see cref="Tower"/>
    /// </summary>
    public int CrewNumber;

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