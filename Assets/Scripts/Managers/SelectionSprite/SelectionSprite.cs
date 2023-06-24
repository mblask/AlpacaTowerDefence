using UnityEngine;

public class SelectionSprite : MonoBehaviour, ISelectionSprite
{
    private static SelectionSprite _instance;
    public static SelectionSprite Instance
    {
        get
        {
            return _instance;
        }
    }

    private bool _isActive = false;
    private Vector3 _defaultScale;

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = _isActive;
        _defaultScale = transform.localScale;
    }

    public void SetInteractible(InteractableObject interactable)
    {
        transform.position = interactable.transform.position;
        _isActive = true;
        _spriteRenderer.enabled = true;
        Scaling(interactable);
    }

    public void ResetSelection()
    {
        _isActive = false;

        if (_spriteRenderer != null)
            _spriteRenderer.enabled = false;
    }

    private void Scaling(InteractableObject interactable)
    {
        transform.localScale = _defaultScale;
        switch (interactable)
        {
            case Trap trap:
                transform.localScale = trap.transform.localScale * 1.25f;
                break;
            default:
                break;
        }
    }
}
