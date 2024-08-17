using UnityEngine;

public class BuildingShipScene : MonoBehaviour
{
    public ShipPartSpecificationDescription shipPartSpecificationDescription;

    public ShipPartSpecification SelectedSpecification { get; private set; }

    public static BuildingShipScene Instance { get { return _instance; } }
    private static BuildingShipScene _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void SetSelectedSpecification(ShipPartSpecification selectedSpecification)
    {
        SelectedSpecification = selectedSpecification;
        RefreshSpecificationDescription();
    }
    
    public void RefreshSpecificationDescription()
    {
        shipPartSpecificationDescription.LoadWithSpecification(SelectedSpecification);
    }
}
