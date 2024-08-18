using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShipPartArchetype")]
public class ShipPartArchetype : ScriptableObject
{
    public ShipPartType ShipPartType;
    public string Name;
    public string Description;
    public float Weight;
    public float Shield;
    public float Cost;
    public float Hull;
    public Sprite Icon;
    public List<Directions> PossibleDirections;
    public GameObject ShmupScenePrefab;
}
