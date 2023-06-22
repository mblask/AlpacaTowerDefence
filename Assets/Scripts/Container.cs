using System;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private List<Transform> _elementsList = new List<Transform>();
    public int ElementCount => _elementsList.Count;

    public void AddElement(Transform element)
    {
        _elementsList.Add(element);
    }

    public void RemoveAndDestroy(Transform element, Action beforeDeletion = null)
    {
        _elementsList.Remove(element);
        beforeDeletion?.Invoke();
        Destroy(element.gameObject);
    }

    public void RemoveAndDestroyAll()
    {
        foreach (Transform element in _elementsList)
            if (element != null)
                Destroy(element.gameObject);

        _elementsList.Clear();
    }

    public List<Transform> GetElements()
    {
        return _elementsList;
    }
}