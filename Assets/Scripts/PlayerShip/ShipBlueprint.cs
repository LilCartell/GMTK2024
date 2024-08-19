using System.Collections.Generic;
using System.Linq;

public class ShipBlueprint
{
    private List<ShipPart> _shipParts;

    public void LoadWithShipParts(List<ShipPart> shipParts)
    {
        _shipParts = shipParts;
    }

    public List<ShipPart> GetShipParts() 
    {
        return _shipParts;
    }

    public int GetReactorsWithOrientation(Directions orientation)
    {
        int reactors = 0;
        foreach(var part in _shipParts)
        {
            if(part.Specification.ShipPartArchetype.ShipPartType == ShipPartType.REACTOR
                && part.Specification.Orientation == orientation)
            {
                ++reactors;
            }
        }
        return reactors;
    }

    public float GetTotalWeight()
    {
        return _shipParts.Sum(part => part.Specification.ShipPartArchetype.Weight);
    }

    public float GetTotalHull()
    {
        return _shipParts.Sum(part => part.Specification.ShipPartArchetype.Hull);
    }

    public float GetTotalCost()
    {
        return _shipParts.Sum(part => part.Specification.ShipPartArchetype.Cost);
    }

    public Coordinates GetCenterCoordinates()
    {
        float minX = _shipParts.Min(shipPart => shipPart.Coordinates.X);
        float maxX = _shipParts.Max(shipPart => shipPart.Coordinates.X);
        float minY = _shipParts.Min(shipPart => shipPart.Coordinates.Y);
        float maxY = _shipParts.Max(shipPart => shipPart.Coordinates.Y);
        return new Coordinates((float)(minX + maxX) / 2.0f, (float)(minY + maxY) / 2.0f);
    }
}
