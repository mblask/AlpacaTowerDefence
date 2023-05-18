using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles level stats and actions
/// </summary>
public interface ILevelChecker
{
    int EnemiesSpawned { get; }

    int EnemiesSurvived { get; }

    /// <summary>
    /// Remove dead <see cref="Enemy"/>'s transform from the last wave list
    /// </summary>
    /// <param name="enemyTransform"></param>
    void RemoveEnemy(Transform enemyTransform);

    /// <summary>
    /// Add <see cref="Enemy"/> transforms from last wave to a list
    /// </summary>
    /// <param name="lastWaveSpawned"></param>
    void SetLastWaveSpawned(List<Transform> lastWaveSpawned);

    /// <summary>
    /// Add a number of <see cref="Enemy"/> objects spawned
    /// </summary>
    /// <param name="enemiesSpawned"></param>
    void AddEnemiesSpawned(int enemiesSpawned);

    /// <summary>
    /// Increase enemies survived by one
    /// </summary>
    void IncrementEnemiesSurvived();

    /// <summary>
    /// Sets the number of enemies spawned to 0
    /// </summary>
    void ResetEnemiesSpawned();

    /// <summary>
    /// Sets the number of enemies survived to 0
    /// </summary>
    void ResetEnemiesSurvived();
}