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

    private ILevelManager _levelManager;
    private IEnemySpawner _enemySpawner;
    private EnemiesContainer _enemyContainer;
    
    private void Awake()
    {
        _instance = this;
    }
    
    private void Start()
    {
        _levelManager = LevelManager.Instance;
        _enemySpawner = EnemySpawner.Instance;
        _enemyContainer = EnemiesContainer.Instance;
    }

    public void CheckLevelCompletion()
    {
        if (!_enemySpawner.LastWaveSpawned)
            return;

        if (_enemyContainer.ElementCount > 0)
            return;

        _levelManager.SetupLevel();
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
