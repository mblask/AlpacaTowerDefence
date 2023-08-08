using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container for all game assets
/// </summary>
public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance
    {
        get
        {
            return _instance;
        }
    }

    [Header("Building templates")]
    public List<BuildingTemplate> BuildingTemplates;

    [Header("Enemy templates")]
    public List<EnemyTemplate> EnemyTemplates;

    [Header("Objects")]
    public Transform Tower;
    public Transform Trap;
    public Transform Enemy;
    public Transform Canonball;
    public Transform Arrow;
    public Transform DestructionArea;
    public Transform CheckpointPrefab;
    public Transform Spawnpoint;
    public Transform Exitpoint;
    public Transform CheckpointFlag;
    public Transform Torch;

    [Header("Nature objects")]
    public Transform Bush;
    public Transform Rock;
    public Transform PathPiece;

    [Header("Particle systems")]
    public Transform CanonballExplodePS;
    public Transform BloodPS;
    public Transform FirePS;

    private void Awake()
    {
        _instance = this;
    }

    public BuildingTemplate GetTemplateByEnum(BuildingEnum buildingEnum)
    {
        return BuildingTemplates.Find(template => template.Building.Equals(buildingEnum));
    }
}
