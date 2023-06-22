using AlpacaMyGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EnemyHandler : IEnemyHandler
{
    public bool IsActive { get; private set; }

    private Transform _transform;
    private Transform _nextWaypoint;

    private EnemyTemplate _enemyTemplate;
    private float _movingSpeed;
    private Vector2 _movingDirection;
    private float _towerSearchTimeInterval;
    private Vector3 _positionAroundWaypoint;

    private EnemyStats _initialStats;
    [field:SerializeField] public EnemyStats CurrentStats { get; private set; }

    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float Range { get; set; }

    private float _timer;

    private IEnvironmentGenerator _environmentGenerator;
    private IParticleSystemSpawner _particleSystemSpawner;
    private ILevelChecker _levelChecker;
    private IResourceSpawner _resourceSpawner;
    private ICheckpointManager _checkpointManager;
    private EnemiesContainer _enemiesContainer;
    private GameAssets _gameAssets;

    public EnemyHandler(EnemyTemplate template, Transform transform)
    {
        IsActive = true;

        _transform = transform;
        _enemyTemplate = template;
        Health = template.Health;
        Range = template.Range;
        _movingSpeed = template.MovingSpeed;
        _towerSearchTimeInterval = template.BuildingSearchInterval;

        _environmentGenerator = EnvironmentGenerator.Instance;
        _particleSystemSpawner = ParticleSystemSpawner.Instance;
        _levelChecker = LevelChecker.Instance;
        _resourceSpawner = ResourceSpawner.Instance;
        _checkpointManager = CheckpointManager.Instance;
        _enemiesContainer = EnemiesContainer.Instance;
        _gameAssets = GameAssets.Instance;
    }

    public void HandleBehaviour()
    {

    }

    public void SetCheckpointGroup(int checkpointGroup)
    {

    }

    public void SetBehaviour(EnemyBehaviour enemyBehaviour)
    {

    }

    public void AttackProcedure()
    {

    }

    public void BuildingSearchProcedure(Action actionToPerform)
    {

    }
    public void FindBuildingAndAttackIt()
    {

    }

    public void FindBuildingsInRange()
    {

    }

    public void LayPath()
    {
        float chanceToLayPath = 0.05f;
        if (!Utilities.ChanceFunc(chanceToLayPath))
            return;

        _environmentGenerator.LayPath(_transform.position);
    }

    public void Damage(float value)
    {
        _particleSystemSpawner.Spawn(_gameAssets.BloodPS, _transform.position);
        Health -= value;
    }

    public void Die()
    {
        _resourceSpawner.SpawnResources(_enemyTemplate);
        _enemiesContainer.RemoveAndDestroy(_transform, () =>
        {
            _levelChecker.CheckLevelCompletion();
        });
    }

    public void Move()
    {
        if (!IsActive)
            return;

        if (_nextWaypoint == null)
            GetNextWaypoint();

        float distance = Vector2.Distance(_transform.position, _positionAroundWaypoint);

        float waypointRange = 0.1f;
        if (distance > waypointRange)
        {
            Vector2 position = _transform.position;
            position += _movingSpeed * _movingDirection * Time.deltaTime;
            _transform.position = position;
        }
        else
            GetNextWaypoint();
    }

    public void GetNextWaypoint()
    {
        Transform temporary = _checkpointManager.GetNextWaypoint(_nextWaypoint);
        if (temporary == _nextWaypoint)
        {
            IsActive = false;
            _enemiesContainer.RemoveAndDestroy(_transform, () =>
            {
                _levelChecker.IncrementEnemiesSurvived();
                _levelChecker.CheckLevelCompletion();
            });
        }

        _nextWaypoint = _checkpointManager.GetNextWaypoint(_nextWaypoint);

        float radiusAroundWaypoint = 0.5f;
        _positionAroundWaypoint = _nextWaypoint.position + Utilities.GetRandomVector3(radiusAroundWaypoint);
        _movingDirection = _positionAroundWaypoint - _transform.position;
        _movingDirection.Normalize();
    }

    public void BuildingSearchProcedure()
    {
        _timer += Time.deltaTime;

        if (_timer < _towerSearchTimeInterval)
            return;

        _timer = 0.0f;

        float chanceToAttack = 5.0f;
        if (!Utilities.ChanceFunc(chanceToAttack))
            return;

        CheckForBuildingsInRange();
    }

    public void CheckForBuildingsInRange()
    {
        List<Collider2D> colliders = Physics2D.OverlapCircleAll(_transform.position, Range)
            .Where(collider => collider.GetComponent<Building>() != null)
            .ToList();

        if (colliders.Count == 0)
            return;

        Building building = colliders.GetRandomElement().GetComponent<Building>();
        if (building == null)
            return;

        AttackBuilding(building);
    }

    public void AttackBuilding(Building building)
    {
        ITorch torch = UnityEngine.Object.Instantiate(_gameAssets.Torch, _transform.position, Quaternion.identity, null).GetComponent<ITorch>();
        torch.SetupTorch(building);
    }
}
