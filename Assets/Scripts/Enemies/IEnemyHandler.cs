public interface IEnemyHandler
{
    float Health { get; set; }
    bool IsActive { get; }
    float Range { get; set; }

    void AttackBuilding(Building building);
    void BuildingSearchProcedure();
    void CheckForBuildingsInRange();
    void Damage(float value);
    void Die();
    void GetNextWaypoint();
    void LayPath();
    void Move();
}