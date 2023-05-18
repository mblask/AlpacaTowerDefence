using AlpacaMyGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSpawner : MonoBehaviour, IParticleSystemSpawner
{
    private static ParticleSystemSpawner _instance;

    public static ParticleSystemSpawner Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void Spawn(Transform particleSystem, Vector3 position, float destroyAfter = 1.0f)
    {
        Instantiate(particleSystem, position, Quaternion.identity, null)
            .DestroyObject(destroyAfter);
    }
}
