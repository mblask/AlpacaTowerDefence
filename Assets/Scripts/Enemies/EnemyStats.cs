using System;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public EnemyStats(EnemyTemplate template)
    {
        Health = template.Health;
        MovingSpeed = template.MovingSpeed;
        Range = template.Range;
        BuildingSearchTime = template.BuildingSearchInterval;
        BuildingAttackTime = template.BuildingAttackInterval;
        GoldDrop = template.GoldDrop;
        WoodDrop = template.WoodDrop;
        MetalDrop = template.MetalDrop;
    }

    public float Health { get; set; }
    public float MovingSpeed { get; set; }
    public float Range { get; set; }
    public float BuildingSearchTime { get; set; }
    public float BuildingAttackTime { get; set; }
    public Vector2Int GoldDrop { get; set; }
    public Vector2Int WoodDrop { get; set; }
    public Vector2Int MetalDrop { get; set; }
}