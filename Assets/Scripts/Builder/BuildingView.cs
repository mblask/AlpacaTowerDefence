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
    private Vector2 _checkSurroundingBox;
    private BuildingTemplate _buildingTemplate;

    private IBuilder _builder;
    
    private void Awake()
    {
        _instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _checkSurroundingBox = new Vector2(0.9f, 0.9f);
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
        transform.localScale = buildingTemplate.LocalScale;
        Color color = buildingTemplate.Color;
        color.a = 0.6f;
        _spriteRenderer.color = color;
        _defaultColor = color;
    }

    private void checkSurroundings()
    {
        _canBuild = true;
        Collider2D[] colliders = Physics2D
            .OverlapBoxAll(transform.position + new Vector3(0.0f, transform.localScale.y / 2, 0.0f), _checkSurroundingBox, 0.0f);
        foreach (Collider2D collider in colliders)
        {
            ObstacleBase obstacle = collider.GetComponent<ObstacleBase>();
            Checkpoint checkpoint = collider.GetComponent<Checkpoint>();
            Building building = collider.GetComponent<Building>();
            if (obstacle != null || checkpoint != null || building != null)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, transform.localScale.y / 2, 0.0f), _checkSurroundingBox);
    }
}
