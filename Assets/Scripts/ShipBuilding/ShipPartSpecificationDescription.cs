using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipPartSpecificationDescription : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Weight;
    public TextMeshProUGUI Cost;

    public void LoadWithSpecification(ShipPartSpecification specification)
    {
        if(specification == null)
        {
            Name.text = "";
            Description.text = "";
            Weight.text = "";
            Cost.text = "";
        }
        else
        {
            Name.text = specification.ShipPartArchetype.Name + " (" + specification.Orientation.ToString() + ")";
            Description.text = specification.ShipPartArchetype.Description;
            Weight.text = specification.ShipPartArchetype.Weight.ToString();
            Cost.text = specification.ShipPartArchetype.Cost.ToString();
        }
    }
}
