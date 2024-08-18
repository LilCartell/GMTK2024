using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShip : ShmupCharacter
{
    private void Start()
    {
        if(GameSession.Instance.CurrentShipBlueprint == null)
        {
            GameSession.Instance.CurrentShipBlueprint = new ShipBlueprint();
            var shipParts = new List<ShipPart>();
            var archetypes = Resources.LoadAll<ShipPartArchetype>("");
            var hullArchetype = archetypes.First(archetype => archetype.ShipPartType == ShipPartType.HULL);
            var hullPlacementCoordinates = new List<Coordinates>()
            {
                new Coordinates(0,0), new Coordinates(1,0),
                new Coordinates(0,1), new Coordinates(1,1)
            };
            var hullSpec = new ShipPartSpecification() { ShipPartArchetype = hullArchetype };
            foreach(var coordinates in hullPlacementCoordinates)
            {
                var shipPart = new ShipPart() { Specification = hullSpec, Coordinates = coordinates };
                shipParts.Add(shipPart);
            }

            var reactorArchetype = archetypes.First(archetype => archetype.ShipPartType == ShipPartType.REACTOR);
            var reactorLeftSpec = new ShipPartSpecification() { ShipPartArchetype = reactorArchetype, Orientation = Directions.LEFT };
            var reactorLeftCoordinates = new Coordinates(-1, 0);
            shipParts.Add(new ShipPart() { Specification = reactorLeftSpec, Coordinates = reactorLeftCoordinates });

            var reactorRightSpec = new ShipPartSpecification() { ShipPartArchetype = reactorArchetype, Orientation = Directions.RIGHT };
            var reactorRightCoordinates = new Coordinates(2, 0);
            shipParts.Add(new ShipPart() { Specification = reactorRightSpec, Coordinates = reactorRightCoordinates });

            var reactorTopSpec = new ShipPartSpecification() { ShipPartArchetype = reactorArchetype, Orientation = Directions.UP };
            var reactorTopCoordinates = new Coordinates(0, -1);
            shipParts.Add(new ShipPart() { Specification = reactorTopSpec, Coordinates = reactorTopCoordinates });

            var reactorBottomSpec = new ShipPartSpecification() { ShipPartArchetype = reactorArchetype, Orientation = Directions.DOWN };
            var reactorBottomCoordinates = new Coordinates(0, 2);
            shipParts.Add(new ShipPart() { Specification = reactorBottomSpec, Coordinates = reactorBottomCoordinates });

            var gunArchetype = archetypes.First(archetype => archetype.ShipPartType == ShipPartType.GUN);
            var gunLeftSpec = new ShipPartSpecification() { ShipPartArchetype = gunArchetype, Orientation = Directions.LEFT };
            var gunLeftCoordinates = new Coordinates(-1, 1);
            shipParts.Add(new ShipPart() { Specification = gunLeftSpec, Coordinates = gunLeftCoordinates });

            var gunRightSpec = new ShipPartSpecification() { ShipPartArchetype = gunArchetype, Orientation = Directions.RIGHT };
            var gunRightCoordinates = new Coordinates(2, 1);
            shipParts.Add(new ShipPart() { Specification = gunRightSpec, Coordinates = gunRightCoordinates });

            var gunTopSpec = new ShipPartSpecification() { ShipPartArchetype = gunArchetype, Orientation = Directions.UP };
            var gunTopCoordinates = new Coordinates(1, -1);
            shipParts.Add(new ShipPart() { Specification = gunTopSpec, Coordinates = gunTopCoordinates });

            var gunBottomSpec = new ShipPartSpecification() { ShipPartArchetype = gunArchetype, Orientation = Directions.DOWN };
            var gunBottomCoordinates = new Coordinates(1, 2);
            shipParts.Add(new ShipPart() { Specification = gunBottomSpec, Coordinates = gunBottomCoordinates });

            GameSession.Instance.CurrentShipBlueprint.LoadWithShipParts(shipParts);
        }

        var shipCenterCoordinates = GameSession.Instance.CurrentShipBlueprint.GetCenterCoordinates();
        foreach(var shipPart in GameSession.Instance.CurrentShipBlueprint.GetShipParts())
        {
            float xDifference = shipPart.Coordinates.X - shipCenterCoordinates.X;
            float yDifference = shipPart.Coordinates.Y - shipCenterCoordinates.Y;
            var newPart = Instantiate(shipPart.Specification.ShipPartArchetype.ShmupScenePrefab);
            newPart.transform.SetParent(this.transform);
            newPart.transform.localScale = Vector3.one;
            newPart.transform.localRotation = shipPart.Specification.Orientation.GetRotation();
            newPart.transform.localPosition = new Vector3(xDifference, -yDifference, 0) * ShmupScene.Instance.PlayerShipPartsSize;
        }
        _life = GameSession.Instance.CurrentShipBlueprint.GetTotalHull();
    }

    protected override void HandleDeath()
    {
        Debug.Log("Perdu !");
        Destroy(this.gameObject);
    }
}
