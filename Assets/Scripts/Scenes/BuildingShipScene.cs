using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildingShipScene : MonoBehaviour
{
    public ShipBuildingGrid buildingGrid;
    public ShipPartSpecificationDescription shipPartSpecificationDescription;
    public Button undoButton;
    public Button redoButton;

    public Button cheatButton;

    public ShipPartSpecification SelectedSpecification { get; private set; }
    public ShipBuildingActionQueue ShipBuildingActionQueue { get; private set; }
    public bool IsInDeleteMode { get; private set; }

    public static BuildingShipScene Instance { get { return _instance; } }
    private static BuildingShipScene _instance;

    private void Awake()
    {
        _instance = this;
        ShipBuildingActionQueue = new ShipBuildingActionQueue();

    #if UNITY_EDITOR
        cheatButton.gameObject.SetActive(true);
    #else
        cheatButton.gameObject.SetActive(false);
    #endif
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

    public void Confirm()
    {
        GameSession.Instance.CurrentShipBlueprint = buildingGrid.GetShipBlueprint();
        SceneManager.LoadScene("ShmupScene");
    }

    public void Delete()
    {
        IsInDeleteMode = true;
    }

    public void Clear()
    {
        ShipBuildingActionQueue.QueueAndDoAction(new ClearGridShipBuildingAction());
    }

    public void Cheat()
    {
        GameSession.Instance.CurrentMoney = 1000000000;
    }

    public void SetSelectedSpecification(ShipPartSpecification selectedSpecification)
    {
        SelectedSpecification = selectedSpecification;
        IsInDeleteMode = false;
        RefreshSpecificationDescription();
    }
    
    public void RefreshSpecificationDescription()
    {
        shipPartSpecificationDescription.LoadWithSpecification(SelectedSpecification);
    }
}
