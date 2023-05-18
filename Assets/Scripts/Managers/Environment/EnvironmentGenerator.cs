using AlpacaMyGames;
using System.Collections;
using System.Collections.Generic;
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
    private GameAssets _gameAssets;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _gameAssets = GameAssets.Instance;
        _natureContainer = EnvironmentContainer.GetNaturePiecesTransform();
        _pathContainer = EnvironmentContainer.GetPathPiecesTransform();

        generateEnvironment();
    }

    private void generateEnvironment()
    {
        int bushCount = 30;
        int rockCount = 15;

        for (int i = 0; i < bushCount; i++)
        {
            Instantiate(_gameAssets.Bush, Utilities.GetRandomWorldPositionFromScreen(), Quaternion.identity, _natureContainer);
        }

        for (int i = 0; i < rockCount; i++)
        {
            Instantiate(_gameAssets.Rock, Utilities.GetRandomWorldPositionFromScreen(), Quaternion.identity, _natureContainer);
        }
    }

    public void LayPath(Vector3 position)
    {
        int maxPathPieces = 75;
        if (_pathContainer.childCount > maxPathPieces)
            _pathContainer.GetChild(Random.Range(0, _pathContainer.childCount - 1))
                .GetComponent<DissolveAndDestroy>().Dissolve();

        Vector2 scale = Vector2.one * Random.Range(0.1f, 0.3f);
        Instantiate(_gameAssets.PathPiece, position, Quaternion.identity, _pathContainer)
            .transform.localScale = scale;
    }
}
