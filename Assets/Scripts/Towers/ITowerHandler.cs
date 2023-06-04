public interface ITowerHandler
{
    float ChanceToKillCrewMember { get; set; }
    int CrewMembers { get; set; }
    float Health { get; set; }
    float ResistDamage { get; set; }

    void Damage(float damage);
    void EnemyCheckingProcedure();
    void FireMissile();
    void RepairProcedure();
    void ShootingProcedure();
    void ToggleRepairs();
}