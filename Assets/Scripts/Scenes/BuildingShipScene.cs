using UnityEngine;
using UnityEngine.UI;

public class BuildingShipScene : MonoBehaviour
{
    public ShipPartSpecificationDescription shipPartSpecificationDescription;
    public Button undoButton;
    public Button redoButton;

    public ShipPartSpecification SelectedSpecification { get; private set; }
    public ShipBuildingActionQueue ShipBuildingActionQueue { get; private set; }

    public static BuildingShipScene Instance { get { return _instance; } }
    private static BuildingShipScene _instance;

    private void Awake()
    {
        _instance = this;
        ShipBuildingActionQueue = new ShipBuildingActionQueue();
    }

    private void Update()
    {
        undoButton.interactable = ShipBuildingActionQueue.CanUndo();
        redoButton.interactable = ShipBuildingActionQueue.CanRedo();
    }

    public void Undo()
    {
        ShipBuildingActionQueue.UndoLastAction();
    }

    public void Redo()
    {
        ShipBuildingActionQueue.RedoLastAction();
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
