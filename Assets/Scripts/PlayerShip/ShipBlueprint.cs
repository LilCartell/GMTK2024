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
            if(part.Specification.Orientation == orientation)
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
}
