public interface IBuildingManager
{
    /// <summary>
    /// Proceeds to building the selected <see cref="Building"/>
    /// </summary>
    void Build();
    
    /// <summary>
    /// Shows the view of the wooden outpost
    /// </summary>
    void ViewWoodenOutpost();
    
    /// <summary>
    /// Shows the view of the wooden tower
    /// </summary>
    void ViewWoodenTower();

    /// <summary>
    /// Shows the view of the stone tower
    /// </summary>
    void ViewStoneTower();
}
