/// <summary>
/// Handles every damagable object
/// </summary>
public interface IDamagable
{
    /// <summary>
    /// Damages object for <paramref name="value"/> damage
    /// </summary>
    /// <param name="value"></param>
    void Damage(float value);
}
