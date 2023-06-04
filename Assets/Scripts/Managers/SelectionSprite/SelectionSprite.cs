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

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = _isActive;
    }

    public void SetInteractible(InteractableObject interactible)
    {
        transform.position = interactible.transform.position;
        _isActive = true;
        _spriteRenderer.enabled = true;
    }

    public void ResetSelection()
    {
        _isActive = false;

        if (_spriteRenderer != null)
            _spriteRenderer.enabled = false;
    }
}
