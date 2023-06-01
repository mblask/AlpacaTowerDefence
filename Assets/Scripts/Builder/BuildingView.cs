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
    private Color _defaultColor;

    private bool _isActive = false;
    private bool _canBuild = true;
    private BuildingTemplate _buildingTemplate;

    private IBuilder _builder;
    private float _checkSurroundingsRadius = 0.75f;

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
        checkSurroundings();
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
        _defaultColor = color;
    }

    private void checkSurroundings()
    {
        if (!_isActive)
            return;

        _canBuild = true;
        Collider2D[] colliders = Physics2D
            .OverlapCircleAll(transform.position + new Vector3(0.0f, transform.localScale.y / 2, 0.0f), _checkSurroundingsRadius);
        foreach (Collider2D collider in colliders)
        {
            ObstacleBase obstacle = collider.GetComponent<ObstacleBase>();
            Checkpoint checkpoint = collider.GetComponent<Checkpoint>();
            if (obstacle != null || checkpoint != null)
            {
                _canBuild = false;
                _spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, _defaultColor.a);
                return;
            }
        }

        _spriteRenderer.color = _defaultColor;
    }

    public void Build()
    {
        if (!_isActive)
            return;

        if (!_canBuild)
            return;

        Vector3 position = Utilities.GetMouseWorldLocation();
        _builder.Build(_buildingTemplate, position);
    }
}
