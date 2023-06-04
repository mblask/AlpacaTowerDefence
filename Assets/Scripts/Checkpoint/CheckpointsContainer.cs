using UnityEngine;

public class CheckpointsContainer : MonoBehaviour
{
    private static CheckpointsContainer _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static Transform GetContainer()
    {
        return _instance.transform;
    }
}
