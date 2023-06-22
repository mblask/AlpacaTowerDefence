using UnityEngine;

public class EnemyContainer : Container
{
    public static EnemyContainer _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static EnemyContainer GetInstance()
    {
        return _instance;
    }

    public override Transform GetContainer()
    {
        return _instance.transform;
    }

    public override void ClearContainer()
    {
        foreach (Transform child in transform)
            if (child != null)
                Destroy(child.gameObject);
    }
}
