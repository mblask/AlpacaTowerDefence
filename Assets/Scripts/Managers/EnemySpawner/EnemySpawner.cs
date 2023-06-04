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

    [SerializeField] private bool _spawnEnemies = false;
    [SerializeField] private int _numberOfWaves = 5;

    private Transform _enemiesContainer;
    private List<EnemyTemplate> _enemiesToSpawn = new List<EnemyTemplate>();
    private List<Transform> _lastSpawnedWave = new List<Transform>();

    private GameAssets _gameAssets;
    private ICheckpointManager _checkpointManager;
    private ILevelChecker _levelChecker;

    private float _timer = 0.0f;
    private float _spawnTime;
    
    private void Awake()
    {
        _instance = this;
        _enemiesContainer = transform.Find("Enemies");
    }
    
    private void Start()
    {
        _gameAssets = GameAssets.Instance;
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
            SpawnEnemyWave();
            _timer = 0.0f;
            _spawnTime = Random.Range(5.0f, 10.0f);
        }
    }

    public void SetupSpawner(LevelSettings levelSettings)
    {
        _numberOfWaves = levelSettings.EnemyWaves;
        _enemiesToSpawn = levelSettings.Enemies;

        _spawnEnemies = true;
    }

    public void SpawnEnemyWave(int enemyNumber = 5)
    {
        if (_enemiesToSpawn.Count == 0)
            _enemiesToSpawn.Add(_gameAssets.EnemyTemplates.Find(template => template.Name == "Footman"));

        _lastSpawnedWave.Clear();
        for (int i = 0; i < enemyNumber; i++)
        {
            Vector2 spawnPosition = _checkpointManager.GetSpawnPoint().position + Utilities.GetRandomVector3(0.5f);
            _lastSpawnedWave.Add(SpawnEnemy(spawnPosition, _enemiesToSpawn.GetRandomElement()));
        }

        _levelChecker.AddEnemiesSpawned(enemyNumber);
        _numberOfWaves--;

        if (_numberOfWaves == 0)
        {
            _spawnEnemies = false;
            _levelChecker.SetLastWaveSpawned(_lastSpawnedWave);
        }
    }

    public Transform SpawnEnemy(Vector3 position, EnemyTemplate enemyTemplate)
    {
        Transform enemyTransform = Instantiate(_gameAssets.Enemy, position, Quaternion.identity, _enemiesContainer);
        enemyTransform.GetComponent<Enemy>().SetupEnemy(enemyTemplate);

        return enemyTransform;
    }

    public void StartSpawning(bool value)
    {
        _spawnEnemies = value;
    }
}
