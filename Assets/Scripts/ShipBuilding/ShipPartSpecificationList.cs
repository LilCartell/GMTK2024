using UnityEngine;

public class ShipPartArchetypeList : MonoBehaviour
{
    public GameObject listRoot;

    void Start()
    {
        foreach(var shipPartArchetype in Resources.LoadAll<ShipPartArchetype>(""))
        {
            foreach(var direction in shipPartArchetype.PossibleDirections)
            {
                foreach (var cell in listRoot.GetComponentsInChildren<ShipPartSpecificationCell>())
                {
                    if(cell.ShipPartType == shipPartArchetype.ShipPartType
                        && cell.Orientation == direction)
                    {
                        var specification = new ShipPartSpecification() { Orientation = direction, ShipPartArchetype = shipPartArchetype };
                        cell.GetComponent<ShipPartSpecificationCell>().LoadWithSpecification(specification);
                    }
                }
            }
        }
    }
}
