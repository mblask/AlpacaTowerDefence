using AlpacaMyGames;
using UnityEngine;

public class BuildingView : MonoBehaviour, IBuildingView
{
    private static BuildingView _instance;
    public static BuildingView Instance
    {
        get
        {
            return _instance;
        }
    }

    private SpriteRenderer _spriteRenderer;

    private bool _isActive = false;
    private BuildingTemplate _buildingTemplate;

    private IBuilder _builder;

    private void Awake()
    {
        _instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _builder = Builder.Instance;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        transform.position = Utilities.GetMouseWorldLocation();
    }

    public void SetupView(BuildingTemplate buildingTemplate)
    {
        if (buildingTemplate == null)
        {
            _isActive = false;
            return;
        }

        if (_isActive && _buildingTemplate == buildingTemplate)
        {
            _isActive = false;
            _spriteRenderer.enabled = _isActive;
            return;
        }

        if (!_isActive)
        {
            _isActive = true;
            _spriteRenderer.enabled = _isActive;
        }

        setupViewParameters(buildingTemplate);
    }

    private void setupViewParameters(BuildingTemplate buildingTemplate)
    {
        transform.position = Utilities.GetMouseWorldLocation();
        _buildingTemplate = buildingTemplate;
        _spriteRenderer.sprite = buildingTemplate.Sprite;
        Color color = buildingTemplate.Color;
        color.a = 0.6f;
        _spriteRenderer.color = color;
    }

    public void Build()
    {
        if (!_isActive)
            return;

        Vector3 position = Utilities.GetMouseWorldLocation();
        _builder.Build(_buildingTemplate, position);
    }
}
