using System.Collections.Generic;
using UnityEngine;
using AlpacaMyGames;
using System.Linq;

public class Tower : Building, ITower
{
    private Transform _shootingSpotTransform;
    private Transform _towerRadiusMarker;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _health = 100.0f;
    public override float Health
    {
        get 
        { 
            return _health; 
        }
    }

    private float _shootingPrecision;
    private float _range;
    private Vector2 _damage;
    private float _shootingRate;
    private float _resistDamage;
    private int _crewMembers;
    private float _baseRepair;

    private List<Transform> _enemiesInSight = new List<Transform>();
    private float _checkEnemyRate = 0.1f;
    private float _enemyCheckingTimer = 0.0f;

    private float _timer = 0.0f;

    private GameAssets _gameAssets;

    private void Awake()
    {
        _shootingSpotTransform = transform.Find("ShootingSpot");
        _towerRadiusMarker = transform.Find("TowerRadiusMarker");

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _gameAssets = GameAssets.Instance;
    }

    public void SetupTower(TowerTemplate towerTemplate)
    {
        name = towerTemplate.Name;
        _health = towerTemplate.Health;
        _shootingPrecision = towerTemplate.Precision;
        _damage = towerTemplate.Damage;
        _shootingRate = towerTemplate.ShootingRate;
        _range = towerTemplate.Range;
        _spriteRenderer.color = towerTemplate.Color;
        _resistDamage = towerTemplate.ResistDamage;
        _crewMembers = towerTemplate.CrewNumber;
        _baseRepair = towerTemplate.BaseRepair;

        Vector3 scale = new Vector3(towerTemplate.Range, towerTemplate.Range, 0.0f) * 2.0f;
        _towerRadiusMarker.localScale = scale;
        _towerRadiusMarker.gameObject.SetActive(false);
    }

    private void Update()
    {
        enemyCheckingProcedure();
        shootingProcedure();
    }

    public override void Damage(float damage)
    {
        _health -= damage * (1 - _resistDamage / 100);

        if (_health < 0.0f)
        {
            Ruin();
        }
    }

    public void Ruin()
    {
        Destroy(gameObject);
    }

    public override void Interact()
    {
        Debug.Log($"Interact with building: {name}");
        _towerRadiusMarker.gameObject.SetActive(true);
    }

    public override void StopInteracting()
    {
        Debug.Log($"Stop interacting with building: {name}");
        _towerRadiusMarker.gameObject.SetActive(false);
    }

    private void shootingProcedure()
    {
        if (_enemiesInSight.Count == 0)
            return;

        _timer += Time.deltaTime;

        if (_timer < _shootingRate)
            return;

        _timer = 0.0f;
        fireMissile();
    }

    private void fireMissile()
    {
        _enemiesInSight.TrimExcess();

        Transform randomTransform = _enemiesInSight.GetRandomElement();
        if (randomTransform != null)
        {
            ICanonball canonball = Instantiate(_gameAssets.Canonball, _shootingSpotTransform.position, Quaternion.identity, null).GetComponent<ICanonball>();
            Vector2 targetPosition = (Vector2)randomTransform.position + Utilities.GetRandomVector2(_shootingPrecision);

            canonball.SetupCanonball(targetPosition, _damage);
        }
    }

    private void enemyCheckingProcedure()
    {
        _enemyCheckingTimer += Time.deltaTime;

        if (_enemyCheckingTimer >= _checkEnemyRate)
        {
            _enemyCheckingTimer = 0.0f;

            List<Transform> colliders = Physics2D.OverlapCircleAll(transform.position, _range)
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
}
