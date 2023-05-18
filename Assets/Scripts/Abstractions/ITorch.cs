/// <summary>
/// Handles everything related to <see cref="Torch"/> class
/// </summary>
public interface ITorch
{
    /// <summary>
    /// Sets up a new instance of <see cref="Torch"/> object
    /// </summary>
    /// <param name="targetBuilding"></param>
    void SetupTorch(Building targetBuilding);
}