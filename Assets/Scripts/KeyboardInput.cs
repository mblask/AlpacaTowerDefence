using AlpacaMyGames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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

    [SerializeField] private KeyCode _woodenOutpostKey;
    [SerializeField] private KeyCode _woodenTowerKey;
    [SerializeField] private KeyCode _stoneTowerKey;
    [SerializeField] private KeyCode _towerComplexKey;
    [SerializeField] private KeyCode _fortKey;
    [SerializeField] private KeyCode _castleKey;

    private IBuildingManager _buildingManager;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _buildingManager = BuildingManager.Instance;

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
}
