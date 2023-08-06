using AlpacaMyGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EnemyHandler
{
    public bool IsActive { get; private set; }

    private EnemyBehaviour _enemyBehaviour;
    private int _checkpointGroup;

    private Transform _enemyTransform;
    private Transform _nextWaypoint;

    private EnemyTemplate _enemyTemplate;
    private Vector2 _movingDirection;
    private Vector3 _positionAroundWaypoint;

    private bool _isAttacking = false;
    private Building _buildingInSight;
    private float _buildingSearchTimer;
    private float _buildingAttackTimer;

    private float _damageOverTimeDuration;
    private float _damagePerSecond;
    private bool _isDamagedOverTime = false;
    private float _damageOverTimeTimer;

    [field: SerializeField] public EnemyStats CurrentStats { get; set; }

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
        Move();

        switch (_enemyBehaviour)
        {
            case EnemyBehaviour.Survive:
                //Go to exit checkpoint
                //Damage player buildings on the way
                //Survive
                BuildingSearchProcedure(() => FindBuildingsInRange());
                break;
            case EnemyBehaviour.Attack:
                //Find player building and attack it
                //If building destroyed go to next
                //Repeat until no buildings left
                //Go to exit checkpoint
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
        CurrentStats.Health -= value;
    }

    public void HandleDamageOverTime()
    {
        if (!_isDamagedOverTime)
            return;

        _damageOverTimeTimer += Time.deltaTime;

        if (_damageOverTimeTimer >= _damageOverTimeDuration)
        {
            _damageOverTimeTimer = 0.0f;
            _isDamagedOverTime = false;
            return;
        }

        CurrentStats.Health -= _damagePerSecond * Time.deltaTime;

        if (CurrentStats.Health <= 0.0f)
            Die();
    }

    public void TriggerDamageOverTime(float duration, float dps)
    {
        if (_isDamagedOverTime)
            return;

        _isDamagedOverTime = true;

        _damageOverTimeDuration = duration;
        _damagePerSecond = dps;
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

        if (_isAttacking)
            return;

        if (_nextWaypoint == null)
            GetNextWaypoint();

        float distance = Vector2.Distance(_enemyTransform.position, _positionAroundWaypoint);

        float waypointRange = 0.1f;
        if (distance > waypointRange)
        {
            Vector2 position = _enemyTransform.position;
            position += CurrentStats.MovingSpeed * _movingDirection * Time.deltaTime;
            _enemyTransform.position = position;
        }
        else
            GetNextWaypoint();
    }

    public void GetNextWaypoint()
    {
        Transform temporary = _checkpointManager.GetNextWaypointMultiple(_checkpointGroup, _nextWaypoint);
        if (temporary == _nextWaypoint)
        {
            IsActive = false;
            _enemiesContainer.RemoveAndDestroy(_enemyTransform, () =>
            {
                _levelChecker.IncrementEnemiesSurvived();
                _levelChecker.CheckLevelCompletion();
            });
        }

        _nextWaypoint = _checkpointManager.GetNextWaypointMultiple(_checkpointGroup, _nextWaypoint);

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
                .Where(collider => {
                    Building building = collider.GetComponent<Building>();
                    return building != null && building.IsVisible;
                    })
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
        float chanceToAttack = 5.0f;
        if (!Utilities.ChanceFunc(chanceToAttack))
            return;

        List<Collider2D> colliders = Physics2D.OverlapCircleAll(_enemyTransform.position, CurrentStats.Range)
            .Where(collider => {
                Building building = collider.GetComponent<Building>();
                return building != null && building.IsVisible;
            })
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
