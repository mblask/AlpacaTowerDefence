using System;
using UnityEngine;

public interface ICheckpointsContainer
{
    int CheckpointsCount { get; }

    void AddCheckpoint(Transform checkpoint);
    void RemoveAndDestroy(Transform checkpoint, Action beforeDeletion);
}