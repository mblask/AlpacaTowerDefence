using AlpacaMyGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TowerHandler : ITowerHandler
{
    private Transform _towerTransform;
    private Transform _shootingSpotTransform;

    private List<Transform> _enemiesInSight = new List<Transform>();

    private TowerTemplate _template;
    private bool _isRepairing = false;
    [field: SerializeField] public float Health { get; set; }
    private float _shootingRate;
    private float _shootingPrecision;
    private float _range;
    private float _baseRepair;
    [field: SerializeField] public int CrewMembers { get; set; }
    [field: SerializeField] public float ResistDamage { get; set; }
    private Vector2 _damage;

    private float _checkEnemyRate;
    private float _enemyCheckingTimer = 0.0f;
    private float _timer = 0.0f;

    [field: SerializeField] public float ChanceToKillCrewMember { get; set; }

    private float _towerShootingRatePercentageWhileRepairing;

    private GameAssets _gameAssets;

    public TowerHandler(TowerTemplate template, Transform towerTransform, Transform shootingSpotTransform)
    {
        _towerTransform = towerTransform;
        _shootingSpotTransform = shootingSpotTransform;

        _template = template;
        _shootingRate = template.ShootingRate;
        _shootingPrecision = template.Precision;
        _range = template.Range;
        _baseRepair = template.BaseRepair;
        _damage = template.Damage;

        _checkEnemyRate = template.EnemyCheckRate;
        _towerShootingRatePercentageWhileRepairing = template.ShootingRatePercentageWhileRepairing;

        Health = template.Health;
        CrewMembers = template.CrewNumber;
        ResistDamage = template.ResistDamage;
        ChanceToKillCrewMember = template.ChanceToKillCrewMember;

        _gameAssets = GameAssets.Instance;
    }

    public void Damage(float damage)
    {
        Health -= damage * (1 - ResistDamage / 100);

        if (Utilities.ChanceFunc(ChanceToKillCrewMember))
            CrewMembers--;
    }

    public void ToggleRepairs()
    {
        _isRepairing = !_isRepairing;
        _shootingRate = _isRepairing ? _shootingRate * _towerShootingRatePercentageWhileRepairing : _template.ShootingRate;
    }

    public void RepairProcedure()
    {
        if (!_isRepairing)
            return;

        if (Health >= _template.Health)
        {
            _shootingRate = _template.ShootingRate;
            _isRepairing = false;
            return;
        }

        Health += _baseRepair * CrewMembers * Time.deltaTime;
    }

    public void ShootingProcedure()
    {
        if (_enemiesInSight.Count == 0)
            return;

        _timer += Time.deltaTime;

        if (_timer < _shootingRate)
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
            Vector2 targetPosition = (Vector2)randomTransform.position + Utilities.GetRandomVector2(_shootingPrecision);

            canonball.SetupCanonball(targetPosition, _damage);
        }
    }

    public void EnemyCheckingProcedure()
    {
        _enemyCheckingTimer += Time.deltaTime;

        if (_enemyCheckingTimer < _checkEnemyRate)
            return;
        _enemyCheckingTimer = 0.0f;

        List<Transform> colliders = Physics2D.OverlapCircleAll(_towerTransform.position, _range)
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
}
