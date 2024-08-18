using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipBuildingGrid : MonoBehaviour
{
    public GameObject shipBuildingCellPrefab;
    public GameObject gridRoot;
    public TextMeshProUGUI remainingMoney;
    public int LinesNumber;
    public int ColumnsNumber;

    private List<ShipBuildingCell> _buildingCells;

    private void Awake()
    {
        //Initializing with a single hull part in the center of the ship;
        if (GameSession.Instance.CurrentShipBlueprint == null)
        {
            GameSession.Instance.CurrentShipBlueprint = new ShipBlueprint();
            var hullArchetype = Resources.LoadAll<ShipPartArchetype>("").First(archetype => archetype.ShipPartType == ShipPartType.HULL);
            var centerCoordinates = new Coordinates
            (
                Mathf.RoundToInt(((float)ColumnsNumber) / 2.0f),
                Mathf.RoundToInt(((float)LinesNumber) / 2.0f)
            );
            var hullSpec = new ShipPartSpecification() { ShipPartArchetype = hullArchetype };
            var centerShipPart = new ShipPart() { Specification = hullSpec, Coordinates = centerCoordinates };
            GameSession.Instance.CurrentShipBlueprint.LoadWithShipParts(new List<ShipPart>() { centerShipPart });
        }

        //Clearing the grid
        foreach (Transform child in gridRoot.transform)
        {
            Destroy(child.gameObject);
        }
        _buildingCells = new List<ShipBuildingCell>();
        var gridLayoutGroup = gridRoot.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = ColumnsNumber;
        for(int i = 0; i < LinesNumber * ColumnsNumber; ++i)
        {
            var newCell = Instantiate(shipBuildingCellPrefab);
            newCell.transform.SetParent(gridRoot.transform);
            newCell.transform.localPosition = Vector3.zero;
            newCell.transform.localScale = Vector3.one;
            var newCellComponent = newCell.GetComponent<ShipBuildingCell>();
            newCellComponent.SetCoordinates(GetCoordinatesFromIndex(i));
            _buildingCells.Add(newCellComponent);
        }

        //Setting the grid with the ship
        foreach(var shipPart in GameSession.Instance.CurrentShipBlueprint.GetShipParts())
        {
            _buildingCells[GetIndexFromCoordinates(shipPart.Coordinates)].PlaceSpecInCell(shipPart.Specification);
        }
        GameSession.Instance.CurrentMoney = BuildingShipScene.Instance.BaseMoney - GameSession.Instance.CurrentShipBlueprint.GetTotalCost();
    }

    public void Update()
    {
        remainingMoney.text = GameSession.Instance.CurrentMoney.ToString();
    }

    public bool CanBuildSpecificationsOnCoordinates(ShipPartSpecification specifications, Coordinates coordinates)
    {
        var directionsToTest = new List<Directions>() { Directions.RIGHT, Directions.LEFT, Directions.DOWN, Directions.UP };
        bool hasAHullNextToIt = false;
        bool hasAHullOpposedToIt = false;
        foreach (var direction in directionsToTest) 
        {
            int cellIndexInThatDirection = GetIndexFromCoordinates(coordinates.GetCoordinatesInDirection(direction));
            if(cellIndexInThatDirection > 0 && cellIndexInThatDirection <_buildingCells.Count)
            {
                var cellInThatDirection = _buildingCells[cellIndexInThatDirection];
                if (cellInThatDirection.LoadedSpecification != null)
                {
                    if (cellInThatDirection.LoadedSpecification.Orientation == Directions.NONE)
                    {
                        hasAHullNextToIt = true;
                        if(specifications.Orientation != Directions.NONE && direction == specifications.Orientation.GetOpposite())
                            hasAHullOpposedToIt = true;
                    }
                    else
                    {
                        if (cellInThatDirection.LoadedSpecification.Orientation == direction.GetOpposite())
                        {
                            return false; //Don't put a new cell if something is pointing at it
                        }
                    }
                }
            }
        }

        if(!hasAHullNextToIt)
        {
            return false; //Cells can't float
        }

        if(specifications.Orientation != Directions.NONE)
        {
            if (!hasAHullOpposedToIt) //Cells pointing in a direction must have a hull opposed to them to "support" them
                return false;

            var cellPointedByNewCell = _buildingCells[GetIndexFromCoordinates(coordinates.GetCoordinatesInDirection(specifications.Orientation))];
            if (cellPointedByNewCell.LoadedSpecification != null)
                return false; //Don't put a new cell pointing to one already in place
        }
        return true;
    }

    public ShipBlueprint GetShipBlueprint()
    {
        var shipBlueprint = new ShipBlueprint();
        var shipParts = new List<ShipPart>();
        foreach (var cell in _buildingCells)
        {
            if (cell.LoadedSpecification != null)
            {
                shipParts.Add(new ShipPart() { Specification = cell.LoadedSpecification, Coordinates = cell.Coordinates });
            }
        }
        shipBlueprint.LoadWithShipParts(shipParts);
        return shipBlueprint;
    }

    private Coordinates GetCoordinatesFromIndex(int index)
    {
        return new Coordinates( index % ColumnsNumber, index / ColumnsNumber );
    }

    private int GetIndexFromCoordinates(Coordinates coordinates)
    {
        return ColumnsNumber * (int)coordinates.Y + (int)coordinates.X;
    }
}
