using AlpacaMyGames;
using UnityEngine;

public class Canonball : MonoBehaviour, ICanonball
{    
    private Rigidbody2D _rigidbody;

    private Vector2 _targetPosition;
    private Vector2 _movingDirection;
    private float _speed = 5.0f;
    private float _destructionRadius = 0.4f;
    private Vector2 _damage;

    private GameAssets _gameAssets;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _gameAssets = GameAssets.Instance;
    }

    private void FixedUpdate()
    {
        move();
    }

    public void SetupCanonball(Vector2 targetPosition, Vector2 damage)
    {
        _damage = damage;
        _targetPosition = targetPosition;
        _movingDirection = _targetPosition - (Vector2)transform.position;
        _movingDirection.Normalize();
    }

    private void move()
    {
        if (_movingDirection == null || _movingDirection == Vector2.zero)
            return;

        float distance = Vector2.Distance(transform.position, _targetPosition);
        if (distance <= 0.1f)
            canonballMeetsTarget();

        Vector2 position = (Vector2)transform.position + _speed * Time.fixedDeltaTime * _movingDirection;
        _rigidbody.MovePosition(position);
    }

    private void canonballMeetsTarget()
    {
        checkHitRadius();

        float destroyObjectAfter = 1.0f;
        Instantiate(_gameAssets.CanonballExplodePS, transform.position, Quaternion.identity, null)
            .DestroyObject(destroyObjectAfter);

        IDestructionArea destructionArea = Instantiate(_gameAssets.DestructionArea, transform.position, Quaternion.identity, null).GetComponent<IDestructionArea>();
        destructionArea.Setup(_destructionRadius);

        Destroy(gameObject);
    }

    private void checkHitRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _destructionRadius);
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
