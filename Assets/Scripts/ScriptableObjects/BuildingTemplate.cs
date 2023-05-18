using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTemplate : ScriptableObject
{
    public string Name;
    public BuildingEnum Building;
    public Sprite Sprite;
    public Color Color;
    public int Cost;
    public int Health;
    public int CrewNumber;
    public float BaseRepair;
}
