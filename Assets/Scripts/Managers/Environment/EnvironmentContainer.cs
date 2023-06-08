using UnityEngine;

public class EnvironmentContainer : MonoBehaviour
{
    private static EnvironmentContainer _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static Transform GetTransform()
    {
        return _instance.transform;
    }

    public static Transform GetPathPiecesTransform()
    {
        return _instance.transform.Find("PathPieces");
    }

    public static Transform GetNaturePiecesTransform()
    {
        return _instance.transform.Find("NaturePieces");
    }
}
