using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameAssets _gameAssets;
    private Transform _environment;

    [SerializeField] private bool _spawnFlag = true;

    private void Start()
    {
        if (!_spawnFlag)
            return;

        _gameAssets = GameAssets.Instance;
        _environment = EnvironmentContainer.GetNaturePiecesTransform();
        Instantiate(_gameAssets.CheckpointFlag, transform.position, Quaternion.identity, _environment);
    }
}
