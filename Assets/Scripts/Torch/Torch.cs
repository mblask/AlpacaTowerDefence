using AlpacaMyGames;
using UnityEngine;

public class Torch : MonoBehaviour, ITorch
{
    private bool _isMoving = true;
    private bool _isRotating = true;

    private Vector2 _movingDirection;
    private Vector2 _targetPosition;
    private float _speed = 2.0f;
    private float _rotationalSpeed = 60.0f;
    private float _rotationDirection = 1.0f;
    private Vector2 _damage = new Vector2(1.0f, 2.0f);
    private Building _targetBuilding;

    public void SetupTorch(Building targetBuilding)
    {
        _targetBuilding = targetBuilding;
        _targetPosition = targetBuilding.transform.position;
        _movingDirection = targetBuilding.transform.position - transform.position;
        _movingDirection.Normalize();

        _rotationalSpeed = Random.Range(30.0f, 90.0f);
        _rotationDirection = Utilities.ChanceFunc(50) ? 1.0f : -1.0f;
    }

    public void SetupStaticTorch(bool isMoving, bool isRotating)
    {
        _isMoving = isMoving;
        _isRotating = isRotating;
        Destroy(gameObject, Random.Range(4.0f, 8.0f));
    }

    private void Update()
    {
        move();
        rotate();
    }

    private void move()
    {
        if (!_isMoving)
            return;

        if (_movingDirection == null || _movingDirection == Vector2.zero)
            return;

        if (_targetBuilding.Health <= 0.0f)
            Destroy(gameObject);

        float distance = Vector2.Distance(transform.position, _targetPosition);
        if (distance <= 0.1f)
            torchMeetsTarget();

        Vector2 position = (Vector2)transform.position + _speed * Time.deltaTime * _movingDirection;
        transform.position = position;
    }

    private void rotate()
    {
        if (!_isRotating)
            return;

        transform.Rotate(0.0f, 0.0f, _rotationDirection * _rotationalSpeed * Time.deltaTime);
    }

    private void torchMeetsTarget()
    {
        _targetBuilding?.Damage(Random.Range(_damage.x, _damage.y));
        Destroy(gameObject);
    }
}
