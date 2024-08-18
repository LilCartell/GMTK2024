public class DeleteSpecsFromCellShipBuildingAction : IShipBuildingAction
{
    private ShipPartSpecification _shipPartSpecification;
    private ShipBuildingCell _shipBuildingCell;

    public DeleteSpecsFromCellShipBuildingAction(ShipPartSpecification shipPartSpecification, ShipBuildingCell shipBuildingCell)
    {
        _shipPartSpecification = shipPartSpecification;
        _shipBuildingCell = shipBuildingCell;
    }
    public void Do()
    {
        _shipBuildingCell.RemoveSpecFromCell();
        GameSession.Instance.CurrentMoney += _shipPartSpecification.ShipPartArchetype.Cost;
    }

    public void Undo()
    {
        _shipBuildingCell.PlaceSpecInCell(_shipPartSpecification);
        GameSession.Instance.CurrentMoney -= _shipPartSpecification.ShipPartArchetype.Cost;
    }
}