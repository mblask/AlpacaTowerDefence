using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Template", menuName = "Scriptable Objects / Enemy")]
public class EnemyTemplate : ScriptableObject
{
    public bool IsActive;
    public string Name;
    public Sprite Sprite;
    public Color Color;
    public float Health;
    public float MovingSpeed;
    public float Range;
    public float TowerSearchTimeInterval;
    public Vector2Int GoldDrop;
    public Vector2Int WoodDrop;
    public Vector2Int MetalDrop;
}
