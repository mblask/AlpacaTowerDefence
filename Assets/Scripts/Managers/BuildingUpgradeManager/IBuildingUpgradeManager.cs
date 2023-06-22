public interface IBuildingUpgradeManager
{
    /// <summary>
    /// Get current <see cref="TowerUpgrades"/>
    /// </summary>
    /// <returns></returns>
    TowerUpgrades GetTowerUpgrades();

    /// <summary>
    /// Retrieves the last <see cref="TowerUpgrades"/> valid before the latest upgrade
    /// </summary>
    /// <returns></returns>
    TowerUpgrades GetLastTowerUpgrades();

    /// <summary>
    /// Upgrades <see cref="Tower"/> damage resist
    /// </summary>
    void TowerArmorUpgrade();

    /// <summary>
    /// Upgrades <see cref="Tower"/> health
    /// </summary>
    void TowerConstructionUpgrade();

    /// <summary>
    /// Upgrades <see cref="Tower"/> crew number, shooting rate and base repair
    /// </summary>
    void TowerCrewUpgrade();
}