using System;

public interface IEnemyHandler
{
    public EnemyStats CurrentStats { get; }
    bool IsActive { get; }

    void HandleBehaviour();
    void AttackBuilding(Building building);
    void AttackProcedure();
    void BuildingSearchProcedure(Action actionToPerform);
    void FindBuildingAndAttackIt();
    void FindBuildingsInRange();
    void Damage(float value);
    void Die();
    void GetNextWaypoint();
    void LayPath();
    void Move();
}