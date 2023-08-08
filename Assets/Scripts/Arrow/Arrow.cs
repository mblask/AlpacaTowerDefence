using AlpacaMyGames;
using UnityEngine;

public class Arrow : MonoBehaviour, IMissile
{
    private Rigidbody2D _rigidbody;

    private Vector2 _targetPosition;
    private Vector2 _movingDirection;
    private float _speed = 6.0f;
    public float Speed => _speed;
    private float _effectRadius = 0.2f;
    private Vector2 _damage;

    private GameAssets _gameAssets;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        move();
    }

    public void SetupMissile(Vector2 targetPosition, Vector2 damage)
    {
        _damage = damage;
        _targetPosition = targetPosition;
        _movingDirection = _targetPosition - (Vector2)transform.position;
        _movingDirection.Normalize();

        float angle = Mathf.Atan2(_movingDirection.y, _movingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    private void move()
    {
        if (_movingDirection == null || _movingDirection == Vector2.zero)
            return;

        float distance = Vector2.Distance(transform.position, _targetPosition);
        if (distance <= 0.1f)
            MissileMeetsTarget();

        Vector2 position = (Vector2)transform.position + _speed * Time.fixedDeltaTime * _movingDirection;
        _rigidbody.MovePosition(position);
    }

    public void MissileMeetsTarget()
    {
        checkHitRadius();
        Destroy(gameObject);
    }

    private void checkHitRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _effectRadius);
        foreach (Collider2D collider in colliders)
        {
            ITower tower = collider.GetComponent<ITower>();
            if (tower != null)
                continue;

            IEnemy enemy = collider.GetComponent<IEnemy>();
            if (enemy == null)
                continue;

            float damage = Random.Range(_damage.x, _damage.y);
            enemy.Damage(damage);
        }
    }
}
