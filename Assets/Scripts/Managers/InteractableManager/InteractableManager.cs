using UnityEngine;

public class InteractableManager : MonoBehaviour, IInteractableManager
{
    private static InteractableManager _instance;
    public static InteractableManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private InteractableObject _currentInteractableObject;
    private ISelectionSprite _selectionSprite;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _selectionSprite = SelectionSprite.Instance;
    }

    public void Interact(InteractableObject interactable)
    {
        if (_currentInteractableObject != null)
            ResetInteractable();

        _currentInteractableObject = interactable;
        _selectionSprite.SetInteractible(interactable);
        _currentInteractableObject.Interact();
    }

    public void ResetInteractable()
    {
        if (_currentInteractableObject == null)
            return;

        _currentInteractableObject.StopInteracting();
        _currentInteractableObject = null;
        _selectionSprite.ResetSelection();
    }
}
