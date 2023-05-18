using UnityEngine;

/// <summary>
/// Handles everything related to <see cref="Builder"/> class
/// </summary>
public interface IBuilder
{
    /// <summary>
    /// Builds a specific <see cref="Building"/> at <paramref name="position"/> depending on <see cref="BuildingTemplate"/>
    /// </summary>
    /// <param name="buildingTemplate"></param>
    /// <param name="position"></param>
    void Build(BuildingTemplate buildingTemplate, Vector3 position);
}