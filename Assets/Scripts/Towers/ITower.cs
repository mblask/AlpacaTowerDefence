/// <summary>
/// Handles eveything related with <see cref="Tower"/> class
/// </summary>
public interface ITower
{
    /// <summary>
    /// Sets up a new instance of a <see cref="Tower"/> using <see cref="TowerTemplate"/>
    /// </summary>
    /// <param name="towerTemplate"></param>
    void SetupTower(TowerTemplate towerTemplate);
}
