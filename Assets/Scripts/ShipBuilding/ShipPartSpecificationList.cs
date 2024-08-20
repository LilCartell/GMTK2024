using UnityEngine;
using UnityEngine.UI;

public class ShipPartSpecificationList : MonoBehaviour
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
        listRoot.GetComponentInChildren<Toggle>().isOn = true; //Enable first toggle
    }

    public void OnDeleteModeActivated()
    {
        foreach(var toggle in listRoot.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
        }
    }
}
