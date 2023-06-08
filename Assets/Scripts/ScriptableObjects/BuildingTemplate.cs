using UnityEngine;

/// <summary>
/// General building template
/// </summary>
public class BuildingTemplate : ScriptableObject
{
    /// <summary>
    /// Name of a building
    /// </summary>
    public string Name;

    /// <summary>
    /// Type of a building
    /// </summary>
    public BuildingEnum Building;

    /// <summary>
    /// Building sprite
    /// </summary>
    public Sprite Sprite;

    /// <summary>
    /// Building color
    /// </summary>
    public Color Color;

    /// <summary>
    /// Building build cost
    /// </summary>
    public int Cost;

    /// <summary>
    /// Building health
    /// </summary>
    public int Health;

    /// <summary>
    /// Building crew number
    /// </summary>
    public int CrewNumber;

    /// <summary>
    /// Building base repair
    /// </summary>
    public float BaseRepair;
}
