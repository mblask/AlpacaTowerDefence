using AlpacaMyGames;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour, IResourceSpawner
{
    private static ResourceSpawner _instance;
    public static ResourceSpawner Instance
    {
        get 
        { 
            return _instance;
        }
    }

    private IResourcesManager _resourceManager;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _resourceManager = ResourcesManager.Instance;
    }

    public void SpawnResources(EnemyTemplate enemyTemplate)
    {
        _resourceManager.UpdateGold(enemyTemplate.GoldDrop.GetRandom());
    }
}
