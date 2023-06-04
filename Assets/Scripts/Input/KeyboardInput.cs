using AlpacaMyGames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
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

    private Dictionary<KeyBinding, KeyCode> _keyBindingDictionary;

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

    private IBuildingManager _buildingManager;
    private IInteractableManager _interactableManager;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _buildingManager = BuildingManager.Instance;
        _interactableManager = InteractableManager.Instance;

        _keyBindingDictionary = new Dictionary<KeyBinding, KeyCode>()
        {
            { KeyBinding.WoodenOutpost, _woodenOutpostKey },
            { KeyBinding.WoodenTower, _woodenTowerKey },
            { KeyBinding.StoneTower, _stoneTowerKey },
            { KeyBinding.TowerComplex, _towerComplexKey },
            { KeyBinding.Fort, _fortKey },
            { KeyBinding.Castle, _castleKey }
        };
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
            foreach (Transform transform in BuildingsContainer.GetContainer())
                transform.GetComponent<Building>()?.Repair();
        }
    }
}
