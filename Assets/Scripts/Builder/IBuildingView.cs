/// <summary>
/// Handles everything related to <see cref="BuildingView"/> class
/// </summary>
public interface IBuildingView
{
    /// <summary>
    /// Initiates building of chosen <see cref="BuildingTemplate"/> using <see cref="Builder"/>
    /// </summary>
    void Build();

    /// <summary>
    /// Sets up <see cref="BuildingView"/> with chosen <see cref="BuildingTemplate"/>
    /// </summary>
    /// <param name="buildingTemplate"></param>
    void SetupView(BuildingTemplate buildingTemplate);
}