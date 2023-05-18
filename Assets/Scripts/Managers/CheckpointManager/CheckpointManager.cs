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
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private List<Transform> _checkpoints = new List<Transform>();

    [SerializeField] private Transform _checkpointPrefab;

    private IEnemySpawner _enemySpawner;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _enemySpawner = EnemySpawner.Instance;

        generateCheckpoints();
    }

    private void generateCheckpoints()
    {
        clearCheckpoints();

        int spawnSide = Utilities.ChanceFunc(50) ? 1 : -1;
        _spawnPoint.position = Utilities.GetRandomXBoundaryPosition(1.0f, +spawnSide);
        _exitPoint.position = Utilities.GetRandomXBoundaryPosition(1.0f, -spawnSide);

        int numberOfCheckpoints = 10;
        float viewAngle = 45;
        Transform tempTransform = _spawnPoint;
        float totalDistance = Vector2.Distance(_exitPoint.position, _spawnPoint.position);
        Vector2 worldMargin = new Vector2(-0.5f, -1.0f);
        Vector2 newPosition;

        for (int i = 0; i < numberOfCheckpoints - 1; i++)
        {
            Vector2 direction = _exitPoint.position - tempTransform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x);
            float randomAngle = Random.Range(-viewAngle, viewAngle) * Mathf.Deg2Rad;
            angle += randomAngle;
            direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            float pieceDistance = totalDistance / (numberOfCheckpoints * Mathf.Abs(Mathf.Cos(randomAngle)));
            newPosition = (Vector2)tempTransform.position + direction * pieceDistance;

            if (!Utilities.IsInsideWorldScreen(newPosition, worldMargin) && i != 0 && i != numberOfCheckpoints - 1)
            {
                i--;
                continue;
            }
            
            tempTransform = Instantiate(_checkpointPrefab, newPosition, Quaternion.identity, null);
            _checkpoints.Add(tempTransform);
        }

        _enemySpawner.StartSpawning(true);
    }

    private void clearCheckpoints()
    {
        if (_checkpoints.Count == 0)
            return;

        foreach (Transform checkpoint in _checkpoints)
        {
            Destroy(checkpoint.gameObject);
        }

        _checkpoints.Clear();
    }

    public Transform GetSpawnPoint()
    {
        return _spawnPoint;
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
}
