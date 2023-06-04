using UnityEngine;

public class BuildingsContainer : MonoBehaviour
{
    private static BuildingsContainer _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static Transform GetContainer()
    {
        return _instance.transform;
    }
}
