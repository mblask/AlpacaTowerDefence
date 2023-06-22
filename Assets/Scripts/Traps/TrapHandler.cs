using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class TrapHandler
{
    private Transform _trapTransform;

    private float _enemyCheckingTimer;
    //private List<Transform> _enemiesInSight = new List<Transform>();
    private bool _enemiesInRange = false;

    private float _trapActivationTimer;

    private bool _trapTriggered = false;

    private TrapStats _initialStats;
    [field: SerializeField] public TrapStats CurrentStats { get; set; }

    public TrapHandler(TrapTemplate template, Transform trapTransform)
    {
        _trapTransform = trapTransform;

        _initialStats = new TrapStats(template);
        CurrentStats = new TrapStats(_initialStats);
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
        Debug.Log("Trigger trap and deactivate it");
        _trapTriggered = true;
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