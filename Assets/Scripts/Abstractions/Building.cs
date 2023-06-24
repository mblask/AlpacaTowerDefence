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
    /// By default buildings are visible, but <see cref="Trap"/> objects can be invisible
    /// </summary>
    public virtual bool IsVisible
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// Damages <see cref="Building"/> for <paramref name="value"/> damage
    /// </summary>
    /// <param name="value"></param>
    public abstract void Damage(float value);

    /// <summary>
    /// Destroys the <see cref="Building"/>
    /// </summary>
    public void DestroyBuilding()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Toggle repair process of a building, effectivelly lowering its effectivity but slowly repairing damages
    /// </summary>
    public abstract void Repair();
}
