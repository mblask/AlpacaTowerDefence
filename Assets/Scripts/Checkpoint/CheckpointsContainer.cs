public class CheckpointsContainer : Container
{
    private static CheckpointsContainer _instance;
    public static CheckpointsContainer Instance
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
}
