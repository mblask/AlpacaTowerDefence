using UnityEngine;

public class Tower : Building, ITower
{
    private SpriteRenderer _spriteRenderer;
    private Transform _shootingSpotTransform;
    private Transform _towerRadiusMarker;

    public override float Health
    {
        get 
        {
            return _towerHandler.CurrentStats.Health;
        }
    }

    private bool _isActive = true;

    [SerializeField] private TowerHandler _towerHandler;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _shootingSpotTransform = transform.Find("ShootingSpot");
        _towerRadiusMarker = transform.Find("TowerRadiusMarker");
    }

    public void SetupTower(TowerTemplate towerTemplate)
    {
        name = towerTemplate.Name;
        _spriteRenderer.color = towerTemplate.Color;
        _towerHandler = new TowerHandler(towerTemplate, transform, _shootingSpotTransform);

        Vector3 scale = new Vector3(towerTemplate.Range, towerTemplate.Range, 0.0f) * 2.0f;
        _towerRadiusMarker.localScale = scale;
        _towerRadiusMarker.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isActive)
            return;

        _towerHandler.EnemyCheckingProcedure();
        _towerHandler.ShootingProcedure();
        _towerHandler.RepairProcedure();
    }

    public override void Damage(float damage)
    {
        _towerHandler.Damage(damage);

        if (_towerHandler.CurrentStats.CrewNumber == 0)
            _isActive = false;

        if (_towerHandler.CurrentStats.Health < 0.0f)
            DestroyBuilding();
    }

    public override void Repair()
    {
        _towerHandler.ToggleRepairs();
    }

    public void UpdateStats()
    {
        _towerHandler.UpdateStats();
    }

    public override void Interact()
    {
        _towerRadiusMarker.gameObject.SetActive(true);
    }

    public override void StopInteracting()
    {
        _towerRadiusMarker.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StopInteracting();
        SelectionSprite.Instance.ResetSelection();
    }
}