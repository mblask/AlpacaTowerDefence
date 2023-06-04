using AlpacaMyGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private SpriteRenderer _spriteRenderer;
    private EnemyTemplate _enemyTemplate;

    private bool _isActive = true;
    private float _health = 10.0f;
    private float _movingSpeed = 1.0f;
    private float _range = 1.0f;
    private float _towerSearchTimeInterval = 0.5f;

    private Transform _nextWaypoint = null;
    private Vector2 _movingDirection = Vector2.zero;
    private Vector3 _positionAroundWaypoint = Vector3.zero;

    private float _timer = 0.0f;

    private GameAssets _gameAssets;
    private ICheckpointManager _waypointManager;
    private IParticleSystemSpawner _particleSystemSpawner;
    private IEnvironmentGenerator _environmentGenerator;
    private IResourceSpawner _resourceSpawner;
    private ILevelChecker _levelChecker;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _waypointManager = CheckpointManager.Instance;
        _gameAssets = GameAssets.Instance;
        _particleSystemSpawner = ParticleSystemSpawner.Instance;
        _environmentGenerator = EnvironmentGenerator.Instance;
        _resourceSpawner = ResourceSpawner.Instance;
        _levelChecker = LevelChecker.Instance;
    }

    private void Update()
    {
        move();
        layPath();
        buildingSearchProcedure();
    }

    public void SetupEnemy(EnemyTemplate enemyTemplate)
    {
        _enemyTemplate = enemyTemplate;

        _movingDirection = Vector2.zero;
        _nextWaypoint = null;
        _positionAroundWaypoint = Vector3.zero;

        _isActive = enemyTemplate.IsActive;
        name = enemyTemplate.Name;
        _spriteRenderer.sprite = enemyTemplate.Sprite;
        _spriteRenderer.color = enemyTemplate.Color;
        _health = enemyTemplate.Health;
        _movingSpeed = enemyTemplate.MovingSpeed;
        _range = enemyTemplate.Range;
        _towerSearchTimeInterval = enemyTemplate.TowerSearchTimeInterval;
    }

    private void layPath()
    {
        float chanceToLayPath = 0.05f;
        if (!Utilities.ChanceFunc(chanceToLayPath))
            return;

        _environmentGenerator.LayPath(transform.position);
    }

    public void Damage(float value)
    {
        shake();
        _particleSystemSpawner.Spawn(_gameAssets.BloodPS, transform.position);

        _health -= value;

        if (_health > 0.0f)
            return;

        Die();
    }

    public void Die()
    {
        _levelChecker.RemoveEnemy(this.transform);
        _resourceSpawner.SpawnResources(_enemyTemplate);
        Destroy(gameObject);
    }

    private void move()
    {
        if (!_isActive)
            return;

        if (_nextWaypoint == null)
            getNextWaypoint();

        float distance = Vector2.Distance(transform.position, _positionAroundWaypoint);

        float waypointRange = 0.1f;
        if (distance > waypointRange)
        {
            Vector2 position = transform.position;
            position += _movingSpeed * _movingDirection * Time.deltaTime;
            transform.position = position;
        }
        else
            getNextWaypoint();
    }

    private void getNextWaypoint()
    {
        Transform temporary = _waypointManager.GetNextWaypoint(_nextWaypoint);
        if (temporary == _nextWaypoint)
        {
            _isActive = false;
            _levelChecker.IncrementEnemiesSurvived();
            _levelChecker.RemoveEnemy(this.transform);
            Destroy(gameObject);
        }

        _nextWaypoint = _waypointManager.GetNextWaypoint(_nextWaypoint);

        float radiusAroundWaypoint = 0.5f;
        _positionAroundWaypoint = _nextWaypoint.position + Utilities.GetRandomVector3(radiusAroundWaypoint);
        _movingDirection = _positionAroundWaypoint - transform.position;
        _movingDirection.Normalize();
    }

    private void shake()
    {
        StopCoroutine(nameof(shakeCoroutine));
        StartCoroutine(nameof(shakeCoroutine));
    }

    private IEnumerator shakeCoroutine()
    {
        float magnitude = 5.0f;
        float timer = 0.0f;
        float duration = 0.2f;
        Quaternion defaultRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        while (timer <= duration)
        {
            float angle = UnityEngine.Random.Range(-magnitude, magnitude);
            transform.Rotate(new Vector3(0.0f, 0.0f, angle));

            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = defaultRotation;
    }

    private void buildingSearchProcedure()
    {
        _timer += Time.deltaTime;

        if (_timer < _towerSearchTimeInterval)
            return;

        _timer = 0.0f;

        float chanceToAttack = 5.0f;
        if (!Utilities.ChanceFunc(chanceToAttack))
            return;

        checkForBuildingsInRange();
    }

    private void checkForBuildingsInRange()
    {
        List<Collider2D> colliders = Physics2D.OverlapCircleAll(transform.position, _range)
            .Where(collider => collider.GetComponent<Building>() != null)
            .ToList();

        if (colliders.Count == 0)
            return;

        Building building = colliders.GetRandomElement().GetComponent<Building>();
        if (building == null)
            return;

        attackBuilding(building);
    }

    private void attackBuilding(Building building)
    {
        ITorch torch = Instantiate(_gameAssets.Torch, transform.position, Quaternion.identity, null).GetComponent<ITorch>();
        torch.SetupTorch(building);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
