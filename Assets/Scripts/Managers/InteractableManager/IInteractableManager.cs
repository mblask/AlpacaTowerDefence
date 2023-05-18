/// <summary>
/// Handles current <see cref="InteractableObject"/>
/// </summary>
public interface IInteractableManager
{
    /// <summary>
    /// Sets the current <see cref="InteractableObject"/> and interacts with it
    /// </summary>
    /// <param name="interactable"></param>
    void Interact(InteractableObject interactable);

    /// <summary>
    /// Resets the current <see cref="InteractableObject"/>
    /// </summary>
    void ResetInteractable();
}
