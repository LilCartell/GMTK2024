using System.Collections.Generic;

public class ClearGridShipBuildingAction : IShipBuildingAction
{
    private List<DeleteSpecsFromCellShipBuildingAction> _deleteActionsList;

    public ClearGridShipBuildingAction()
    {
        _deleteActionsList = new List<DeleteSpecsFromCellShipBuildingAction>();
    }
    public void Do()
    {
        _deleteActionsList.Clear();
        foreach(var cell in BuildingShipScene.Instance.buildingGrid.GetShipBuildingCells())
        {
            if(cell.LoadedSpecification != null && cell.isDeletable)
            {
                var newDeleteAction = new DeleteSpecsFromCellShipBuildingAction(cell.LoadedSpecification, cell);
                newDeleteAction.Do();
                _deleteActionsList.Add(newDeleteAction);
            }
        }
    }

    public void Undo()
    {
        foreach (var action in _deleteActionsList)
        {
            action.Undo();
        }
    }
}
