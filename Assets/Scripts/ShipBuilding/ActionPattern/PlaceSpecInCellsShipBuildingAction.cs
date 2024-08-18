public class PlaceSpecInCellsShipBuildingAction : IShipBuildingAction
{
    private ShipPartSpecification _oldSpecsInCell;
    private ShipPartSpecification _shipPartSpecification;
    private ShipBuildingCell _shipBuildingCell;

    public PlaceSpecInCellsShipBuildingAction(ShipPartSpecification shipPartSpecification, ShipBuildingCell shipBuildingCell)
    {
        _shipPartSpecification = shipPartSpecification;
        _shipBuildingCell = shipBuildingCell;
    }
    public void Do()
    {
        _oldSpecsInCell = _shipBuildingCell.LoadedSpecification;
        _shipBuildingCell.PlaceSpecInCell(_shipPartSpecification);
        GameSession.Instance.CurrentMoney -= _shipPartSpecification.ShipPartArchetype.Cost;
        if(_oldSpecsInCell != null)
        {
            GameSession.Instance.CurrentMoney += _oldSpecsInCell.ShipPartArchetype.Cost;
        }
    }

    public void Undo()
    {
        _shipBuildingCell.RemoveSpecFromCell();
        GameSession.Instance.CurrentMoney += _shipPartSpecification.ShipPartArchetype.Cost;
        if(_oldSpecsInCell != null)
        {
            _shipBuildingCell.PlaceSpecInCell(_oldSpecsInCell);
            GameSession.Instance.CurrentMoney -= _oldSpecsInCell.ShipPartArchetype.Cost;
        }
    }
}
