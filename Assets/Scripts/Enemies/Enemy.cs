using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private SpriteRenderer _spriteRenderer;

    private bool _isActive = true;

    [SerializeField] private EnemyHandler _enemyHandler;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _enemyHandler.LayPath();
        _enemyHandler.HandleBehaviour();
        _enemyHandler.HandleDamageOverTime();
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
        _enemyHandler.TriggerDamageOverTime(duration, dps);
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

    public EnemyHandler GetEnemyHandler()
    {
        return _enemyHandler;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _enemyHandler.CurrentStats.Range);
    }
}