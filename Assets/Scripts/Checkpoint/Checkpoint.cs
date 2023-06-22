using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameAssets _gameAssets;
    private NatureContainer _natureContainer;

    [SerializeField] private bool _spawnFlag = true;

    private void Start()
    {
        if (!_spawnFlag)
            return;

        _gameAssets = GameAssets.Instance;
        _natureContainer = NatureContainer.Instance;
        Transform checkpointTransform = Instantiate(_gameAssets.CheckpointFlag, transform.position, Quaternion.identity, _natureContainer.transform);
        _natureContainer.AddElement(checkpointTransform);
    }
}
