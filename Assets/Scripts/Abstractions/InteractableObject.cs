using UnityEngine;

/// <summary>
/// Object with which is possible to interact with a mouse pointer
/// </summary>
public abstract class InteractableObject : MonoBehaviour
{
    /// <summary>
    /// Interact with <see cref="InteractableObject"/>
    /// </summary>
    public abstract void Interact();

    /// <summary>
    /// Stop interaction with <see cref="InteractableObject"/>
    /// </summary>
    public abstract void StopInteracting();
}
