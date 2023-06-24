using UnityEngine;
using AlpacaMyGames;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    private static EnemySpawner _instance;
    public static EnemySpawner Instance
    {
        get
        {
            return _instance;
        }
    }

    private EnemyBehaviour _enemyBehaviour;
    [SerializeField] private bool _spawnEnemies = false;
    [SerializeField] private int _numberOfWaves = 5;

    private List<EnemyTemplate> _enemiesToSpawn = new List<EnemyTemplate>();
    private bool _lastWaveSpawned = false;
    public bool LastWaveSpawned => _lastWaveSpawned;

    private GameAssets _gameAssets;
    private EnemiesContainer _enemiesContainer;
    private ICheckpointManager _checkpointManager;
    private ILevelChecker _levelChecker;

    private float _timer = 0.0f;
    private float _spawnTime;
    
    private void Awake()
    {
        _instance = this;
    }
    
    private void Start()
    {
        _gameAssets = GameAssets.Instance;
        _enemiesContainer = EnemiesContainer.Instance;
        _checkpointManager = CheckpointManager.Instance;
        _levelChecker = LevelChecker.Instance;
    
        _spawnTime = Random.Range(0.0f, 2.0f);
    }

    private void Update()
    {
        spawnWaves();
    }

    private void spawnWaves()
    {
        if (!_spawnEnemies)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _spawnTime)
        {
            SpawnWave();
            _timer = 0.0f;
            _spawnTime = Random.Range(5.0f, 10.0f);
        }
    }

    public void SetupSpawner(LevelSettings levelSettings)
    {
        _numberOfWaves = levelSettings.EnemyWaves;
        _enemiesToSpawn = levelSettings.Enemies;
        _enemyBehaviour = levelSettings.EnemyBehaviour;

        _spawnEnemies = true;
    }

    public void SpawnWaveMultiSpawnpoints(int enemyNumber = 5)
    {
        if (_enemiesToSpawn.Count == 0)
            _enemiesToSpawn.Add(_gameAssets.EnemyTemplates.Find(template => template.Name == "Footman"));

        List<Transform> spawnPoints = _checkpointManager.GetSpawnPoints();
        int randomSpawnpointIndex = Random.Range(0, spawnPoints.Count);
        Transform randomSpawnPoint = spawnPoints[randomSpawnpointIndex];
        for (int i = 0; i < enemyNumber; i++)
        {
            Vector2 spawnPosition = randomSpawnPoint.position + Utilities.GetRandomVector3(0.5f);
            SpawnEnemy(spawnPosition, _enemiesToSpawn.GetRandomElement(), randomSpawnpointIndex);
        }

        _levelChecker.AddEnemiesSpawned(enemyNumber);
        _numberOfWaves--;

        if (_numberOfWaves == 0)
        {
            _spawnEnemies = false;
        }
    }

    public void SpawnWave(int enemyNumber = 5)
    {
        if (_enemiesToSpawn.Count == 0)
            _enemiesToSpawn.Add(_gameAssets.EnemyTemplates.Find(template => template.Name == "Footman"));

        List<Transform> spawnPoints = _checkpointManager.GetSpawnPoints();
        int randomSpawnpointIndex = Random.Range(0, spawnPoints.Count);
        for (int i = 0; i < enemyNumber; i++)
        {
            Vector2 spawnPosition = _checkpointManager.GetSpawnPoints().GetRandomElement().position + Utilities.GetRandomVector3(0.5f);
            SpawnEnemy(spawnPosition, _enemiesToSpawn.GetRandomElement(), randomSpawnpointIndex);
        }

        _levelChecker.AddEnemiesSpawned(enemyNumber);
        _numberOfWaves--;

        if (_numberOfWaves == 0)
        {
            _spawnEnemies = false;
            _lastWaveSpawned = true;
        }
    }

    public Transform SpawnEnemy(Vector3 position, EnemyTemplate enemyTemplate, int checkpointGroup)
    {
        Transform enemyTransform = 
            Instantiate(_gameAssets.Enemy, position, Quaternion.identity, _enemiesContainer.transform);
        _enemiesContainer.AddElement(enemyTransform);
        EnemyBehaviour behaviour = _enemyBehaviour == EnemyBehaviour.Mixed ?
            Utilities.GetRandomEnumValueExcluding(EnemyBehaviour.Mixed) : _enemyBehaviour;
        enemyTransform.GetComponent<IEnemy>()
            .SetupEnemy(enemyTemplate)
            .SetEnemyBehaviour(behaviour)
            .SetCheckpointGroup(checkpointGroup);

        return enemyTransform;
    }

    public void StartSpawning(bool value)
    {
        _spawnEnemies = value;
    }
}
