using UnityEngine;

public interface IMissile
{
    float Speed { get; }
    void SetupMissile(Vector2 targetPosition, Vector2 damage);
    void MissileMeetsTarget();
}
