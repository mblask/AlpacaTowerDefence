using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private static KeyboardInput _instance;
    public static KeyboardInput Instance
    {
        get
        {
            return _instance;
        }
    }

    [Header("Building keys")]
    [SerializeField] private KeyCode _woodenOutpostKey;
    [SerializeField] private KeyCode _woodenTowerKey;
    [SerializeField] private KeyCode _stoneTowerKey;
    [SerializeField] private KeyCode _towerComplexKey;
    [SerializeField] private KeyCode _fortKey;
    [SerializeField] private KeyCode _castleKey;

    [Header("All same-sort-building-action key")]
    [SerializeField] private KeyCode _allEqualActionKey;

    [Header("Building action keys")]
    [SerializeField] private KeyCode _buildingRepairKey;

    [Header("Building upgrades")]
    [SerializeField] private KeyCode _towerArmorUpgradeKey;
    [SerializeField] private KeyCode _towerConstructionUpgradeKey;
    [SerializeField] private KeyCode _towerCrewUpgradeKey;

    private IBuildingManager _buildingManager;
    private IBuildingUpgradeManager _buildingUpgradeManager;
    private BuildingsContainer _buildingsContainer;
    private IInteractableManager _interactableManager;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _buildingManager = BuildingManager.Instance;
        _buildingUpgradeManager = BuildingUpgradeManager.Instance;
        _buildingsContainer = BuildingsContainer.GetInstance();
        _interactableManager = InteractableManager.Instance;
    }

    private void Update()
    {
        viewWoodenOutpost();
        viewWoodenTower();
        viewStoneTower();
        viewTowerComplex();
        viewFort();
        viewCastle();
        repairBuilding();
        repairAllBuildings();
        towerArmorUpgrade();
        towerConstructionUpgrade();
        towerCrewUpgrade();
    }

    private void viewWoodenOutpost()
    {
        if (Input.GetKeyUp(_woodenOutpostKey))
            _buildingManager.ViewWoodenOutpost();
    }

    private void viewWoodenTower()
    {
        if (Input.GetKeyUp(_woodenTowerKey))
            _buildingManager.ViewWoodenTower();
    }

    private void viewStoneTower()
    {
        if (Input.GetKeyUp(_stoneTowerKey))
            _buildingManager.ViewStoneTower();
    }
    
    private void viewTowerComplex()
    {
        if (Input.GetKeyUp(_towerComplexKey))
            Debug.Log("View tower complex");
    }

    private void viewFort()
    {
        if (Input.GetKeyUp(_fortKey))
            Debug.Log("View fort");
    }

    private void viewCastle()
    {
        if (Input.GetKeyUp(_castleKey))
            Debug.Log("View castle");
    }

    private void repairBuilding()
    {
        if (Input.GetKeyUp(_buildingRepairKey))
        {
            Building building = _interactableManager.GetCurrentInteractableObject()?.GetComponent<Building>();
            building?.Repair();
        }
    }

    private void repairAllBuildings()
    {
        if (Input.GetKey(_allEqualActionKey) && Input.GetKeyUp(_buildingRepairKey))
        {
            Debug.Log("Repair all buildings");
            foreach (Transform transform in _buildingsContainer.GetContainer())
                transform.GetComponent<Building>()?.Repair();
        }
    }

    private void towerArmorUpgrade()
    {
        if (Input.GetKeyUp(_towerArmorUpgradeKey))
            _buildingUpgradeManager.TowerArmorUpgrade();
    }

    private void towerConstructionUpgrade()
    {
        if (Input.GetKeyUp(_towerConstructionUpgradeKey))
            _buildingUpgradeManager.TowerConstructionUpgrade();
    }

    private void towerCrewUpgrade()
    {
        if (Input.GetKeyUp(_towerCrewUpgradeKey))
            _buildingUpgradeManager.TowerCrewUpgrade();
    }
}
