public class TowerStatsHandler : ITowerStatsHandler
{
    private IBuildingUpgradeManager _buildingUpgradeManager;

    public TowerStatsHandler()
    {
        _buildingUpgradeManager = BuildingUpgradeManager.Instance;
    }

    public TowerStats GetFinalStats(TowerTemplate towerTemplate)
    {
        TowerUpgrades towerUpgrades = _buildingUpgradeManager.GetTowerUpgrades();
        return new TowerStats(towerTemplate, towerUpgrades);
    }

    public TowerStats GetTowerStatDifference(TowerTemplate towerTemplate)
    {
        TowerStats lastStats = new TowerStats(towerTemplate, _buildingUpgradeManager.GetLastTowerUpgrades());
        TowerStats newStats = new TowerStats(towerTemplate, _buildingUpgradeManager.GetTowerUpgrades());

        return newStats - lastStats;
    }
}
