/// <summary>
/// Handles everything related to <see cref="Enemy"/> class
/// </summary>
public interface IEnemy
{   
    /// <summary>
    /// Initializes the <see cref="Enemy"/> object using <see cref="EnemyTemplate"/>
    /// </summary>
    /// <param name="enemyTemplate"></param>
    IEnemy SetupEnemy(EnemyTemplate enemyTemplate);

    /// <summary>
    /// Sets the <see cref="EnemyBehaviour"/> of one enemy unit
    /// </summary>
    /// <param name="enemyBehaviour"></param>
    IEnemy SetEnemyBehaviour(EnemyBehaviour enemyBehaviour);

    /// <summary>
    /// Sets the checkpoint group of the enemy
    /// </summary>
    /// <param name="checkpointGroup"></param>
    void SetCheckpointGroup(int checkpointGroup);

    /// <summary>
    /// Removes <see cref="Enemy"/> object from the scene
    /// </summary>
    void Die();

    /// <summary>
    /// Damages <see cref="Enemy"/> object for <paramref name="value"/> damage
    /// </summary>
    /// <param name="value"></param>
    void Damage(float value = 0.0f);

    /// <summary>
    /// Damages <see cref="Enemy"/> object over time; dps = damage per second
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="dps"></param>
    void DamageOverTime(float duration, float dps);

    /// <summary>
    /// Returns the <see cref="EnemyHandler"/> of the <see cref="Enemy"/> object
    /// </summary>
    /// <returns></returns>
    EnemyHandler GetEnemyHandler();
}
