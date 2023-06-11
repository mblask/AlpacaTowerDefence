/// <summary>
/// Used for setting enemy behaviour, either survive or attack. 
/// Mixed enemy behaviour is used exclusivelly for randomizing <see cref="EnemyBehaviour.Survive"/> and <see cref="EnemyBehaviour.Attack"/>
/// </summary>
public enum EnemyBehaviour
{
    Survive,
    Attack,
    Mixed
}