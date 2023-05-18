/// <summary>
/// Sprite showing which <see cref="InteractableObject"/> is selected
/// </summary>
public interface ISelectionSprite
{
    /// <summary>
    /// Set new <see cref="InteractableObject"/>
    /// </summary>
    /// <param name="interactible"></param>
    void SetInteractible(InteractableObject interactible);

    /// <summary>
    /// Disable <see cref="SelectionSprite"/>
    /// </summary>
    void ResetSelection();
}
