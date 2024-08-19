using TMPro;
using UnityEngine;

public class ShipStatsPanel : MonoBehaviour
{
    public TextMeshProUGUI hp;
    public TextMeshProUGUI leftSpeed;
    public TextMeshProUGUI rightSpeed;
    public TextMeshProUGUI upSpeed;
    public TextMeshProUGUI downSpeed;

    public void Refresh()
    {
        var shipBlueprint = BuildingShipScene.Instance.buildingGrid.GetShipBlueprint();
        hp.text = Mathf.RoundToInt(shipBlueprint.GetTotalHull()).ToString();
        leftSpeed.text = Mathf.RoundToInt(shipBlueprint.GetTotalSpeed(Directions.LEFT)).ToString();
        rightSpeed.text = Mathf.RoundToInt(shipBlueprint.GetTotalSpeed(Directions.RIGHT)).ToString();
        upSpeed.text = Mathf.RoundToInt(shipBlueprint.GetTotalSpeed(Directions.UP)).ToString();
        downSpeed.text = Mathf.RoundToInt(shipBlueprint.GetTotalSpeed(Directions.DOWN)).ToString();
    }
}
