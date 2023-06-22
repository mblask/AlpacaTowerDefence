using AlpacaMyGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TowerHandler
{
    private Transform _towerTransform;
    private Transform _shootingSpotTransform;

    private List<Transform> _enemiesInSight = new List<Transform>();

    private TowerTemplate _towerTemplate;
    private TowerStats _initialStats;
    [field: SerializeField] public TowerStats CurrentStats { get; set; }
    private bool _isRepairing = false;

    private float _enemyCheckingTimer = 0.0f;
    private float _timer = 0.0f;

    private GameAssets _gameAssets;
    private TowerStatsHandler _towerStatsHandler;

    public TowerHandler(TowerTemplate template, Transform towerTransform, Transform shootingSpotTransform)
    {
        _towerTemplate = template;
        _towerTransform = towerTransform;
        _shootingSpotTransform = shootingSpotTransform;

        _towerStatsHandler = new TowerStatsHandler();

        _initialStats = _towerStatsHandler.GetFinalStats(template);
        CurrentStats = new TowerStats(_initialStats);

        _gameAssets = GameAssets.Instance;
    }

    public void Damage(float damage)
    {
        CurrentStats.Health -= damage * (1 - CurrentStats.ResistDamage / 100);

        if (CurrentStats.CrewNumber > 0 && Utilities.ChanceFunc(CurrentStats.ChanceToKillCrewMember))
            CurrentStats.CrewNumber--;
    }

    public void ToggleRepairs()
    {
        _isRepairing = !_isRepairing;
        CurrentStats.ShootingRate = _isRepairing ? 
            _initialStats.ShootingRate * _initialStats.ShootingRatePercentageWhileRepairing : _initialStats.ShootingRate;
    }

    public void RepairProcedure()
    {
        if (!_isRepairing)
            return;

        if (CurrentStats.Health > _initialStats.Health)
        {
            CurrentStats.ShootingRate = _initialStats.ShootingRate;
            _isRepairing = false;
            return;
        }

        CurrentStats.Health += _initialStats.BaseRepair * CurrentStats.CrewNumber * Time.deltaTime;
    }

    public void ShootingProcedure()
    {
        if (_enemiesInSight.Count == 0)
            return;

        float timeIncrement = Time.deltaTime * CurrentStats.CrewNumber / _initialStats.CrewNumber;
        _timer += timeIncrement;

        if (_timer < CurrentStats.ShootingRate)
            return;

        _timer = 0.0f;
        FireMissile();
    }

    public void FireMissile()
    {
        _enemiesInSight.TrimExcess();

        Transform randomTransform = _enemiesInSight.GetRandomElement();
        if (randomTransform != null)
        {
            ICanonball canonball = UnityEngine.Object.Instantiate(_gameAssets.Canonball, _shootingSpotTransform.position, Quaternion.identity, null).GetComponent<ICanonball>();
            Vector2 targetPosition = (Vector2)randomTransform.position + Utilities.GetRandomVector2(CurrentStats.Precision);

            canonball.SetupCanonball(targetPosition, CurrentStats.Damage);
        }
    }

    public void EnemyCheckingProcedure()
    {
        _enemyCheckingTimer += Time.deltaTime;

        if (_enemyCheckingTimer < CurrentStats.EnemyCheckRate)
            return;

        _enemyCheckingTimer = 0.0f;

        List<Transform> colliders = Physics2D.OverlapCircleAll(_towerTransform.position, CurrentStats.Range)
            .Where(collider => collider.GetComponent<Enemy>() != null)
            .Select(collider => collider.transform).ToList();

        for (int i = 0; i < colliders.Count; i++)
        {
            if (!_enemiesInSight.Contains(colliders[i]))
                _enemiesInSight.Add(colliders[i]);
        }

        for (int i = 0; i < _enemiesInSight.Count; i++)
        {
            if (!colliders.Contains(_enemiesInSight[i]))
            {
                _enemiesInSight.RemoveAt(i);
                i--;
            }
        }
    }

    public void UpdateStats()
    {
        _initialStats = _towerStatsHandler.GetFinalStats(_towerTemplate);
        TowerStats statDifference = _towerStatsHandler.GetTowerStatDifference(_towerTemplate);

        CurrentStats += statDifference;
    }
}
