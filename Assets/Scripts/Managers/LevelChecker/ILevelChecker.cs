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
    /// Checks the completion objectives of the level and if they are accomplished loads a new level
    /// </summary>
    void CheckLevelCompletion();

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