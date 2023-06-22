public class NatureContainer : Container
{
    private static NatureContainer _instance;
    public static NatureContainer Instance
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
