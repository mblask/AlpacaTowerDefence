using System;
using System.Collections.Generic;

[Serializable]
public class LevelSettings
{
    public EnvironmentType EnvironmentType;
    public List<EnemyTemplate> Enemies;
    public int EnemyWaves;
    public EnemyBehaviour EnemyBehaviour;
}