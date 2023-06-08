using UnityEngine;

/// <summary>
/// Handles eveything related to level <see cref="Checkpoint"/> objects
/// </summary>
public interface ICheckpointManager
{
    /// <summary>
    /// Generate checkpoints across the map through which the enemies will travel
    /// </summary>
    void GenerateCheckpoints();

    /// <summary>
    /// Retrieves the next checkpoint in a row, given current checkpoint
    /// </summary>
    /// <param name="currentWaypoint"></param>
    /// <returns>Checkpoint <see cref="Transform"/></returns>
    Transform GetNextWaypoint(Transform currentWaypoint = null);

    /// <summary>
    /// Retrieves the spawn point in the level
    /// </summary>
    /// <returns>Spawn point <see cref="Transform"/></returns>
    Transform GetSpawnPoint();
}