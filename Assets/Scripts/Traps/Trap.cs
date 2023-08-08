using UnityEngine;

public class Trap : Building, ITrap
{
    private SpriteRenderer _spriteRenderer;
    private Transform _trapRadiusMarker;

    public override bool IsVisible => _trapHandler.TrapTriggered;
    public override float Health => _trapHandler.CurrentStats.Health;

    [SerializeField] private TrapHandler _trapHandler;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trapRadiusMarker = transform.Find("TrapRadiusMarker");
    }

    private void Update()
    {
        _trapHandler.EnemyCheckingProcedure();
        _trapHandler.TriggerTrap();
    }

    public void SetupTrap(TrapTemplate trapTemplate)
    {
        name = trapTemplate.Name;
        _spriteRenderer.color = trapTemplate.Color;

        Vector3 scale = new Vector3(trapTemplate.Range, trapTemplate.Range, 0.0f) * 2.0f;
        _trapRadiusMarker.transform.localScale = scale;
        _trapRadiusMarker.gameObject.SetActive(false);

        _trapHandler = new TrapHandler(trapTemplate, transform);
    }

    public void ResetTrap()
    {
        _trapHandler.ResetTrap();
    }

    public override void Repair()
    {
        //No repairs for traps
    }

    public override void Damage(float value)
    {
        _trapHandler.Damage(value);

        if (_trapHandler.CurrentStats.Health <= 0.0f)
            DestroyBuilding();
    }

    public override void Interact()
    {
        _trapRadiusMarker.gameObject.SetActive(true);
    }

    public override void StopInteracting()
    {
        _trapRadiusMarker.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StopInteracting();
        SelectionSprite.Instance.ResetSelection();
    }
}
