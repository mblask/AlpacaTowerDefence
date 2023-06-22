using System;

[Serializable]
public class TrapStats
{
    public int Cost;
    public float Health;
    public float Damage;
    public float Range;
    public float ResistDamage;
    public float EnemyCheckRate;
    public float TrapActivationTime;

    public TrapStats() { }

    public TrapStats(TrapStats trapStats)
    {
        Cost = trapStats.Cost;
        Health = trapStats.Health;
        Damage = trapStats.Damage;
        Range = trapStats.Range;
        ResistDamage = trapStats.ResistDamage;
        EnemyCheckRate = trapStats.EnemyCheckRate;
        TrapActivationTime = trapStats.TrapActivationTime;
    }

    public TrapStats(TrapTemplate trapTemplate)
    {
        Cost = trapTemplate.Cost;
        Health = trapTemplate.Health;
        Damage = trapTemplate.Damage;
        Range = trapTemplate.Range;
        ResistDamage = trapTemplate.ResistDamage;
        EnemyCheckRate = trapTemplate.EnemyCheckRate;
        TrapActivationTime = trapTemplate.TrapActivationTime;
    }
}