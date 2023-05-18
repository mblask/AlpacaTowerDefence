using UnityEngine;

public interface IParticleSystemSpawner
{
    void Spawn(Transform particleSystem, Vector3 position, float destroyAfter = 1);
}