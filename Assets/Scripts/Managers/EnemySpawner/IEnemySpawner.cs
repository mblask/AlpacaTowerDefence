using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles spawning of <see cref="Enemy"/> objects
/// </summary>
public interface IEnemySpawner
{
    void SetupSpawner(LevelSettings levelSettings);

    /// <summary>
    /// Spawn a number of <see cref="Enemy"/> objects
    /// </summary>
    /// <param name="enemyNumber"></param>
    void SpawnEnemyWave(int enemyNumber = 5);

    /// <summary>
    /// Spawn one <see cref="Enemy"/> by <see cref="EnemyTemplate"/>
    /// </summary>
    /// <param name="position"></param>
    /// <param name="enemyTemplate"></param>
    Transform SpawnEnemy(Vector3 position, EnemyTemplate enemyTemplate);

    /// <summary>
    /// Enable or disable enemy spawning
    /// </summary>
    /// <param name="value"></param>
    void StartSpawning(bool value);
}