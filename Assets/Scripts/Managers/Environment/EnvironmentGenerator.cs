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

    private Transform _natureContainer;
    private Transform _pathContainer;
    private Transform _towerContainer;
    private Transform _enemyContainer;
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
        _natureContainer = EnvironmentContainer.GetNaturePiecesTransform();
        _pathContainer = EnvironmentContainer.GetPathPiecesTransform();
        _towerContainer = BuildingsContainer.GetContainer();
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
            _natureContainer = EnvironmentContainer.GetNaturePiecesTransform();

        clearEnvironment();

        int bushCount = 30;
        int rockCount = 15;

        for (int i = 0; i < bushCount; i++)
            Instantiate(_gameAssets.Bush, Utilities.GetRandomWorldPositionFromScreen(), Quaternion.identity, _natureContainer);

        for (int i = 0; i < rockCount; i++)
            Instantiate(_gameAssets.Rock, Utilities.GetRandomWorldPositionFromScreen(), Quaternion.identity, _natureContainer);
    }

    public void LayPath(Vector3 position)
    {
        if (_gameAssets == null)
            _gameAssets = GameAssets.Instance;

        if (_pathContainer == null)
            _pathContainer = EnvironmentContainer.GetPathPiecesTransform();

        int maxPathPieces = 75;
        if (_pathContainer.childCount > maxPathPieces)
            _pathContainer.GetChild(Random.Range(0, _pathContainer.childCount - 1))
                .GetComponent<DissolveAndDestroy>().Dissolve();

        Vector2 scale = Vector2.one * Random.Range(0.1f, 0.3f);
        Instantiate(_gameAssets.PathPiece, position, Quaternion.identity, _pathContainer)
            .transform.localScale = scale;
    }

    private void clearEnvironment()
    {
        if (_natureContainer != null)
            foreach (Transform transform in _natureContainer)
                Destroy(transform.gameObject);

        if (_pathContainer != null)
            foreach (Transform transform in _pathContainer)
                Destroy(transform.gameObject);

        if (_towerContainer != null)
            foreach (Transform transform in _towerContainer)
                Destroy(transform.gameObject);

        if (_enemyContainer != null)
            foreach (Transform transform in _enemyContainer)
                Destroy(transform.gameObject);
    }
}
