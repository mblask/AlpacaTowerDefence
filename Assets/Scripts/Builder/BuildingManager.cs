using UnityEngine;

public class BuildingManager : MonoBehaviour, IBuildingManager
{
    private static BuildingManager _instance;
    public static BuildingManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private GameAssets _gameAssets;
    private IBuildingView _currentBuildingView;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _gameAssets = GameAssets.Instance;
        _currentBuildingView = BuildingView.Instance;
    }

    public void Build()
    {
        _currentBuildingView?.Build();
    }

    public void ViewWoodenOutpost()
    {
        _currentBuildingView.SetupView(_gameAssets.GetTemplateByEnum(BuildingEnum.WoodenOutpost));
    }

    public void ViewWoodenTower()
    {
        _currentBuildingView.SetupView(_gameAssets.GetTemplateByEnum(BuildingEnum.WoodenTower));
    }

    public void ViewStoneTower()
    {
        _currentBuildingView.SetupView(_gameAssets.GetTemplateByEnum(BuildingEnum.StoneTower));
    }

    public void ViewSpikeTrap()
    {
        _currentBuildingView.SetupView(_gameAssets.GetTemplateByEnum(BuildingEnum.SpikeTrap));
    }
}
