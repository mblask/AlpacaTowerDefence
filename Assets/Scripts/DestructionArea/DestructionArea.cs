using UnityEngine;

public class DestructionArea : MonoBehaviour, IDestructionArea
{
    private float _expansionSpeed = 20.0f;
    private float _maxArea = 0.5f;

    private bool _isActive = false;

    private void Update()
    {
        if (!_isActive)
            return;

        float scale = transform.localScale.x;
        if (scale >= _maxArea)
            Destroy(gameObject);
        
        scale += _expansionSpeed * Time.deltaTime;
        transform.localScale = Vector2.one * scale;
    }

    public void Setup(float hitRadius)
    {
        _isActive = true;
        _maxArea = hitRadius;
    }
}
