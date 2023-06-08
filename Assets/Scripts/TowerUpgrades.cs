using System;

[Serializable]
public class TowerUpgrades
{
    public Modifier<float> DamageResistModifier = new Modifier<float>();

    public Modifier<float> HealthModifier = new Modifier<float>();

    public Modifier<int> CrewModifier = new Modifier<int>();

    public Modifier<float> ShootingRateModifier = new Modifier<float>();

    public Modifier<float> BaseRepairModifier = new Modifier<float>();

    public TowerUpgrades() { }

    public TowerUpgrades(TowerUpgrades towerUpgrades)
    {
        DamageResistModifier = new Modifier<float>(towerUpgrades.DamageResistModifier);
        HealthModifier = new Modifier<float>(towerUpgrades.HealthModifier);
        CrewModifier = new Modifier<int>(towerUpgrades.CrewModifier);
        ShootingRateModifier = new Modifier<float>(towerUpgrades.ShootingRateModifier);
        BaseRepairModifier = new Modifier<float>(towerUpgrades.BaseRepairModifier);
    }
}