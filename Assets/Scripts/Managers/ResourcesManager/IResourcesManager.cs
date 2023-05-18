public interface IResourcesManager
{
    void UpdateGold(int gold);

    void UpdateWood(int wood);
    
    void UpdateStone(int stone);
    
    int GetAvailableGold();
}