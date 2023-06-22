using AlpacaMyGames;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour, IEnvironmentGenerator
{
    private static EnvironmentGenerator _instance;

    public static EnvironmentGenerator Instance
    {
        get
        {
            return _instance;
        }
    }

    private NatureContainer _natureContainer;
    private PathContainer _pathContainer;
    private BuildingsContainer _buildingsContainer;
    private EnemiesContainer _enemyContainer;
    private Camera _camera;
    private GameAssets _gameAssets;

    private void Awake()
    {
        _instance = this;
        _camera = Camera.main;
    }

    private void Start()
    {
        _gameAssets = GameAssets.Instance;
        _natureContainer = NatureContainer.Instance;
        _pathContainer = PathContainer.Instance;
        _buildingsContainer = BuildingsContainer.Instance;
        _enemyContainer = EnemiesContainer.Instance;
    }

    public void SetupEnvironment(EnvironmentType environmentType)
    {
        if (_camera == null)
            _camera = Camera.main;

        switch (environmentType)
        {
            case EnvironmentType.Forest:
                _camera.backgroundColor = new Color(0.08f, 0.39f, 0.0f, 0.0f);
                break;
            case EnvironmentType.Desert:
                _camera.backgroundColor = new Color(0.75f, 0.75f, 0.39f, 0.0f);
                break;
            default:
                _camera.backgroundColor = new Color(0.08f, 0.39f, 0.0f, 0.0f);
                break;
        }

        generateEnvironment();
    }

    private void generateEnvironment()
    {
        if (_gameAssets == null)
            _gameAssets = GameAssets.Instance;

        if (_natureContainer == null )
            _natureContainer = NatureContainer.Instance;

        clearEnvironment();

        int bushCount = 30;
        int rockCount = 15;

        for (int i = 0; i < bushCount; i++)
        {
            Transform transform = Instantiate(_gameAssets.Bush, Utilities.GetRandomWorldPositionFromScreen(), Quaternion.identity, _natureContainer.transform);
            _natureContainer.AddElement(transform);
        }

        for (int i = 0; i < rockCount; i++)
        {
            Transform transform = Instantiate(_gameAssets.Rock, Utilities.GetRandomWorldPositionFromScreen(), Quaternion.identity, _natureContainer.transform);
            _natureContainer.AddElement(transform);
        }
    }

    public void LayPath(Vector3 position)
    {
        if (_gameAssets == null)
            _gameAssets = GameAssets.Instance;

        if (_pathContainer == null)
            _pathContainer = PathContainer.Instance;

        int maxPathPieces = 75;
        if (_pathContainer.ElementCount > maxPathPieces)
            _pathContainer.transform.GetChild(Random.Range(0, _pathContainer.transform.childCount - 1))
                .GetComponent<DissolveAndDestroy>().Dissolve();

        Vector2 scale = Vector2.one * Random.Range(0.1f, 0.3f);
        Transform pathTransform = Instantiate(_gameAssets.PathPiece, position, Quaternion.identity, _pathContainer.transform);
        pathTransform.transform.localScale = scale;
        _pathContainer.AddElement(pathTransform);
    }

    private void clearEnvironment()
    {
        if (_natureContainer != null)
            _natureContainer.RemoveAndDestroyAll();

        if (_pathContainer != null)
            _pathContainer.RemoveAndDestroyAll();

        if (_buildingsContainer != null)
            _buildingsContainer.RemoveAndDestroyAll();

        if (_enemyContainer != null)
            _enemyContainer.RemoveAndDestroyAll();
    }
}
