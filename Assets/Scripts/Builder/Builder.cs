using UnityEngine;

public class Builder : MonoBehaviour, IBuilder
{
    private static Builder _instance;
    public static Builder Instance
    {
        get
        {
            return _instance;
        }
    }

    private GameAssets _gameAssets;
    private BuildingsContainer _buildingsContainer;
    private IResourcesManager _resourcesManager;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _gameAssets = GameAssets.Instance;
        _buildingsContainer = BuildingsContainer.Instance;
        _resourcesManager = ResourcesManager.Instance;
    }

    public void Build(BuildingTemplate buildingTemplate, Vector3 position)
    {
        if (buildingTemplate.Cost > _resourcesManager.GetAvailableGold())
        {
            Debug.LogError("Not enough gold!");
            return;
        }

        switch (buildingTemplate)
        {
            case TowerTemplate towerTemplate:
                buildTower(towerTemplate, position);
                break;
            case TrapTemplate trapTemplate:
                buildTrap(trapTemplate, position);
                break;
            default:
                break;
        }
    }

    private void buildTower(TowerTemplate towerTemplate, Vector3 position)
    {
        Transform towerTransform = Instantiate(_gameAssets.Tower, position, Quaternion.identity, _buildingsContainer.transform);
        _buildingsContainer.AddElement(towerTransform);

        ITower tower = towerTransform.GetComponent<ITower>();
        tower.SetupTower(towerTemplate);
        _resourcesManager.UpdateGold(-towerTemplate.Cost);
    }

    private void buildTrap(TrapTemplate trapTemplate, Vector3 position)
    {
        Transform trapTransform = Instantiate(_gameAssets.Trap, position, Quaternion.identity,
            _buildingsContainer.transform);
        _buildingsContainer.AddElement(trapTransform);

        ITrap trap = trapTransform.GetComponent<ITrap>();
        trap.SetupTrap(trapTemplate);
        _resourcesManager.UpdateGold(-trapTemplate.Cost);
    }
}
