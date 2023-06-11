using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles spawning of <see cref="Enemy"/> objects
/// </summary>
public interface IEnemySpawner
{
    void SetupSpawner(LevelSettings levelSettings);

    /// <summary>
    /// Spawns a number of <see cref="Enemy"/> objects
    /// </summary>
    /// <param name="enemyNumber"></param>
    void SpawnWave(int enemyNumber = 5);

    /// <summary>
    /// Spawns a number of <see cref="Enemy"/> objects from different spawn points
    /// </summary>
    /// <param name="enemyNumber"></param>
    void SpawnWaveMultiSpawnpoints(int enemyNumber = 5);

    /// <summary>
    /// Spawn one <see cref="Enemy"/> by <see cref="EnemyTemplate"/>
    /// </summary>
    /// <param name="position"></param>
    /// <param name="enemyTemplate"></param>
    Transform SpawnEnemy(Vector3 position, EnemyTemplate enemyTemplate, int checkpointGroup = 0);

    /// <summary>
    /// Enable or disable enemy spawning
    /// </summary>
    /// <param name="value"></param>
    void StartSpawning(bool value);
}