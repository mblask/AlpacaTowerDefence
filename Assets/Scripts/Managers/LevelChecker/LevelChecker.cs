using System.Collections.Generic;
using UnityEngine;

public class LevelChecker : MonoBehaviour, ILevelChecker
{
    private static LevelChecker _instance;
    public static LevelChecker Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private int _enemiesSpawned;
    public int EnemiesSpawned => _enemiesSpawned;
    [SerializeField] private int _enemiesSurvived;
    public int EnemiesSurvived => _enemiesSurvived;

    private List<Transform> _lastEnemyWave;
    private bool _lastWaveSpawned = false;

    private EnemyContainer _enemyContainer;

    private ILevelManager _levelManager;
    
    private void Awake()
    {
        _instance = this;
    }
    
    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _enemyContainer = EnemyContainer.GetInstance();
    }

    public void RemoveEnemy(Transform enemyTransform)
    {
        if (!_lastWaveSpawned)
            return;

        _lastEnemyWave.Remove(enemyTransform);

        if (_lastEnemyWave.Count == 0)
        {
            Debug.Log("Level finished");

            _lastWaveSpawned = false;
            _lastEnemyWave = new List<Transform>();

            _enemyContainer.ClearContainer();

            _enemiesSpawned = 0;
            _enemiesSurvived = 0;
            _levelManager.Setup();
        }
    }

    public void SetLastWaveSpawned(List<Transform> lastWaveSpawned)
    {
        _lastWaveSpawned = true;
        _lastEnemyWave = new List<Transform>(lastWaveSpawned);
    }

    public void AddEnemiesSpawned(int enemiesSpawned)
    {
        _enemiesSpawned += enemiesSpawned;
    }

    public void ResetEnemiesSpawned()
    {
        _enemiesSpawned = 0;
    }

    public void IncrementEnemiesSurvived()
    {
        _enemiesSurvived++;
    }

    public void ResetEnemiesSurvived()
    {
        _enemiesSurvived = 0;
    }
}
