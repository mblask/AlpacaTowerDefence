using AlpacaMyGames;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private Transform _transform;

    private IBuildingManager _buildingManager;
    private IInteractableManager _interactableManager;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _buildingManager = BuildingManager.Instance;
        _interactableManager = InteractableManager.Instance;
    }

    private void Update()
    {
        followMouse();
        onLeftMouseUp();
    }

    private void followMouse()
    {
        _transform.position = Input.mousePosition;
    }

    private void onLeftMouseUp()
    {
        if (!Input.GetMouseButtonUp(0))
            return;

        checkForInteractables();
        buildBuilding();
    }

    private void buildBuilding()
    {
        _buildingManager.Build();
    }

    private void checkForInteractables()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(Utilities.GetMouseWorldLocation());

        foreach (Collider2D collider in colliders)
        {
            InteractableObject interactable = collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                _interactableManager.Interact(interactable);
                return;
            }
        }

        _interactableManager.ResetInteractable();
    }
}
