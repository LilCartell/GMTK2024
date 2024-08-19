using System.Collections.Generic;
using System.Linq;

public class ShipBuildingActionQueue
{
    private List<IShipBuildingAction> _doneActionsList = new List<IShipBuildingAction>();
    private List<IShipBuildingAction> _undoneActionsList = new List<IShipBuildingAction>();

    public void QueueAndDoAction(IShipBuildingAction action)
    {
        _doneActionsList.Add(action);
        action.Do();
        _undoneActionsList.Clear();
        BuildingShipScene.Instance.shipStatsPanel.Refresh();
    }

    public void UndoLastAction()
    {
        var lastAction = _doneActionsList.Last();
        _undoneActionsList.Add(lastAction);
        lastAction.Undo();
        _doneActionsList.Remove(lastAction);
        BuildingShipScene.Instance.shipStatsPanel.Refresh();
    }

    public void RedoLastAction()
    {
        var lastAction = _undoneActionsList.Last();
        _doneActionsList.Add(lastAction);
        lastAction.Do();
        _undoneActionsList.Remove(lastAction);
        BuildingShipScene.Instance.shipStatsPanel.Refresh();
    }

    public bool CanUndo()
    {
        return _doneActionsList.Count > 0;
    }

    public bool CanRedo()
    {
        return _undoneActionsList.Count > 0;
    }
}
