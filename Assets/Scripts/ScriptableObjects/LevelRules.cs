using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Rule", menuName = "Scriptable Objects / Level Rules")]
public class LevelRules : ScriptableObject
{
    public List<LevelSettings> LevelSettings;
}
