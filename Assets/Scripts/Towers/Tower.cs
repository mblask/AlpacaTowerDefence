using UnityEngine;

public class Tower : Building, ITower
{
    private Transform _shootingSpotTransform;
    private Transform _towerRadiusMarker;

    public override float Health
    {
        get 
        { 
            return _towerHandler.Health; 
        }
    }

    private bool _isActive = true;

    [Header("Read-only")]
    [SerializeField] private ITowerHandler _towerHandler;

    private void Awake()
    {
        _shootingSpotTransform = transform.Find("ShootingSpot");
        _towerRadiusMarker = transform.Find("TowerRadiusMarker");
    }

    public void SetupTower(TowerTemplate towerTemplate)
    {
        name = towerTemplate.Name;
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

        if (_towerHandler.CrewMembers == 0)
            _isActive = false;

        if (_towerHandler.Health < 0.0f)
            Ruin();
    }

    public override void Repair()
    {
        _towerHandler.ToggleRepairs();
    }

    public void Ruin()
    {
        Destroy(gameObject);
    }

    public override void Interact()
    {
        //Debug.Log($"Interact with building: {name}");
        _towerRadiusMarker.gameObject.SetActive(true);
    }

    public override void StopInteracting()
    {
        //Debug.Log($"Stop interacting with building: {name}");
        _towerRadiusMarker.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StopInteracting();
        SelectionSprite.Instance.ResetSelection();
    }
}