using AlpacaMyGames;
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class TrapHandler
{
    private TrapTemplate _trapTemplate;
    private Transform _trapTransform;

    private float _enemyCheckingTimer;
    private bool _enemiesInRange = false;

    private float _trapActivationTimer;

    public bool TrapTriggered => _trapTriggered;
    private bool _trapTriggered = false;

    private TrapStats _initialStats;
    [field: SerializeField] public TrapStats CurrentStats { get; set; }

    private GameAssets _gameAssets;

    public TrapHandler(TrapTemplate template, Transform trapTransform)
    {
        _trapTemplate = template;
        _trapTransform = trapTransform;

        _initialStats = new TrapStats(template);
        CurrentStats = new TrapStats(_initialStats);

        _gameAssets = GameAssets.Instance;
    }

    public void Damage(float damage)
    {
        CurrentStats.Health -= damage * (1 - CurrentStats.ResistDamage / 100);
    }

    public void TriggerTrap()
    {
        if (!_enemiesInRange || _trapTriggered)
            return;

        _trapActivationTimer += Time.deltaTime;
        if (_trapActivationTimer < CurrentStats.TrapActivationTime)
            return;

        _trapActivationTimer = 0.0f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_trapTransform.position, CurrentStats.Range);
        switch (_trapTemplate.Building)
        {
            case BuildingEnum.SpikeTrap:
                foreach (Collider2D collider in colliders)
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy == null)
                        continue;

                    enemy.Damage(CurrentStats.Damage);
                }
                break;
            case BuildingEnum.FireTrap:
                int fireSources = 10;
                for (int i = 0; i< fireSources; i++)
                {
                    Vector2 position = _trapTransform.position + Utilities.GetRandomLengthVector3(CurrentStats.Range, false);
                    UnityEngine.Object.Instantiate(_gameAssets.Torch, position, Quaternion.identity)
                        .GetComponent<ITorch>().SetupStaticTorch(false);

                    foreach (Collider2D collider in colliders)
                    {
                        Enemy enemy = collider.GetComponent<Enemy>();
                        if (enemy == null)
                            continue;

                        enemy.DamageOverTime(CurrentStats.Damage, UnityEngine.Random.Range(2.0f, 4.0f));
                    }
                }
                break;
            case BuildingEnum.TarTrap:
                break;
            default:
                break;
        }

        _trapTriggered = true;
    }

    public void ResetTrap()
    {
        if (_trapTemplate.Building != BuildingEnum.SpikeTrap)
            return;

        _trapTriggered = false;
    }

    public void EnemyCheckingProcedure()
    {
        if (_trapTriggered)
            return;

        _enemyCheckingTimer += Time.deltaTime;

        if (_enemyCheckingTimer < CurrentStats.EnemyCheckRate)
            return;

        _enemyCheckingTimer = 0.0f;

        _enemiesInRange =
            Physics2D.OverlapCircleAll(_trapTransform.position, CurrentStats.Range)
            .Where(collider => collider.GetComponent<Enemy>() != null).Count() > 0;
    }
}