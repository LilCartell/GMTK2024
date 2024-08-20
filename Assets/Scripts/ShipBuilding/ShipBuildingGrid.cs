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
        var centerCoordinates = new Coordinates
        (
            Mathf.RoundToInt(((float)ColumnsNumber) / 2.0f),
            Mathf.RoundToInt(((float)LinesNumber) / 2.0f)
        );

        //Initializing with a single hull part in the center of the ship;
        if (GameSession.Instance.CurrentShipBlueprint == null)
        {
            GameSession.Instance.CurrentShipBlueprint = new ShipBlueprint();
            var hullArchetype = Resources.LoadAll<ShipPartArchetype>("").First(archetype => archetype.ShipPartType == ShipPartType.HULL);
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
            var coordinates = GetCoordinatesFromIndex(i);
            newCellComponent.SetCoordinates(coordinates);
            newCellComponent.isDeletable = !coordinates.Equals(centerCoordinates);
            _buildingCells.Add(newCellComponent);
        }

        //Setting the grid with the ship
        foreach(var shipPart in GameSession.Instance.CurrentShipBlueprint.GetShipParts())
        {
            _buildingCells[GetIndexFromCoordinates(shipPart.Coordinates)].PlaceSpecInCell(shipPart.Specification);
        }
        GameSession.Instance.CurrentMoney = GameSession.Instance.GetCurrentLevelInfo().Money - GameSession.Instance.CurrentShipBlueprint.GetTotalCost();
    }

    private void Start()
    {
        BuildingShipScene.Instance.shipStatsPanel.Refresh();
    }

    public void Update()
    {
        remainingMoney.text = GameSession.Instance.CurrentMoney.ToString("00");
    }

    public List<ShipBuildingCell> GetShipBuildingCells()
    {
        return _buildingCells;
    }

    public bool CanBuildSpecificationsOnCoordinates(ShipPartSpecification specifications, Coordinates coordinates)
    {
        int consideredCellIndex = GetIndexFromCoordinates(coordinates);
        if(consideredCellIndex < 0 || consideredCellIndex >= _buildingCells.Count)
            return false;

        var consideredCell = _buildingCells[consideredCellIndex];
        if (!consideredCell.isDeletable || !IsShipConnectedWithoutCell(consideredCell))
            return false;

        var directionsToTest = new List<Directions>() { Directions.RIGHT, Directions.LEFT, Directions.DOWN, Directions.UP };
        bool hasAHullNextToIt = false;
        bool hasAHullOpposedToIt = false;
        bool cellInCoordinatesIsCurrentlySupportingSomething = false;
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
                        if(cellInThatDirection.LoadedSpecification.Orientation == direction)
                        {
                            cellInCoordinatesIsCurrentlySupportingSomething = true;
                        }
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
            if(cellInCoordinatesIsCurrentlySupportingSomething) //Don't allow replacement of a supporting cell with something that won't be a support
                return false;

            if (!hasAHullOpposedToIt) //Cells pointing in a direction must have a hull opposed to them to "support" them
                return false;

            int cellPointedByNewCellIndex = GetIndexFromCoordinates(coordinates.GetCoordinatesInDirection(specifications.Orientation));
            if (cellPointedByNewCellIndex >= 0 && cellPointedByNewCellIndex < _buildingCells.Count)
            {
                var cellPointedByNewCell = _buildingCells[cellPointedByNewCellIndex];
                if (cellPointedByNewCell.LoadedSpecification != null)
                    return false; //Don't put a new cell pointing to one already in place
            }
        }
        return true;
    }

    public bool CanDeleteCell(ShipBuildingCell cell)
    {
        if (cell.LoadedSpecification != null)
        {
            if (cell.LoadedSpecification.ShipPartArchetype.ShipPartType != ShipPartType.HULL)
                return true;

            var directionsToTest = new List<Directions>() { Directions.RIGHT, Directions.LEFT, Directions.DOWN, Directions.UP };

            foreach (var direction in directionsToTest)
            {
                int cellIndexInThatDirection = GetIndexFromCoordinates(cell.Coordinates.GetCoordinatesInDirection(direction));
                if (cellIndexInThatDirection > 0 && cellIndexInThatDirection < _buildingCells.Count)
                {
                    var cellInThatDirection = _buildingCells[cellIndexInThatDirection];
                    if(cellInThatDirection.LoadedSpecification != null && cellInThatDirection.LoadedSpecification.Orientation == direction)
                    {
                        return false; //Cell is currently a support for a gun or reactor
                    }
                }
            }

            return IsShipConnectedWithoutCell(cell);
        }
        return false;
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

    private bool IsShipConnectedWithoutCell(ShipBuildingCell cellToTest)
    {
        var currentShipParts = GetShipBlueprint().GetShipParts();
        currentShipParts.RemoveAll(shipPart => shipPart.Coordinates.Equals(cellToTest.Coordinates));
        int hullShipParts = currentShipParts.Count(shipPart => shipPart.Specification.ShipPartArchetype.ShipPartType == ShipPartType.HULL);
        int hullsConnected = 0;
        var queueToTestConnection = new List<Coordinates>();
        var firstHull = currentShipParts.FirstOrDefault(shipPart => shipPart.Specification.ShipPartArchetype.ShipPartType == ShipPartType.HULL);
        if (firstHull != null)
        {
            queueToTestConnection.Add(firstHull.Coordinates);
        }
        else
        {
            return false;
        }

        var alreadyTestedCoordinates = new List<Coordinates>();
        while(queueToTestConnection.Count > 0 )
        {
            ++hullsConnected;
            var coordinatesToTest = queueToTestConnection.First();
            queueToTestConnection.RemoveAt(0);
            alreadyTestedCoordinates.Add(coordinatesToTest);
            var directionsToTest = new List<Directions>() { Directions.RIGHT, Directions.LEFT, Directions.DOWN, Directions.UP };
            foreach (var direction in directionsToTest)
            {
                int cellIndexInThatDirection = GetIndexFromCoordinates(coordinatesToTest.GetCoordinatesInDirection(direction));
                if (cellIndexInThatDirection > 0 && cellIndexInThatDirection < _buildingCells.Count)
                {
                    var cellInThatDirection = _buildingCells[cellIndexInThatDirection];
                    if(cellInThatDirection != cellToTest && cellInThatDirection.LoadedSpecification !=null
                        && cellInThatDirection.LoadedSpecification.ShipPartArchetype.ShipPartType == ShipPartType.HULL
                        && !alreadyTestedCoordinates.Contains(cellInThatDirection.Coordinates)
                        && !queueToTestConnection.Contains(cellInThatDirection.Coordinates))
                    {
                        queueToTestConnection.Add(cellInThatDirection.Coordinates);
                    }
                }
            }
        }

        return (hullsConnected == hullShipParts);
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
