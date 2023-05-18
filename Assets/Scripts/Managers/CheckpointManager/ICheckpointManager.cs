using UnityEngine;

public interface ICheckpointManager
{
    Transform GetNextWaypoint(Transform currentWaypoint = null);
    Transform GetSpawnPoint();
}