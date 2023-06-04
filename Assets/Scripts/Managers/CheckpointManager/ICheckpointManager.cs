using UnityEngine;

public interface ICheckpointManager
{
    void GenerateCheckpoints();
    Transform GetNextWaypoint(Transform currentWaypoint = null);
    Transform GetSpawnPoint();
}