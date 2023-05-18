using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour, IResourcesManager
{
    private static ResourcesManager _instance;
    public static ResourcesManager Instance
    {
        get 
        { 
            return _instance; 
        }
    }

    [SerializeField] private int _gold;
    [SerializeField] private int _wood;
    [SerializeField] private int _stone;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateGold(int gold)
    {
        _gold += gold;
    }

    public void UpdateWood(int wood)
    {
        _wood += wood;
    }

    public void UpdateStone(int stone)
    {
        _stone += stone;
    }

    public int GetAvailableGold()
    {
        return _gold;
    }
}
