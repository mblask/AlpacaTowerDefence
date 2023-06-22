public class EnemiesContainer : Container
{
    public static EnemiesContainer _instance;
    public static EnemiesContainer Instance
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
