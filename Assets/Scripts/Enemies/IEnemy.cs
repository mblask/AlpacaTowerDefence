/// <summary>
/// Handles everything related to <see cref="Enemy"/> class
/// </summary>
public interface IEnemy
{   
    /// <summary>
    /// Initializes the <see cref="Enemy"/> object using <see cref="EnemyTemplate"/>
    /// </summary>
    /// <param name="enemyTemplate"></param>
    void SetupEnemy(EnemyTemplate enemyTemplate);

    /// <summary>
    /// Removes <see cref="Enemy"/> object from the scene
    /// </summary>
    void Die();

    /// <summary>
    /// Damages <see cref="Enemy"/> object for <paramref name="value"/> damage
    /// </summary>
    /// <param name="value"></param>
    void Damage(float value = 0.0f);
}
