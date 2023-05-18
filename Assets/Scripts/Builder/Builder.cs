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
    private IResourcesManager _resourcesManager;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _gameAssets = GameAssets.Instance;
        _resourcesManager = ResourcesManager.Instance;
    }

    public void Build(BuildingTemplate buildingTemplate, Vector3 position)
    {
        if (buildingTemplate.Cost > _resourcesManager.GetAvailableGold())
        {
            Debug.LogError("Not enough gold!");
            return;
        }

        if (buildingTemplate is TowerTemplate)
        {
            TowerTemplate towerTemplate = (TowerTemplate)buildingTemplate;
            buildTower(towerTemplate, position);
        }
    }

    private void buildTower(TowerTemplate towerTemplate, Vector3 position)
    {
        ITower tower = Instantiate(_gameAssets.Tower, position, Quaternion.identity, null).GetComponent<ITower>();
        tower.SetupTower(towerTemplate);
        _resourcesManager.UpdateGold(-towerTemplate.Cost);
    }
}
