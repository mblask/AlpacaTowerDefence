using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private SpriteRenderer _spriteRenderer;

    private bool _isActive = true;

    private bool _isDamagedOverTime = false;
    private float _damageOverTimeTimer = 0.0f;

    [SerializeField] private EnemyHandler _enemyHandler;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _enemyHandler.LayPath();
        _enemyHandler.HandleBehaviour();
    }

    public IEnemy SetupEnemy(EnemyTemplate enemyTemplate)
    {
        _isActive = enemyTemplate.IsActive;
        name = enemyTemplate.Name;
        _spriteRenderer.sprite = enemyTemplate.Sprite;
        _spriteRenderer.color = enemyTemplate.Color;

        _enemyHandler = new EnemyHandler(enemyTemplate, transform);

        return this;
    }

    public IEnemy SetEnemyBehaviour(EnemyBehaviour enemyBehaviour)
    {
        _enemyHandler.SetBehaviour(enemyBehaviour);
        return this;
    }

    public void SetCheckpointGroup(int checkpointGroup)
    {
        _enemyHandler.SetCheckpointGroup(checkpointGroup);
    }

    public void Damage(float value)
    {
        shake();
        _enemyHandler.Damage(value);

        if (_enemyHandler.CurrentStats.Health <= 0.0f)
            Die();
    }

    public void DamageOverTime(float duration, float dps)
    {
        //Enemy handler procedure!!
    }

    public void Die()
    {
        _enemyHandler.Die();
    }

    private void shake()
    {
        StopCoroutine(nameof(shakeCoroutine));
        StartCoroutine(nameof(shakeCoroutine));
    }

    private IEnumerator shakeCoroutine()
    {
        float magnitude = 5.0f;
        float timer = 0.0f;
        float duration = 0.2f;
        Quaternion defaultRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        while (timer <= duration)
        {
            float angle = UnityEngine.Random.Range(-magnitude, magnitude);
            transform.Rotate(new Vector3(0.0f, 0.0f, angle));

            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = defaultRotation;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _enemyHandler.CurrentStats.Range);
    }
}