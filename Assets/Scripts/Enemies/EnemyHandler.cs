using AlpacaMyGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EnemyHandler : IEnemyHandler
{
    public bool IsActive { get; private set; }

    private EnemyBehaviour _enemyBehaviour;
    private int _checkpointGroup;

    private Transform _enemyTransform;
    private Transform _nextWaypoint;

    private EnemyTemplate _enemyTemplate;
    private float _movingSpeed;
    private Vector2 _movingDirection;
    private float _towerSearchTimeInterval;
    private Vector3 _positionAroundWaypoint;

    private bool _isAttacking = false;
    private Building _buildingInSight;
    private float _buildingSearchTimer;
    private float _buildingAttackTimer;

    [field:SerializeField] public EnemyStats CurrentStats { get; private set; }

    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float Range { get; set; }

    private float _timer;

    private IEnvironmentGenerator _environmentGenerator;
    private IParticleSystemSpawner _particleSystemSpawner;
    private ILevelChecker _levelChecker;
    private IResourceSpawner _resourceSpawner;
    private ICheckpointManager _checkpointManager;
    private BuildingsContainer _buildingsContainer;
    private EnemiesContainer _enemiesContainer;
    private GameAssets _gameAssets;

    public EnemyHandler(EnemyTemplate template, Transform transform)
    {
        IsActive = true;

        _enemyTransform = transform;
        _enemyTemplate = template;
        CurrentStats = new EnemyStats(template);

        _environmentGenerator = EnvironmentGenerator.Instance;
        _particleSystemSpawner = ParticleSystemSpawner.Instance;
        _levelChecker = LevelChecker.Instance;
        _resourceSpawner = ResourceSpawner.Instance;
        _checkpointManager = CheckpointManager.Instance;
        _buildingsContainer = BuildingsContainer.Instance;
        _enemiesContainer = EnemiesContainer.Instance;
        _gameAssets = GameAssets.Instance;
    }

    public void SetCheckpointGroup(int checkpointGroup)
    {
        _checkpointGroup = checkpointGroup;
    }

    public void SetBehaviour(EnemyBehaviour enemyBehaviour)
    {
        _enemyBehaviour = enemyBehaviour;
    }

    public void HandleBehaviour()
    {
        switch (_enemyBehaviour)
        {
            case EnemyBehaviour.Survive:
                //Go to exit checkpoint
                //Damage player buildings on the way
                //Survive
                Move();
                BuildingSearchProcedure(() => FindBuildingsInRange());
                break;
            case EnemyBehaviour.Attack:
                //Find player building and attack it
                //If building destroyed go to next
                //Repeat until no buildings left
                //Go to exit checkpoint
                Move();
                BuildingSearchProcedure(() => FindBuildingAndAttackIt());
                AttackProcedure();
                break;
            default:
                break;
        }
    }

    public void LayPath()
    {
        if (_isAttacking)
            return;

        float chanceToLayPath = 0.05f;

        if (!Utilities.ChanceFunc(chanceToLayPath))
            return;

        _environmentGenerator.LayPath(_enemyTransform.position);
    }

    public void Damage(float value)
    {
        _particleSystemSpawner.Spawn(_gameAssets.BloodPS, _enemyTransform.position);
        Health -= value;
    }

    public void Die()
    {
        _resourceSpawner.SpawnResources(_enemyTemplate);
        _enemiesContainer.RemoveAndDestroy(_enemyTransform, () =>
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

        float distance = Vector2.Distance(_enemyTransform.position, _positionAroundWaypoint);

        float waypointRange = 0.1f;
        if (distance > waypointRange)
        {
            Vector2 position = _enemyTransform.position;
            position += _movingSpeed * _movingDirection * Time.deltaTime;
            _enemyTransform.position = position;
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
            _enemiesContainer.RemoveAndDestroy(_enemyTransform, () =>
            {
                _levelChecker.IncrementEnemiesSurvived();
                _levelChecker.CheckLevelCompletion();
            });
        }

        _nextWaypoint = _checkpointManager.GetNextWaypoint(_nextWaypoint);

        float radiusAroundWaypoint = 0.5f;
        _positionAroundWaypoint = _nextWaypoint.position + Utilities.GetRandomVector3(radiusAroundWaypoint);
        _movingDirection = _positionAroundWaypoint - _enemyTransform.position;
        _movingDirection.Normalize();
    }

    public void BuildingSearchProcedure(Action actionToPerform)
    {
        if (_isAttacking)
            return;

        _buildingSearchTimer += Time.deltaTime;

        if (_buildingSearchTimer < CurrentStats.BuildingSearchTime)
            return;

        _buildingSearchTimer = 0.0f;

        actionToPerform.Invoke();
    }

    public void FindBuildingAndAttackIt()
    {
        _isAttacking = false;
        if (_buildingsContainer.ElementCount == 0)
            return;

        if (_buildingInSight == null)
        {
            List<Collider2D> colliders = Physics2D.OverlapCircleAll(_enemyTransform.position, CurrentStats.Range)
            .Where(collider => collider.GetComponent<Building>() != null)
            .ToList();

            if (colliders.Count == 0)
                return;

            _buildingInSight = colliders.GetRandomElement().GetComponent<Building>();
        }

        _isAttacking = true;

        AttackBuilding(_buildingInSight);
    }

    public void FindBuildingsInRange()
    {
        List<Collider2D> colliders = Physics2D.OverlapCircleAll(_enemyTransform.position, Range)
            .Where(collider => collider.GetComponent<Building>() != null)
            .ToList();

        if (colliders.Count == 0)
            return;

        Building building = colliders.GetRandomElement().GetComponent<Building>();
        AttackBuilding(building);
    }

    public void AttackProcedure()
    {
        if (!_isAttacking)
            return;

        _buildingAttackTimer += Time.deltaTime;

        if (_buildingAttackTimer < CurrentStats.BuildingAttackTime)
            return;

        _buildingAttackTimer = 0.0f;
        if (_buildingInSight == null)
        {
            _isAttacking = false;
            return;
        }

        AttackBuilding(_buildingInSight);
    }

    public void AttackBuilding(Building building)
    {
        ITorch torch = UnityEngine.Object.Instantiate(_gameAssets.Torch, _enemyTransform.position, Quaternion.identity, null).GetComponent<ITorch>();
        torch.SetupTorch(building);
    }
}
