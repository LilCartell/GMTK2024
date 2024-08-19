using System;
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
    public Sprite ShopIcon;
    public List<SpriteForOrientation> SpritesByOrientation;
    public List<Directions> PossibleDirections;
    public GameObject ShmupScenePrefab;

    public Sprite GetSpriteByOrientation(Directions orientation)
    {
        foreach(var spriteByOrientation in SpritesByOrientation)
        {
            if (spriteByOrientation.Orientation == orientation)
                return spriteByOrientation.Sprite;
        }
        return Icon;
    }
}

[Serializable]
public class SpriteForOrientation
{
    public Sprite Sprite;
    public Directions Orientation;
}