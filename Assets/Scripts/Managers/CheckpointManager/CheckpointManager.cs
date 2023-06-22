using AlpacaMyGames;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour, ICheckpointManager
{
    private static CheckpointManager _instance;
    public static CheckpointManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    [SerializeField] private Transform _exitPoint;
    [SerializeField] private List<Transform> _checkpoints = new List<Transform>();

    private int _numberOfCheckpoints = 10;

    private GameAssets _gameAssets;
    private CheckpointsContainer _checkpointsContainer;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _checkpointsContainer = CheckpointsContainer.Instance;
    }

    public void GenerateWithMultiSpawnPoints(int numberOfSpawnPoints)
    {
        clearCheckpoints();

        if (_gameAssets == null)
            _gameAssets = GameAssets.Instance;

        _checkpointsContainer = CheckpointsContainer.Instance;

        int spawnSide = Utilities.ChanceFunc(50) ? 1 : -1;
        for (int i = 0; i < numberOfSpawnPoints; i++)
            _spawnPoints.Add(Instantiate(_gameAssets.Spawnpoint, Utilities.GetRandomXBoundaryPosition(1.0f, +spawnSide), 
                Quaternion.identity, _checkpointsContainer.transform));

        _exitPoint = Instantiate(_gameAssets.Exitpoint, Utilities.GetRandomXBoundaryPosition(1.0f, -spawnSide),
            Quaternion.identity, _checkpointsContainer.transform);

        float viewAngle = 45;

        foreach (Transform spawnPoint in _spawnPoints)
        {
            Transform tempTransform = spawnPoint;
            float totalDistance = Vector2.Distance(_exitPoint.position, spawnPoint.position);
            Vector2 worldMargin = new Vector2(-0.5f, -1.0f);
            Vector2 newPosition;

            for (int i = 0; i < _numberOfCheckpoints - 1; i++)
            {
                Vector2 direction = _exitPoint.position - tempTransform.position;
                direction.Normalize();

                float angle = Mathf.Atan2(direction.y, direction.x);
                float randomAngle = Random.Range(-viewAngle, viewAngle) * Mathf.Deg2Rad;
                angle += randomAngle;
                direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                float pieceDistance = totalDistance / (_numberOfCheckpoints * Mathf.Abs(Mathf.Cos(randomAngle)));
                newPosition = (Vector2)tempTransform.position + direction * pieceDistance;

                if (!Utilities.IsInsideWorldScreen(newPosition, worldMargin) && i != 0 && i != _numberOfCheckpoints - 1)
                {
                    i--;
                    continue;
                }

                tempTransform = Instantiate(_gameAssets.CheckpointPrefab, newPosition, Quaternion.identity, _checkpointsContainer.transform);
                _checkpoints.Add(tempTransform);
            }
        }
    }

    public void GenerateCheckpoints()
    {
        clearCheckpoints();

        _checkpointsContainer = CheckpointsContainer.Instance;

        if (_gameAssets == null)
            _gameAssets = GameAssets.Instance;

        int spawnSide = Utilities.ChanceFunc(50) ? 1 : -1;
        _spawnPoint = Instantiate(_gameAssets.Spawnpoint, Utilities.GetRandomXBoundaryPosition(1.0f, +spawnSide),
            Quaternion.identity, _checkpointsContainer.transform);
        _exitPoint = Instantiate(_gameAssets.Exitpoint, Utilities.GetRandomXBoundaryPosition(1.0f, -spawnSide),
            Quaternion.identity, _checkpointsContainer.transform);

        float viewAngle = 45;
        Transform tempTransform = _spawnPoint;
        float totalDistance = Vector2.Distance(_exitPoint.position, _spawnPoint.position);
        Vector2 worldMargin = new Vector2(-0.5f, -1.0f);
        Vector2 newPosition;

        for (int i = 0; i < _numberOfCheckpoints - 1; i++)
        {
            Vector2 direction = _exitPoint.position - tempTransform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x);
            float randomAngle = Random.Range(-viewAngle, viewAngle) * Mathf.Deg2Rad;
            angle += randomAngle;
            direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            float pieceDistance = totalDistance / (_numberOfCheckpoints * Mathf.Abs(Mathf.Cos(randomAngle)));
            newPosition = (Vector2)tempTransform.position + direction * pieceDistance;

            if (!Utilities.IsInsideWorldScreen(newPosition, worldMargin) && i != 0 && i != _numberOfCheckpoints - 1)
            {
                i--;
                continue;
            }
            
            tempTransform = Instantiate(_gameAssets.CheckpointPrefab, newPosition, Quaternion.identity, _checkpointsContainer.transform);
            _checkpointsContainer.AddElement(tempTransform);
            _checkpoints.Add(tempTransform);
        }
    }

    private void clearCheckpoints()
    {
        if (_spawnPoint != null)
            Destroy(_spawnPoint.gameObject);

        foreach (Transform spawnPoint in _spawnPoints)
            if (spawnPoint != null)
                Destroy(spawnPoint.gameObject);

        _spawnPoints.Clear();

        if (_exitPoint != null)
            Destroy(_exitPoint.gameObject);

        if (_checkpoints.Count == 0)
            return;

        foreach (Transform checkpoint in _checkpoints)
            if (checkpoint != null)
                Destroy(checkpoint.gameObject);

        _checkpoints.Clear();
    }

    public Transform GetSpawnPoint()
    {
        return _spawnPoint;
    }

    public List<Transform> GetSpawnPoints()
    {
        return _spawnPoints;
    }

    public Transform GetNextWaypoint(Transform currentWaypoint = null)
    {
        if (currentWaypoint == null)
            return _checkpoints[0];

        if (currentWaypoint == _exitPoint)
            return _exitPoint;

        int index = _checkpoints.IndexOf(currentWaypoint);

        if (index != _checkpoints.Count - 1)
            return _checkpoints[index + 1];
        else
            return _exitPoint;
    }

    public Transform GetNextWaypointMultiple(int groupNumber, Transform currentWaypoint = null)
    {
        if (currentWaypoint == null)
            return _checkpoints[_numberOfCheckpoints * groupNumber];

        if (currentWaypoint == _exitPoint)
            return _exitPoint;

        int index = _checkpoints.IndexOf(currentWaypoint);

        if (index != (groupNumber + 1) * (_numberOfCheckpoints - 1) - 1)
            return _checkpoints[index + 1];
        else
            return _exitPoint;
    }
}
