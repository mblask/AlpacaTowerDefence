using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    public static EnemiesContainer _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static Transform GetContainer()
    {
        return _instance.transform;
    }
}
