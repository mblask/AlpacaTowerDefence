using UnityEngine;

public interface IEnvironmentGenerator
{
    void SetupEnvironment(EnvironmentType environmentType);
    void LayPath(Vector3 position);
}