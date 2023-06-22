using AlpacaMyGames;
using UnityEngine;

public class LevelManager : MonoBehaviour, ILevelManager
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private IEnvironmentGenerator _environmentGenerator;
    private ICheckpointManager _checkpointManager;
    private IEnemySpawner _enemySpawner;

    [SerializeField] private LevelRules _levelRules;
    
    private void Awake()
    {
        _instance = this;
    }
    
    private void Start()
    {
        _environmentGenerator = EnvironmentGenerator.Instance;
        _checkpointManager = CheckpointManager.Instance;
        _enemySpawner = EnemySpawner.Instance;
    
        SetupLevel();
    }

    public void SetupLevel()
    {
        LevelSettings levelSettings = _levelRules.LevelSettings.GetRandomElement();

        _environmentGenerator.SetupEnvironment(levelSettings.EnvironmentType);
        _checkpointManager.GenerateCheckpoints();
        _enemySpawner.SetupSpawner(levelSettings);
    }
}
