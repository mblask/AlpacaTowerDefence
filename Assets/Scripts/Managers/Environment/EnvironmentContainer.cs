using UnityEngine;

public class EnvironmentContainer : Container
{
    private static EnvironmentContainer _instance;

    private Transform _pathPieces;
    private Transform _naturePieces;

    private void Awake()
    {
        _instance = this;
        _pathPieces = transform.Find("PathPieces");
        _naturePieces = transform.Find("NaturePieces");
    }

    public static EnvironmentContainer GetInstance()
    {
        return _instance;
    }

    public override Transform GetContainer()
    {
        return transform;
    }

    public Transform GetPathContainer()
    {
        return _pathPieces;
    }

    public Transform GetNatureContainer()
    {
        return _naturePieces;
    }

    public override void ClearContainer()
    {
        foreach (Transform child in _naturePieces)
            if (child != null)
                Destroy(child.gameObject);

        foreach (Transform child in _pathPieces)
            if (child != null)
                Destroy(child.gameObject);
    }
}
