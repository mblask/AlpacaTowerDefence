using System.Collections.Generic;
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
    /// Generate checkpoins across the map with multiple spawn points
    /// </summary>
    /// <param name="numberOfSpawnPoints"></param>
    void GenerateWithMultiSpawnPoints(int numberOfSpawnPoints);

    /// <summary>
    /// Retrieves the next checkpoint in a row, given current checkpoint
    /// </summary>
    /// <param name="currentWaypoint"></param>
    /// <returns>Checkpoint <see cref="Transform"/></returns>
    Transform GetNextWaypoint(Transform currentWaypoint = null);

    /// <summary>
    /// Retrieves the next checkpoint in a row for a given group, given current checkpoint
    /// </summary>
    /// <param name="groupNumber"></param>
    /// <param name="currentWaypoint"></param>
    /// <returns>Checkpoint <see cref="Transform"/></returns>
    Transform GetNextWaypointMultiple(int groupNumber, Transform currentWaypoint = null);

    /// <summary>
    /// Retrieves the level's spawn point
    /// </summary>
    /// <returns>Spawn point <see cref="Transform"/></returns>
    Transform GetSpawnPoint();

    /// <summary>
    /// Retrieves the level's spawn points
    /// </summary>
    /// <returns><see cref="List{T}"/> of spawn point <see cref="Transform"/></returns>
    List<Transform> GetSpawnPoints();
}