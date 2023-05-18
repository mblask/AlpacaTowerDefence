using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAndDestroy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _dissolvingSpeed = 1.0f;

    private bool _isDissolving = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_spriteRenderer == null)
            return;

        if (!_isDissolving)
            return;

        Color color = _spriteRenderer.color;

        if (color.a <= 0.0f)
            Destroy(gameObject);

        color.a -= _dissolvingSpeed * Time.deltaTime;
        _spriteRenderer.color = color;
    }

    public void Dissolve()
    {
        _isDissolving = true;
    }
}
