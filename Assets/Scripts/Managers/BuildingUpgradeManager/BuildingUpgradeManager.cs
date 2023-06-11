using UnityEngine;

public class BuildingUpgradeManager : MonoBehaviour, IBuildingUpgradeManager
{
    private static BuildingUpgradeManager _instance;
    public static BuildingUpgradeManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private int _towerArmorLevel;
    private int _towerConstructionLevel;
    private int _towerCrewLevel;

    private TowerUpgrades _lastTowerUpgrades = new TowerUpgrades();
    [SerializeField] private TowerUpgrades _towerUpgrades = new TowerUpgrades();

    private void Awake()
    {
        _instance = this;
    }

    public TowerUpgrades GetTowerUpgrades()
    {
        return _towerUpgrades;
    }

    public TowerUpgrades GetLastTowerUpgrades()
    {
        return _lastTowerUpgrades;
    }

    public void TowerArmorUpgrade()
    {
        int maxLevel = 10;

        if (_towerArmorLevel < maxLevel)
        {
            _towerArmorLevel++;

            _lastTowerUpgrades = new TowerUpgrades(_towerUpgrades);
    
            _towerUpgrades.DamageResistModifier.Level++;
    
            float baseDamageResistModifier = 7.0f;
            _towerUpgrades.DamageResistModifier.Value += 
                Mathf.Round(baseDamageResistModifier / _towerUpgrades.DamageResistModifier.Level);

            UpdateTowers();
        }
    }

    private void UpdateTowers()
    {
        Transform buildingContainer = BuildingsContainer.GetInstance().GetContainer();
        foreach (Transform transform in buildingContainer)
        {
            Tower tower = transform.GetComponent<Tower>();
            if (tower != null)
            {
                tower.UpdateStats();
            }
        }
    }

    public void TowerConstructionUpgrade()
    {
        int maxLevel = 10;

        if (_towerConstructionLevel < maxLevel)
        {
            _towerConstructionLevel++;

            _lastTowerUpgrades = new TowerUpgrades(_towerUpgrades);

            _towerUpgrades.HealthModifier.Level++;

            float baseHealthModifier = 10.0f;
            _towerUpgrades.HealthModifier.Value += Mathf.Round(baseHealthModifier / _towerConstructionLevel);

            UpdateTowers();
        }
    }

    public void TowerCrewUpgrade()
    {
        int maxLevel = 5;

        if (_towerCrewLevel < maxLevel)
        {
            _towerCrewLevel++;

            _lastTowerUpgrades = new TowerUpgrades(_towerUpgrades);

            _towerUpgrades.CrewModifier.Level++;
            _towerUpgrades.CrewModifier.Value++;

            _towerUpgrades.ShootingRateModifier.Level++;
            float baseShootingRateModifier = 10.0f;
            _towerUpgrades.ShootingRateModifier.Value -= 
                Mathf.Round(baseShootingRateModifier / _towerUpgrades.ShootingRateModifier.Level) / 100.0f;

            _towerUpgrades.BaseRepairModifier.Level++;
            float baseRepairModifier = 8.0f;
            _towerUpgrades.BaseRepairModifier.Value += 
                Mathf.Round(baseRepairModifier / _towerUpgrades.BaseRepairModifier.Level) / 100.0f;

            UpdateTowers();
        }
    }
}
