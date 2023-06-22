public class BuildingsContainer : Container
{
    private static BuildingsContainer _instance;
    public static BuildingsContainer Instance
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
