/// <summary>
/// Abstract class used as base for all buildings
/// </summary>
public abstract class Building : InteractableObject
{
    /// <summary>
    /// Exposend health property of the <see cref="Building"/>
    /// </summary>
    public abstract float Health
    {
        get;
    }

    /// <summary>
    /// Damages <see cref="Building"/> for <paramref name="value"/> damage
    /// </summary>
    /// <param name="value"></param>
    public abstract void Damage(float value);
}
