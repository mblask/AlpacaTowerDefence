public interface ITowerStatsHandler
{
    /// <summary>
    /// Updates the base <see cref="TowerTemplate"/> with the latest tower upgrades
    /// </summary>
    /// <param name="towerTemplate"></param>
    /// <returns><see cref="TowerStats"/></returns>
    TowerStats GetFinalStats(TowerTemplate towerTemplate);

    /// <summary>
    /// Retrieves the stat differences between the last and current tower stats
    /// </summary>
    /// <param name="towerTemplate"></param>
    /// <returns><see cref="TowerStats"/></returns>
    TowerStats GetTowerStatDifference(TowerTemplate towerTemplate);
}