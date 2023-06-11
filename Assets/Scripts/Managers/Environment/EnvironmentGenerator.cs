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

    private EnvironmentContainer _environmentContainer;
    private BuildingsContainer _towerContainer;
    private EnemyContainer _enemyContainer;
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
        _environmentContainer = EnvironmentContainer.GetInstance();
        _enemyContainer = EnemyContainer.GetInstance();
        _towerContainer = BuildingsContainer.GetInstance();
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

        if (_environmentContainer == null )
            _environmentContainer = EnvironmentContainer.GetInstance();

        clearEnvironment();

        int bushCount = 30;
        int rockCount = 15;

        for (int i = 0; i < bushCount; i++)
            Instantiate(_gameAssets.Bush, Utilities.GetRandomWorldPositionFromScreen(), Quaternion.identity, _environmentContainer.GetNatureContainer());

        for (int i = 0; i < rockCount; i++)
            Instantiate(_gameAssets.Rock, Utilities.GetRandomWorldPositionFromScreen(), Quaternion.identity, _environmentContainer.GetNatureContainer());
    }

    public void LayPath(Vector3 position)
    {
        if (_gameAssets == null)
            _gameAssets = GameAssets.Instance;

        if (_environmentContainer == null)
            _environmentContainer = EnvironmentContainer.GetInstance();

        int maxPathPieces = 75;
        if (_environmentContainer.GetPathContainer().childCount > maxPathPieces)
            _environmentContainer.GetPathContainer().GetChild(Random.Range(0, _environmentContainer.GetPathContainer().childCount - 1))
                .GetComponent<DissolveAndDestroy>().Dissolve();

        Vector2 scale = Vector2.one * Random.Range(0.1f, 0.3f);
        Instantiate(_gameAssets.PathPiece, position, Quaternion.identity, _environmentContainer.GetPathContainer())
            .transform.localScale = scale;
    }

    private void clearEnvironment()
    {
        if (_environmentContainer != null)
            _environmentContainer.ClearContainer();

        if (_towerContainer != null)
            _towerContainer.ClearContainer();

        if (_enemyContainer != null)
            _enemyContainer.ClearContainer();
    }
}
