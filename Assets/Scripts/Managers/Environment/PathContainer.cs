public class PathContainer : Container
{
    private static PathContainer _instance;
    public static PathContainer Instance
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
