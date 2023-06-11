using UnityEngine;

public abstract class Container : MonoBehaviour
{
    public abstract Transform GetContainer();

    public abstract void ClearContainer();
}
